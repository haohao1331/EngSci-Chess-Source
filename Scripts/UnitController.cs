using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController : MonoBehaviour
{
    protected GameManager gameManager;
    protected Vector3Int position;
    protected bool isRed;
    protected bool isSelected = false;
    protected List<Vector3Int> path = new List<Vector3Int>();

    abstract public int type { get; }
    abstract public int curCharge { set; get; }
    abstract public int maxCharge { get; }
    protected bool hasFlag;
    protected bool isFlagDestinationCandidate;

    private List<Vector3Int> legalMoves = new List<Vector3Int>();

    void Start()
    {
        curCharge = maxCharge; 
    }
    public abstract List<Vector3Int> getLegalMove();
    public bool getRed()
    {
        return isRed;
    }

    public void setRed(bool red)
    {
        isRed = red;
    }

    public bool getIsSelected()
    {
        return isSelected;
    }

    public void setIsSelected(bool selected) 
    {
        this.isSelected = selected;
    }

    public Vector3Int getPosition()
    {
        return position;
    }

    public void setPosition(Vector3Int position)
    {
        this.position = position;
    }

    public bool getHasFlag()
    {
        return hasFlag;
    }

    public void setHasFlag(bool flag)
    {
        this.hasFlag = flag;
    }
    
    public void changeSelectedState()
    {
        isSelected = !isSelected;
        changeMaterial();
    }

    public void init(GameManager gm)
    {
        this.gameManager = gm;
    }

    public virtual void AddCharge()
    {
        if (curCharge < maxCharge) curCharge++;
    }

    public virtual void RemoveCharge()
    {
        if (curCharge > 0) curCharge--;
    }

    public static Vector3Int AbsVector(Vector3Int vec)
    {
        Vector3Int ret = vec;
        if (ret.x < 0) ret.x = -ret.x;
        if (ret.y < 0) ret.y = -ret.y;
        if (ret.z < 0) ret.z = -ret.z;
        return ret;
    }

    public static List<int> NonzeroComps(Vector3Int vec)
    {
        List<int> ret = new List<int>();
        if (vec.x != 0) ret.Add(vec.x);
        if (vec.y != 0) ret.Add(vec.y);
        if (vec.z != 0) ret.Add(vec.z);
        return ret;
    }

    public static bool CanCapture(List<int> comps)
    {
        int value = comps[0];
        foreach(int component in comps)
        {
            if(component <= 1 || component != value)
            {
                return false;
            }
        }
        return true;
    }

    public static bool LeoWeirdRulesCanCapture(Vector3Int dest, Vector3Int orig)
    {
        return CanCapture(NonzeroComps(AbsVector(dest-orig)));
    }

    public List<Vector3Int> getAF(bool isPieceOne=false)
    {
        List<Vector3Int> unitVectors = new List<Vector3Int>() { };
        if (isPieceOne)
        {
            if(this.isRed)
            {
                unitVectors = new List<Vector3Int>() { new Vector3Int(1,0,0), new Vector3Int(-1, 0, 0),
        new Vector3Int(0,1,0),new Vector3Int(0,0,1),new Vector3Int(0,0,-1)};
            }
            else
            {
                unitVectors = new List<Vector3Int>() { new Vector3Int(1,0,0), new Vector3Int(-1, 0, 0),
        new Vector3Int(0,-1,0),new Vector3Int(0,0,1),new Vector3Int(0,0,-1)};
            }
        }
        else
        {
            unitVectors = new List<Vector3Int>() { new Vector3Int(1,0,0), new Vector3Int(-1, 0, 0),
        new Vector3Int(0,1,0),new Vector3Int(0,-1,0),new Vector3Int(0,0,1),new Vector3Int(0,0,-1)};
        }
        return adjacentPosition(this.position, unitVectors);
    }

    public List<Vector3Int> getAS(bool isPieceThree=false)
    {
        List<Vector3Int> unitVectors = new List<Vector3Int>() {
            new Vector3Int(1,1,0), new Vector3Int(-1,1,0), new Vector3Int(1,-1,0), new Vector3Int(-1,-1,0),
            new Vector3Int(0,1,1), new Vector3Int(0,1,-1), new Vector3Int(0,-1,1), new Vector3Int(0,-1,-1),
            new Vector3Int(1,0,1), new Vector3Int(-1,0,1), new Vector3Int(1,0,-1), new Vector3Int(-1,0,-1)};
        List<Vector3Int> ret = new List<Vector3Int>();
        if (!isPieceThree)
        {
            return adjacentPosition(this.position, unitVectors);
        }
        else
        {
            return pieceThreeAdjacentPosition(this.position, unitVectors);
        }
    }

    public List<Vector3Int> getAP()
    {
        List<Vector3Int> unitVectors = new List<Vector3Int>() {
            new Vector3Int(1,1,1), new Vector3Int(1,1,-1), new Vector3Int(1,-1,1), new Vector3Int(1,-1,-1),
            new Vector3Int(-1,1,1), new Vector3Int(-1,1,-1), new Vector3Int(-1,-1,1), new Vector3Int(-1,-1,-1)};
        return adjacentPosition(this.position,unitVectors);
    }

    public List<Vector3Int> adjacentPosition(Vector3Int pos,List<Vector3Int> unitVectors)
    {
        List<Vector3Int> ret = new List<Vector3Int>();
        foreach (Vector3Int element in unitVectors)
        {
            Vector3Int temp = pos + element;
            if (gameManager.PositionValid(temp))
            {
                ret.Add(temp);
            }
        }
        return ret;
    }

    public List<Vector3Int> filterPosition(List<Vector3Int> adjacentPosition, bool isRed,bool isPieceFour=false)
    {
        List<Vector3Int> ret = new List<Vector3Int>();
        Dictionary<Vector3Int, UnitController> unitMapping = gameManager.getUnitMapping();
        foreach (Vector3Int element in adjacentPosition)
        {
            //Debug.Log(element);
            if (!(gameManager.getBoard()[element].hasBlackShield && isRed) && (!(gameManager.getBoard()[element].hasRedShield && !isRed)))
            {
                if (!isPieceFour) { 
                    if (unitMapping[element] == null || unitMapping[element].getRed()!=isRed)
                    {
                        ret.Add(element);
                    }
                }
                else
                {
                    if (unitMapping[element] == null)
                    {
                        ret.Add(element);
                    }
                }
            }
        }
        return ret;
    }

    public List<Vector3Int> pieceThreeAdjacentPosition(Vector3Int pos, List<Vector3Int> unitVectors)
    {
        List<Vector3Int> ret = new List<Vector3Int>();
        int boardSize = gameManager.BOARD_SIZE;
        bool doesAdd = true;
        foreach (Vector3Int element in unitVectors)
        {
            Vector3Int temp = pos + element;
            if (temp.x<0)
            {
                temp.x+= boardSize;
            }
            else if(temp.x>= boardSize)
            {
                temp.x -= boardSize;
            }
            if (temp.y < 0)
            {
                temp.y += boardSize;
                doesAdd = false;
            }
            else if (temp.y >= boardSize)
            {
                temp.y -= boardSize;
                doesAdd = false;
            }
            if (temp.z < 0)
            {
                temp.z += boardSize;
            }
            else if (temp.z >= boardSize)
            {
                temp.z -= boardSize;
            }

            if (doesAdd)
            {
                ret.Add(temp);
            }
            doesAdd = true;
        }
        return ret;
    }


    public void setFlagCandidate(bool state)
    {
        isFlagDestinationCandidate = state;
        changeMaterial();
    }

    public void changeMaterial()
    {
        if (isSelected)
        {
            if (isRed)
            {
                GameManager.changeMaterial(this.gameObject.transform.GetChild(0), gameManager.redSelected);
            }
            else
            {
                GameManager.changeMaterial(this.gameObject.transform.GetChild(0), gameManager.blackSelected);
            }
        }
        else
        {
            if (isFlagDestinationCandidate)
            {
                GameManager.changeMaterial(this.gameObject.transform.GetChild(0), gameManager.flagDestinationCandidate);
            }
            else
            {
                if (isRed)
                {
                    GameManager.changeMaterial(this.gameObject.transform.GetChild(0), gameManager.redUnselected);
                }
                else
                {
                    GameManager.changeMaterial(this.gameObject.transform.GetChild(0), gameManager.blackUnselected);
                }
            }
        }
    }


}
