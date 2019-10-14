using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceThree : UnitController
{
    public override List<Vector3Int> getLegalMove()
    {
        List<Vector3Int> temp = getAS(true);
        return filterPosition(temp, this.isRed);
    }


    public override int maxCharge
    {
        get { return 3; }
    }

    public override int type
    {
        get { return 3; }
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
