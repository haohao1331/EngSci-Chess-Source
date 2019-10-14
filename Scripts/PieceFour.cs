using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFour : UnitController
{
    public override List<Vector3Int> getLegalMove()
    {
        List<Vector3Int> temp = getAF();
        return filterPosition(temp, this.isRed,true);
    }

    private static List<Vector3Int> shieldDirections = new List<Vector3Int>{
        new Vector3Int(1,0,0), new Vector3Int(-1,0,0), new Vector3Int(0,1,0), new Vector3Int(0,-1,0), new Vector3Int(0,0,1), new Vector3Int(0,0,-1)
    };

    public override int maxCharge
    {
        get { return 4; }
    }

    public override int type
    {
        get { return 4; }
    }

    public int shieldDirectionIndex;
    public Vector3Int shieldDirection;

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

    public void changeShieldDirection()
    {
        shieldDirectionIndex++;
        if (shieldDirectionIndex > 5) shieldDirectionIndex = 0;

        shieldDirection = shieldDirections[shieldDirectionIndex];
    }

}
