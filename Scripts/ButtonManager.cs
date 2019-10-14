using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public Button but_playGame, but_instruction, but_mainMenu, but_story, but_about, but_quit, but_yes, but_no;
    public GameObject QuitGamePopUp;
    
    private void Start()
    {
        //Debug.Log("!!");
        if (but_playGame != null)
        {
            but_playGame.onClick.AddListener(delegate { enterGameScene(); });
        }
        if (but_instruction != null)
        {
            but_instruction.onClick.AddListener(delegate { enterInstructionScene(); });
        }
        if (but_quit != null)
        {
            but_quit.onClick.AddListener(delegate { activateQuitGamePopUp(); });
        }
        if (but_mainMenu != null)
        {
            but_mainMenu.onClick.AddListener(delegate { enterMainMenu(); });
        }
        if (but_story != null)
        {
            but_story.onClick.AddListener(delegate { enterStory(); });
        }
        if (but_about != null)
        {
            but_about.onClick.AddListener(delegate { enterAbout(); });
        }
        if(but_yes != null)
        {
            but_yes.onClick.AddListener(delegate { quitGame(); });
        }
        if (but_no != null)
        {
            but_no.onClick.AddListener(delegate { deactivateQuitGamePopUp(); });
        }

    }

    
    public void enterGameScene()
    {
        //Debug.Log("!!");
        SceneManager.LoadScene("GameScene");
    }

    public void enterInstructionScene()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void enterMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void activateQuitGamePopUp()
    {
        QuitGamePopUp.SetActive(true);
        Time.timeScale = 0f;
    }

    public void deactivateQuitGamePopUp()
    {
        QuitGamePopUp.SetActive(false);
        Time.timeScale = 1f;
    }

    public void quitGame()
    {
        Debug.Log("THE GAME HAS QUIT.");
        Application.Quit();
    }

    public void enterStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void enterAbout()
    {
        SceneManager.LoadScene("Credits");
    }
}
