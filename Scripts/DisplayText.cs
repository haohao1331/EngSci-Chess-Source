using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{

    public GameManager gameManager;
    public Text text;

    void Update()
    {
        UnitController unit = gameManager.getUnitMapping()[gameManager.getSelectedPosition()];
        if (unit != null)
        {
            text.text = "Charge: " + unit.curCharge.ToString() + "\n" + "Flag Carrier: " + (unit.getHasFlag() ? "Yes " : "No ")+"\n";
        }
        else
        {
            text.text = "Charge: \nFlag Carrier: \n";
        }
        text.text += (gameManager.isRedTurn ? "Red's" : "Black's") + " turn! \n";

        text.text += gameStateToString();
    }

    private string gameStateToString()
    {
        string ret = "";
        GameManager.GameState currentState = gameManager.getGameState();
        if(currentState == GameManager.GameState.SelectUnit)
        {
            ret = "Please select a unit \n";
        }
        else if(currentState == GameManager.GameState.SelectMovePosition)
        {
            ret = "Please select a position to move\n";
        }
        else if(currentState == GameManager.GameState.MoveFlag)
        {
            ret = "Please select an available white piece to deliver flag \n";
        }else if(currentState == GameManager.GameState.ChangeShieldDirection)
        {
            ret = "Press \"Space\" to change shield direction\n";
        }
        return ret;
    }
}
