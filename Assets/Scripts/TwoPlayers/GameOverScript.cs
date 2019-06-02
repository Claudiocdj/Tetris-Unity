using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour{

    public Text scoreP1;
    public Text scoreP2;

    public Text gameOverP1;
    public Text gameOverP2;

    public Text winText;

    public GameObject menu;

    void Update() {
        if(gameOverP1.IsActive() && gameOverP2.IsActive()) {
            
            if (int.Parse(scoreP1.text) > int.Parse(scoreP2.text))
                winText.text = "PLAYER 1 WIN";

            else if(int.Parse(scoreP1.text) < int.Parse(scoreP2.text))
                winText.text = "PLAYER 2 WIN";

            else
                winText.text = "DRAW";

            menu.SetActive(true);
        }
    }
}
