using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTwo : UnitController
{

    private int moveIndex = 0; //0 -> did not move, 1-> AF, 2->AS

    public override List<Vector3Int> getLegalMove()
    {
        List<Vector3Int> temp=null;
        if (moveIndex == 0)
        {
            temp = getAP();
            temp.AddRange(getAS());
        }else if(moveIndex == 1)
        {
            temp = getAS();
        }else if(moveIndex == 2)
        {
            temp = getAF();
        }
        return filterPosition(temp, this.isRed);
    }

    public void setMoveIndex(int moveIndex)
    {
        this.moveIndex = moveIndex;
    }

    public int getMoveIndex()
    {
        return this.moveIndex;
    }

    public override int maxCharge
    {
        get { return 2; }
    }

    public override int type
    {
        get { return 2; }
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
}
