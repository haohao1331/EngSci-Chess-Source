using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstScenePageTurner : MonoBehaviour
{
    public Text Title;
    public TextMeshProUGUI InsText;

    public GameObject BackBut;
    public GameObject NextPageBut;

    public PageCounter PC;

    public int curPage;
    public int lastPage;
    

    public void nextPageOnClick()
    {
        PC.curPage = PC.curPage + 1;
        setTitleAndText();
        if (PC.curPage == 1)
        {
            activateBackBut();
        }
        if (PC.curPage == lastPage)
        {
            deactivateNextPageBut();
        }
        Debug.Log(PC.curPage);
    }

    public void backOnClick()
    {
        PC.curPage = PC.curPage - 1;
        setTitleAndText();
        if (PC.curPage == 0)
        {
            deactivateBackBut();
        }
        if (PC.curPage == lastPage - 1)
        {
            activateNextPageBut();
        }
        Debug.Log(PC.curPage);
    }

    private void setTitleAndText()
    {
        if (PC.curPage == 0)
        {
            setText0();
        }
        else if (PC.curPage == 1)
        {
            setText1();
        }
        else if (PC.curPage == 2)
        {
            setText2();
        }
        else if (PC.curPage == 3)
        {
            setText3();
        }
        else if (PC.curPage == 4)
        {
            setText4();
        }
        else if (PC.curPage == 5)
        {
            setText5();
        }
        else if (PC.curPage == 6)
        {
            setText6();
        }
        else if (PC.curPage == 7)
        {
            setText7();
        }
        else if (PC.curPage == 8)
        {
            setText8();
        }
        else if (PC.curPage == 9)
        {
            setText9();
        }
        else if (PC.curPage == 10)
        {
            //setText10();
        }
        else if (PC.curPage == 11)
        {
            //setText11();
        }
        else if (PC.curPage == 12)
        {
            //setText12();
        }
    }

    private void activateBackBut()
    {
        BackBut.SetActive(true);
    }

    private void deactivateBackBut()
    {
        BackBut.SetActive(false);
    }

    private void activateNextPageBut()
    {
        NextPageBut.SetActive(true);
    }

    private void deactivateNextPageBut()
    {
        NextPageBut.SetActive(false);
    }

    private void setText0()
    {
        Title.text = "Game Instruction";
        InsText.text = "Engsci Chess is a 3D-Chess game, where two players’ objectives are to eliminate “<b>the Truth</b>” of each others’.  “<b>The Truth</b>” is an abstract status that can be carried through player’s pieces. If the piece that is carrying “<b>the Truth</b>” was captured, the player who owns the piece loses the game. Although “<b>the Truth</b>” was carried initially by the <b>engineers</b>, it can always be passed onto some other pieces. This action is called “<b>Shift</b>”.\n";
    }

    private void setText1()
    {
        Title.text = "Game Instruction";
        InsText.text = "In the game, two players are competing in the 8x8x8 cubes space, each with several scientists as their pieces. In this dimension, for a target unit, we describe the units around it as follows: \n"+
"  -  <b><color=red> Connected </color></b>: If the two cubes share a common<b> face</b>;\n"+
"  -  <b><color=blue> Adjacent </color></b>: If the two cubes share a common<b> edge</b>;\n"+
"  -  <b><color=yellow> Diagonal </color></b>: If the two cubes share a common<b> point</b>.\n";
    }

    private void setText2()
    {
        Title.text = "Game Instruction";
        InsText.text = "Every piece has its <b>stamina</b>, which they will need in order to take action. All pieces start with their <b>maximum stamina</b>, and every turn each piece gains 1 <b>stamina</b> unless it has reached its <b>maximum stamina</b>.\n"+ 
"The cost are as follows: \n"+
"  -  Each <b>move</b> would consume <b>1 stamina</b>. \n"+
"  -  Each <b>capture</b> would consume <b>all the remaining stamina</b>.\n"+
"  -  Each <b>shift</b> would consume <b>1 stamina</b>, and will <b>refund</b> to the piece at the end of the turn.\n";
    }

    private void setText3()
    {
        Title.text = "Game Instruction";
        InsText.text = "Only pieces with max stamina can make actions, and the combination of actions must consume all the stamina this piece has. \n"+
"Each piece has its unique<b> traits</b>, as described in the following section.\n";
    }
    private void setText4()
    {
        InsText.text = "Mathematicians: \n"+
"  -  Number of Pieces per player: 32\n"+
"  -  Max Stamina: 1(make charges in bars)\n"+
"  -  Range: <b><color=red> Connected </color></b> units\n"+
"  -  Traits: [Braveness]: Cannot move towards the player’s own base.\n";
    }
    private void setText5()
    {
        InsText.text = "Physicists:\n"+
"  -  Number of Pieces per player: 8\n"+
"  -  Max Stamina: 2\n"+
"  -  Range: <b><color=red> Connected </color></b> and<b> <color=blue> Adjacent </color></b> units\n"+
"  -  Traits: [Twisted Mind]: The second move can only be<b><color=red> connected </color></b> units if the first move is to<b><color=blue> adjacent </color></b> units; can only be<b><color=blue> adjacent </color></b> units if the first move is to<b><color=red> connected </color></b> units.\n";
    }
    private void setText6()
    {
        InsText.text = "Computer Scientists:\n"+
"  -  Number of Pieces per player: 8\n"+
"  -  Max Stamina: 3\n"+
"  -  Range: <b><color=blue> Adjacent </color></b> units\n"+
"  -  Traits: [Creative Approach]: It can move through the horizontal border of the space and occur on the other side. (i.e.treat the four faces as loops themselves) \n";
    }
    private void setText7()
    {
        InsText.text = "Philosophers: \n"+
"  -  Number of Pieces per player: 2\n"+
"  -  Max Stamina: 4\n"+
"  -  Range: <b><color=red> Connected </color></b> units\n"+
"  -  Traits:  [Mind Shield]: having a 3 x 3 x 1 shield in <b> front </b> of it, that enemies will not be able to move through. <b> Front </b> is by default towards the enemy.\n"+
"              [Obtuse Twist]: can spend<b> 1 stamina </b> to re-select the direction of its <b>front</b>.\n" +
"              [Sympathetic]: cannot<b> capture</b> pieces\n";
    }
    private void setText8()
    {
        InsText.text = "Engineers:\n" +
"  -  Number of Pieces per player: 1\n" +
"  -  Max Stamina: 5\n" +
"  -  Range: <b><color=red>Connected</color></b>, <b><color=blue>Adjacent</color></b> and <b><color=yellow>Diagonal</color></b> units\n" +
"  -  Traits: [Leadership]: start the game with “<b>the truth</b>” on it. \n";

    }
    private void setText9()
    {
        InsText.text = "Controls:\n" +
"  -  <b>W A S D Q E</b> to move selection box\n" +
"  -  <b>SPACE</b> to select piece\n" +
"  -  <b>N M</b> to zoom in/out\n" +
"  -  <b>UP/DOWN/LEFT/RIGHT</b> arrows to rotate the chess board\n" +
"  -  <b>F</b> to enter Shift phase\n" +
"  -  <b>C</b> to enter change shield direction phase\n";
    }
    private void setText10()
    {
        
    }


}
