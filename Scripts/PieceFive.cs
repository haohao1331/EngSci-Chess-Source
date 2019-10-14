using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFive : UnitController
{

    public override List<Vector3Int> getLegalMove()
    {
        List<Vector3Int> temp = getAP();
        temp.AddRange(getAS());
        temp.AddRange(getAF());
        return filterPosition(temp, this.isRed);
    }

    public override int maxCharge
    {
        get { return 5; }
    }

    public override int type
    {
        get { return 5; }
    }

    private int charge = 0;
    public override int curCharge
    {
        get
        {
            return charge;
        }

        set
        {
            charge = value;
        }
    }
    /*
    public override bool PossibleMove(Vector3Int pos)
    {
        
        Dictionary<Vector3Int, UnitController> unitMapping = gameManager.getUnitMapping();
        Dictionary<Vector3Int, BoxController> board = gameManager.getBoard();
        if (unitMapping.ContainsKey(pos) && unitMapping[pos] != null || (board[pos].hasBlackShield && isRed) || (board[pos].hasRedShield && !isRed) || pos == this.position)
        {
            return false;
        }
        if (pos.x - this.position.x <= 1 && pos.x - this.position.x >= -1 && pos.y - this.position.y <= 1 && pos.y - this.position.y >= -1 && pos.z - this.position.z <= 1 && pos.z - this.position.z >= -1)
        {
            return true;
        }
        bool inPath = false;
        foreach(Vector3Int tile in this.path)
        {
            if (tile == pos) inPath = true;
        }
        if (inPath)
        {
            return UnitController.LeoWeirdRulesCanCapture(pos, this.position);
            
        }
        return false;
        
        return true;
    }
    */
}
