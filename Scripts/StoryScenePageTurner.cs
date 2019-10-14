using UnityEngine;
using UnityEngine.UI;

public class StoryScenePageTurner : MonoBehaviour
{
    public Text Title;
    public Text StoryText;

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
            setText10();
        }
        else if (PC.curPage == 11)
        {
            setText11();
        }
        else if (PC.curPage == 12)
        {
            setText12();
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
        Title.text = "General";
        StoryText.text = "\n\nMathematicians, physicists, computer scientists, philosophers and engineers cover almost all the intelligent humans in the world, but they usually hide themselves in labs or their rooms, studying the essence of truth… What about in the heaven? What would they do without the motivation of making humans great again?";
    }

    private void setText1()
    {
        Title.text = "";
        StoryText.text = "The answer is, they will seek the truth for fun! Don’t assume they are boring people… I mean spirits! They developed a lot of ways to have fun. One thing they don’t like is hide & seek because when Newton steps on a square he becomes Pascal, Watt awaits a second becomes Joule… It is too complicated for them!\n" +
"This became a serious battle when two groups of them found two distinct truth!There is only one truth, the other one must be fake!The spirits all joined the fight.They will fight to the death, although spirits won’t die...\n";
    }
    private void setText2()
    {
        Title.text = "Truth";
        StoryText.text = "\n\nEveryone knows that nobody knows the truth besides the god, and that is correct! Only god knows the truth. Thus, when those geniuses becomes spirits, they won’t miss the opportunity to discover the truth!\n" +
"But what if you find two of them? Things will become nasty. Only the true truth can survive, and what determines the legibility of the truth? Brute Force!And of course, strategies…\n";
    }
    private void setText3()
    {
        Title.text = "Math";
        StoryText.text = "\n\n“Mathematics is the queen of the science” – Carl Friedrich Gauss\n" +
"Mathematics, the beautiful language of the nature, the foundation of all science, logics and sanities. Mathematicians spent their whole life digging the truth, developing the basis for progress in technologies, societies and the entire human species! How wonderful…\n" +
"...At least that’s how mathematicians tell themselves when other scientists and engineers are laughing at them.\n";
    }
    private void setText4()
    {
        Title.text = "";
        StoryText.text = "Mathematicians are the basic units in Engsci Chess, as they claim themselves as the basis of science. They have discovered a lot of truly remarkable proofs, but the game board is too small to contain. What can they do without theorems and proofs? I guess they can move, push and capture! As they lose hairs fairly quickly, their stamina is limited at one…\n";
    }
    private void setText5()
    {
        Title.text = "Physics";
        StoryText.text = "\n\n“It would be better for the true physics if there were no mathematicians on earth. “ -- Daniel Bernoulli\n"+
"Some may claim that physics is only applications of mathematics. We physicists would say, mathematics is only theories of physics!Only if those damn mathematicians are not in the way…...\n";
    }
    private void setText6()
    {
        Title.text = "";
        StoryText.text = "Physicists are the second level units in Engsci Chess. Physicists are confident in eliminating everyone using their great knowledge. However, they are unable to do so if there’s their fellows or someone else in their way. Also, the very moment of their hit, will consume all their energies. Their stamina is double of mathematicians, despite two is nothing to be proud of……\n";
    }
    private void setText7()
    {
        Title.text = "Computer Science";
        StoryText.text = "\n\n“The purpose of computing is insight, not numbers. ”-- Richard Hamming\n"+
"How brilliant are computer scientists? They may not be good at solving an integral, but their little buddy Mr.  Machine solves them 100000 % faster! Though they can find nothing but excuses in a mathematical or physical discussions……\n";
    }
    private void setText8()
    {
        Title.text = "";
        StoryText.text = "Computer Scientists are the third level units in Engsci Chess. Their fasting thinking and clear logic gives them the creativity to break the bound of dimensions. I.e. they can reach the other side of world simply by getting out of the board bound! Nevertheless, their lack of theoreticals makes their reach only one block around them…...Their stamina, as years and years of debugging had trained them, is three. \n";
    }
    private void setText9()
    {
        Title.text = "Philosophy";
        StoryText.text = "\n\n“The only defense against the world is a thorough knowledge of it. ” -- John Locke\n"+
"Through the war between those crazy scientists, our mightful philosopher, is still fighting against themselves. Is the truth the Truth? They questioned.It might not matter now, because they have to defend for their very life.With what ? You might ask.The answer is themself.The shield of their being may even protect others.\n";
    }
    private void setText10()
    {
        Title.text = "";
        StoryText.text = "Philosophers are the fourth level units in Engsci Chess. The can’t, and not willing to attack others. However, their strong mind and knowledge creates a 3 x 3 shield in front of them, which can be used to protect themselves and others. Their stamina is, as you might have guessed, four. \n";
    }
    private void setText11()
    {
        Title.text = "Engineering";
        StoryText.text = "\n\n“Engineers like to solve problems. If there are no problems handily available, they will create their own problems.” --  Scott Adams\n"+
" Oh my oh my. Behind the scenes lies the most powerful beings in research field --engineers.Forgive me if their introduction sounds like an admiration, but who else could be found on this battlefield to be more comprehensive, strong and useful than engineers? Though you could say, they stand on the shoulders of mathematicians and physicists, even computer scientists, they are not giants. \n";
    }
    private void setText12()
    {
        Title.text = "";
        StoryText.text = "Engineers are the top tier units in Engsci Chess. Despite the fact that they look down to mathematicians and physicists, they require computer scientists often to solve complicated tasks. However, that doesn’t make them any weaker! With CS people, they can do crazy things; without them, they still come in handy! Their stamina is five.  \n";
    }
    private void setText13()
    {
        Title.text = "";
        StoryText.text = "";
    }
}
