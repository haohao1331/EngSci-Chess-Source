using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string winPlayer;
        int gameOver=PlayerPrefs.GetInt("winPlayer");

        if (gameOver == 1)
        {
            winPlayer = "Red";
        }
        else
        {
            winPlayer = "Black";
        }
        text.text = winPlayer + " Wins!!!\n";
    }

}
