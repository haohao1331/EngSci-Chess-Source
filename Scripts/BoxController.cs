using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

    public GameManager gameManager;
    public bool isSelected;
    public bool hasRedShield;
    public bool hasBlackShield;
    //private Material baseMat;
    public bool legalMoveIdentifierOn;

    public void changeSelectedState()
    {
        isSelected = !isSelected;
        changeMaterial();
    }

    public void addShield(bool isRed)
    {
        if (isRed) hasRedShield = true;
        else hasBlackShield = true;
        changeMaterial();
    }

    public void removeShield(bool isRed)
    {
        if (isRed) hasRedShield = false;
        else if (!isRed) hasBlackShield = false;
        changeMaterial();
    }

    public void init(GameManager gm)
    {
        this.gameManager = gm;
    }


    public void addLegalMoveIdentifier()
    {
        legalMoveIdentifierOn = true;
        changeMaterial();
    }

    public void removeHighlightedLegalMove()
    {
        legalMoveIdentifierOn=false;
        changeMaterial();
    }

    private void changeMaterial()
    {
        if (!isSelected)
        {
            if (!legalMoveIdentifierOn)
            {
                if (hasRedShield || hasBlackShield)
                {
                    GameManager.changeMaterial(this.gameObject.transform, gameManager.boxShield);
                }
                else
                {
                    GameManager.changeMaterial(this.gameObject.transform, gameManager.boxUnselected);
                }
            }
            else
            {
                GameManager.changeMaterial(this.gameObject.transform, gameManager.legalMoveMat);
            }
        }
        else
        {
            GameManager.changeMaterial(this.gameObject.transform, gameManager.boxSelected);
        }
    }
}

    


