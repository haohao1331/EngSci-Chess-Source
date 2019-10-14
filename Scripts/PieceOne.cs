using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceOne : UnitController
{

    public override List<Vector3Int> getLegalMove()
    {
        List<Vector3Int> temp = getAF(true);
        return filterPosition(temp, this.isRed);
    }




    public override int maxCharge
    {
        get { return 1; }
    }

    public override int type
    {
        get { return 1; }
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
