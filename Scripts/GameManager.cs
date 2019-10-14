using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        SelectUnit, SelectMovePosition, Debug, MoveFlag, ChangeShieldDirection
    }

    public Material boxSelected, boxUnselected, boxShield, redUnselected, redSelected, blackUnselected, blackSelected,legalMoveMat, flagDestinationCandidate;

    private GameState currentState;
    public UnitController selectedUnit;
    public CameraTurner cam;

    public int BOARD_SIZE;
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private Vector3Int selectedPosition = new Vector3Int(0, 0, 0);
    private bool flagMoved=false;

    public List<GameObject> unitPrefabs;
    public GameObject boxPrefab;

    private List<Vector3Int> highlightedLegalMoves = new List<Vector3Int>();
    private List<UnitController> flagAvailableDestinations = new List<UnitController>();

    //private List<GameObject> activeUnit = new List<GameObject>();
    private Dictionary<Vector3Int, BoxController> board = new Dictionary<Vector3Int, BoxController>();
    private Dictionary<Vector3Int, UnitController> unitMapping = new Dictionary<Vector3Int, UnitController>();
    
    //private List<UnitController> legalMoves = new List<UnitController>();

    public bool isRedTurn = true;
    public int gameOver = 0; //1 is red win, -1 is black win, 0 is game in progress

    //UI
    public Button[] but_add = new Button[10];
    public Button but_delete, but_setFlag, but_remFlag, but_addCharge, but_remCharge, but_addChargeAll;

    public int redCharge;
    public int blackCharge;

    private void Start()
    {
        SpawnBoard();
        SpawnAllUnits();
        //InitButton();
        currentState = GameState.SelectUnit;
    }

    private void Update()
    {
        HandleKeyInput();
    }

    private void InitButton()
    {
        but_add[0].onClick.AddListener(delegate { SpawnUnit(0, selectedPosition); });
        but_add[1].onClick.AddListener(delegate { SpawnUnit(1, selectedPosition); });
        but_add[2].onClick.AddListener(delegate { SpawnUnit(2, selectedPosition); });
        but_add[3].onClick.AddListener(delegate { SpawnUnit(3, selectedPosition); });
        but_add[4].onClick.AddListener(delegate { SpawnUnit(4, selectedPosition); });
        but_add[5].onClick.AddListener(delegate { SpawnUnit(5, selectedPosition); });
        but_add[6].onClick.AddListener(delegate { SpawnUnit(6, selectedPosition); });
        but_add[7].onClick.AddListener(delegate { SpawnUnit(7, selectedPosition); });
        but_add[8].onClick.AddListener(delegate { SpawnUnit(8, selectedPosition); });
        but_add[9].onClick.AddListener(delegate { SpawnUnit(9, selectedPosition); });

        but_delete.onClick.AddListener(delegate { DeleteUnit(selectedPosition); });
        but_setFlag.onClick.AddListener(delegate { SetFlag(); });
        but_remFlag.onClick.AddListener(delegate { RemoveFlag(); });
        but_addCharge.onClick.AddListener(delegate { AddCharge(); });
        but_remCharge.onClick.AddListener(delegate { RemoveCharge(); });
        but_addChargeAll.onClick.AddListener(delegate { AddChargeToCurrentPlayer(); });
    }

    private void HandleKeyInput()
    {
        if (currentState == GameState.SelectUnit)
        {
            HandleSelectionKeyInput();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SelectUnit(selectedPosition);
            }
        }
        else if(currentState == GameState.SelectMovePosition)
        {
            HandleSelectionKeyInput();
            if (Input.GetKeyUp(KeyCode.C))
            {
                enterChangeShieldDirectionState();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                MoveUnit(selectedPosition);
                endGameCheck();
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                enterMoveFlagState();
            }
        }else if(currentState == GameState.MoveFlag)
        {
            HandleSelectionKeyInput();
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (moveFlag(selectedPosition))
                {
                    leaveMoveFlagState();
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                leaveMoveFlagState();
            }
        }else if(currentState == GameState.ChangeShieldDirection)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                changeShieldDirection();
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                leaveChangeShieldDiretionState();
            }
        }
    }

    private void HandleSelectionKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            updateSelectedPosition(new Vector3Int(-1, 0, 0));
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            updateSelectedPosition(new Vector3Int(1, 0, 0));
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            updateSelectedPosition(new Vector3Int(0, 0, -1));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            updateSelectedPosition(new Vector3Int(0, 0, 1));
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            updateSelectedPosition(new Vector3Int(0, 1, 0));
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            updateSelectedPosition(new Vector3Int(0, -1, 0));
        }

    }

    private void updateSelectedPosition(Vector3Int diff)
    {
        float x = -Camera.main.transform.position.x + cam.pivot.x, z = -Camera.main.transform.position.z + cam.pivot.z;
        //Debug.Log(cam.getCameraRegion(x, z));
        //if (x <= -5.077 + 0.19)
        if(cam.getCameraRegion(x,z)==1)
        {
            diff.x = -diff.x;
            diff.z = -diff.z;
        }
        //else if(z > 5.077 + 0.79)
        else if(cam.getCameraRegion(x,z)==4)
        {
            diff.x += diff.z;
            diff.z = diff.x - diff.z;
            diff.x = diff.x - diff.z;
            diff.x = -diff.x;
        }
        //else if(z < -5.077 + 0.79)
        else if(cam.getCameraRegion(x,z)==2)
        {
            diff.x += diff.z;
            diff.z = diff.x - diff.z;
            diff.x = diff.x - diff.z;
            diff.z = -diff.z;
        }
        if(PositionValid(selectedPosition + diff))
        {
            board[selectedPosition].changeSelectedState();
            selectedPosition += diff;
            board[selectedPosition].changeSelectedState();
        }
    }

    private void enterMoveFlagState()
    {
        if (selectedUnit.getHasFlag())
        {
            currentState = GameState.MoveFlag;
            List<Vector3Int> adjacentPositions = selectedUnit.getAF();
            adjacentPositions.AddRange(selectedUnit.getAS());
            adjacentPositions.AddRange(selectedUnit.getAP());

            foreach (Vector3Int position in adjacentPositions)
            {
                if (unitMapping[position] != null)
                {
                    if (unitMapping[position].getRed() == selectedUnit.getRed())
                    {
                        flagAvailableDestinations.Add(unitMapping[position]);
                        unitMapping[position].setFlagCandidate(true);
                    }
                }
            }
        }
    }

    private void leaveMoveFlagState()
    {
        currentState = GameState.SelectMovePosition;

        foreach (UnitController unit in flagAvailableDestinations)
        {
            unit.setFlagCandidate(false);
        }
        flagAvailableDestinations.Clear();
        endTurnBasedOnCurCharge();
    }

    private bool moveFlag(Vector3Int pos)
    {
        bool isLegalFlagPosition = false;
        foreach (UnitController unit in flagAvailableDestinations)
        {
            if (unit == unitMapping[pos])
            {
                isLegalFlagPosition = true;
            }
        }
        if (isLegalFlagPosition)
        {
            unitMapping[pos].setHasFlag(true);
            selectedUnit.setHasFlag(false);
            flagMoved = true;
            selectedUnit.curCharge--;
            return true;
        }
        return false;
    }

    private void enterChangeShieldDirectionState()
    {
        if (selectedUnit.type == 4)
        {
            currentState = GameState.ChangeShieldDirection;
        }
    }

    private void changeShieldDirection()
    {
        if(selectedUnit.type == 4)
        {
            PieceFour p4 = selectedUnit as PieceFour;
            removeShield(p4.getRed(), p4.getPosition(), p4.shieldDirection);
            p4.changeShieldDirection();
            generateShield(p4.getRed(), p4.getPosition(), p4.shieldDirection);
            
        }
    }

    private void leaveChangeShieldDiretionState()
    {
        currentState = GameState.SelectMovePosition;
        selectedUnit.curCharge -= 1;
        endTurnBasedOnCurCharge();
    }

    private void SelectUnit(Vector3Int position)
    {
        if (unitMapping[position] == null) return;
        if (unitMapping[position].getRed() != isRedTurn) return;
        if (unitMapping[position].curCharge < unitMapping[position].maxCharge) return;

        if (unitMapping[position].getIsSelected())
        {
            selectedUnit = null;
        }
        else
        {
            selectedUnit = unitMapping[position];
        }
        unitMapping[position].changeSelectedState();
        currentState = GameState.SelectMovePosition;
        
        updateSelectedUnitLegalMove(selectedPosition);
    }

    private void deselectUnit()
    {
        selectedUnit.changeSelectedState();
        deselectHighlightedLegalMoves(selectedUnit.getPosition());
        selectedUnit = null;
    }

    private void deselectHighlightedLegalMoves(Vector3Int pos)
    {
        for(int i = 0; i < highlightedLegalMoves.Count; i++)
        {
            board[highlightedLegalMoves[i]].removeHighlightedLegalMove();
        }

        highlightedLegalMoves = new List<Vector3Int>();
    }

    private void updateSelectedUnitLegalMove(Vector3Int pos)
    {
        deselectHighlightedLegalMoves(pos);
        highlightedLegalMoves = unitMapping[pos].getLegalMove();

        for(int i = 0; i < highlightedLegalMoves.Count; i++)
        {
            board[highlightedLegalMoves[i]].addLegalMoveIdentifier();
        }
    }

    private void MoveUnit(Vector3Int position)
    {
        if (unitMapping[position] == selectedUnit)
        {
            if(selectedUnit.curCharge == selectedUnit.maxCharge)
            {
                deselectUnit();
                currentState = GameState.SelectUnit;    // Leo
            }
            return;
        }
        //check if pos is in highlightedLegalMoves
        bool legalPos = false;
        foreach (Vector3Int pos in highlightedLegalMoves)
        {
            if (pos == position)
            {
                legalPos = true;
            }
        }
        if (legalPos)
        {
            selectedUnit.curCharge -= 1;
            if (unitMapping[position] != null)
            {
                DeleteUnit(position);
                selectedUnit.curCharge = 0;
            }
            if (selectedUnit.type == 4) removeShield(selectedUnit.getRed(), selectedUnit.getPosition(), (selectedUnit as PieceFour).shieldDirection);
            unitMapping[selectedUnit.getPosition()] = null;
            selectedUnit.setPosition(position);
            selectedUnit.transform.parent.position = position;
            unitMapping[position] = selectedUnit;
            if (selectedUnit.type == 4) generateShield(selectedUnit.getRed(), selectedPosition, (selectedUnit as PieceFour).shieldDirection);
            updateSelectedUnitLegalMove(position);

            endTurnBasedOnCurCharge();
        }
    }

    private void endTurnBasedOnCurCharge()
    {
        if (selectedUnit.curCharge == 0)
        {
            AddChargeToCurrentPlayer();
            isRedTurn = !isRedTurn;
            currentState = GameState.SelectUnit;
            flagMoved = false;
            deselectUnit();
        }
    }

    private void SpawnBoard()
    {
        Vector3 offset = new Vector3(1.81f, 1.69f, 1.21f);
        for(int i = 0; i < BOARD_SIZE; i++)
        {
            for(int j = 0; j < BOARD_SIZE; j++)
            {
                for(int k = 0; k < BOARD_SIZE; k++)
                {
                    Vector3Int pos = new Vector3Int(i, j, k);
                    GameObject box = Instantiate(boxPrefab, pos-offset, Quaternion.identity) as GameObject;
                    board.Add(pos, box.GetComponent<BoxController>());
                    board[pos].init(this);
                    unitMapping.Add(new Vector3Int(i, j, k), null);
                }
            }
        }
        board[selectedPosition].changeSelectedState();
    }

    public void DeleteUnit(Vector3Int position)
    {
        if (unitMapping[position].type == 4) removeShield(unitMapping[position].getRed(), position, (unitMapping[position] as PieceFour).shieldDirection);
        if (unitMapping[position].getHasFlag())
        {
            gameOver = unitMapping[position].getRed() ? -1 : 1;
        }
        Destroy(unitMapping[position].gameObject);
        unitMapping[position] = null;
    }

    public void SpawnUnit(int index, Vector3Int position)
    {
        if (unitMapping[position] == null)
        {
            GameObject go = Instantiate(unitPrefabs[index], position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            unitMapping[position] = go.transform.GetChild(0).GetComponent<UnitController>();
            unitMapping[position].setPosition(position);
            unitMapping[position].init(this);
            if (index == 3) generateShield(true, position, (unitMapping[position] as PieceFour).shieldDirection);
            else if(index == 8) generateShield(false, position, (unitMapping[position] as PieceFour).shieldDirection);
            if (index >= 0 && index <= 4) unitMapping[position].setRed(true);
        }
    }

    private void SpawnAllUnits()
    {
        //Red
        SpawnUnit(4, new Vector3Int(0, 0, 0)); //5
        unitMapping[new Vector3Int(0, 0, 0)].setHasFlag(true);
        SpawnUnit(3, new Vector3Int(5, 0, 2)); //4
        SpawnUnit(3, new Vector3Int(2, 0, 5)); //4

        //SpawnUnit(2, new Vector3Int(0, 0, 0));
        SpawnUnit(1, new Vector3Int(2, 1, 1)); //2
        SpawnUnit(1, new Vector3Int(1, 1, 2)); //2
        SpawnUnit(1, new Vector3Int(5, 1, 1)); //2
        SpawnUnit(1, new Vector3Int(1, 1, 5)); //2
        SpawnUnit(1, new Vector3Int(6, 1, 2)); //2
        SpawnUnit(1, new Vector3Int(2, 1, 6)); //2
        SpawnUnit(1, new Vector3Int(5, 1, 6)); //2
        SpawnUnit(1, new Vector3Int(6, 1, 5)); //2

        SpawnUnit(2, new Vector3Int(2, 1, 3)); //3
        SpawnUnit(2, new Vector3Int(2, 1, 4)); //3
        SpawnUnit(2, new Vector3Int(3, 1, 5)); //3
        SpawnUnit(2, new Vector3Int(4, 1, 5)); //3
        SpawnUnit(2, new Vector3Int(4, 1, 4)); //3
        SpawnUnit(2, new Vector3Int(5, 1, 3)); //3
        SpawnUnit(2, new Vector3Int(4, 1, 2)); //3
        SpawnUnit(2, new Vector3Int(3, 1, 2)); //3
        SpawnUnit1Plane(true);

        //Black
        SpawnUnit1Plane(false);
        SpawnUnit(6, new Vector3Int(2, 6, 1)); //2
        SpawnUnit(6, new Vector3Int(1, 6, 2)); //2
        SpawnUnit(6, new Vector3Int(5, 6, 1)); //2
        SpawnUnit(6, new Vector3Int(1, 6, 5)); //2
        SpawnUnit(6, new Vector3Int(6, 6, 2)); //2
        SpawnUnit(6, new Vector3Int(2, 6, 6)); //2
        SpawnUnit(6, new Vector3Int(5, 6, 6)); //2
        SpawnUnit(6, new Vector3Int(6, 6, 5)); //2

        SpawnUnit(7, new Vector3Int(2, 6, 3)); //3
        SpawnUnit(7, new Vector3Int(2, 6, 4)); //3
        SpawnUnit(7, new Vector3Int(3, 6, 5)); //3
        SpawnUnit(7, new Vector3Int(4, 6, 5)); //3
        SpawnUnit(7, new Vector3Int(5, 6, 4)); //3
        SpawnUnit(7, new Vector3Int(5, 6, 3)); //3
        SpawnUnit(7, new Vector3Int(4, 6, 2)); //3
        SpawnUnit(7, new Vector3Int(3, 6, 2)); //3

        SpawnUnit(9, new Vector3Int(7, 7, 7)); //5
        unitMapping[new Vector3Int(7, 7, 7)].setHasFlag(true);
        SpawnUnit(8, new Vector3Int(2, 7, 5)); //4
        SpawnUnit(8, new Vector3Int(5, 7, 2)); //4
    }

    private void SpawnUnit1Plane(bool isRed)
    {
        int unitIndex = isRed ? 0 : 5;
        int y = isRed ? 2 : 5;
        List<int> spawn1even = new List<int> { 0, 2, 5, 7 };
        List<int> spawn1odd = new List<int> { 1, 3, 4, 6 };
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            if (i <= 3)
            {
                if (i % 2 == 0)
                {
                    foreach (int element in spawn1even)
                    {
                        SpawnUnit(unitIndex, new Vector3Int(i, y, element)); //1
                    }
                }
                else
                {
                    foreach (int element in spawn1odd)
                    {
                        SpawnUnit(unitIndex, new Vector3Int(i, y, element)); //1
                    }
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    foreach (int element in spawn1odd)
                    {
                        SpawnUnit(unitIndex, new Vector3Int(i, y, element)); //1
                    }
                }
                else
                {
                    foreach (int element in spawn1even)
                    {
                        SpawnUnit(unitIndex, new Vector3Int(i, y, element)); //1
                    }
                }
            }
        }
    }

    public void SetFlag()
    {
        if (selectedUnit != null)
        {
            selectedUnit.setHasFlag(true);
        }
    }

    public void RemoveFlag()
    {
        if (selectedUnit != null)
        {
            selectedUnit.setHasFlag(false);
        }
    }

    public void AddCharge()
    {
        if (selectedUnit != null)
        {
            selectedUnit.AddCharge();
        }
    }

    public void RemoveCharge()
    {
        if (selectedUnit != null)
        {
            selectedUnit.RemoveCharge();
        }
    }

    public void AddChargeToCurrentPlayer()
    {
        foreach(UnitController unit in unitMapping.Values)
        {
            if (unit != null && unit.getRed()==isRedTurn)
            {
                unit.AddCharge();
            }
        }
        if (flagMoved)
        {
            selectedUnit.AddCharge();
        }
    }

    public void generateShield(bool isRed, Vector3Int position, Vector3Int direction)
    {
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(direction.x != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(0, i, j);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].addShield(isRed);
                    }
                }
                else if(direction.y != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(i, 0, j);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].addShield(isRed);
                    }
                }
                else if (direction.z != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(i, j, 0);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].addShield(isRed);
                    }
                }
            }
        }
    }

    public void removeShield(bool isRed, Vector3Int position, Vector3Int direction)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (direction.x != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(0, i, j);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].removeShield(isRed);
                    }
                }
                else if (direction.y != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(i, 0, j);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].removeShield(isRed);
                    }
                }
                else if (direction.z != 0)
                {
                    Vector3Int shieldPos = position + direction + new Vector3Int(i, j, 0);
                    if (PositionValid(shieldPos))
                    {
                        board[shieldPos].removeShield(isRed);
                    }
                }
            }
        }
    }

    public void endGameCheck()  //Leo
    {
        if (gameOver != 0)
        {
            PlayerPrefs.SetInt("winPlayer", gameOver);
            SceneManager.LoadScene("GameOver");
        }
    }

    public static void changeMaterial(Transform transform, Material mat)
    {
        transform.GetComponent<Renderer>().material = mat;
    }

    public Dictionary<Vector3Int, UnitController> getUnitMapping()
    {
        return this.unitMapping;
    }

    public Dictionary<Vector3Int, BoxController> getBoard()
    {
        return board;
    }

    public Vector3Int getSelectedPosition()
    {
        return selectedPosition;
    }

    public bool PositionValid(Vector3Int pos)
    {

        if (pos.x >= 0 && pos.x < BOARD_SIZE && pos.y >= 0 && pos.y < BOARD_SIZE && pos.z >= 0 && pos.z < BOARD_SIZE)
        {
            return true;
        }
        return false;
    }

    public GameState getGameState()     //Leo
    {
        return currentState;
    }
}

