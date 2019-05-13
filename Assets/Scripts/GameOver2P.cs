using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver2P : MonoBehaviour {

    public Text gameOverP1;
    public Text gameOverP2;

    public Text scoreP1;
    public Text scoreP2;

    public Text winText;

    public GameObject gameOverMenu;

    protected  void Update(){
        if (gameOverP1.IsActive() && gameOverP2.IsActive()) {
            int p1 = int.Parse(scoreP1.text);
            int p2 = int.Parse(scoreP2.text);

            if (p1 > p2) winText.text = "PLAYER 1 WIN";
            else if (p1 < p2) winText.text = "PLAYER 2 WIN";
            else if (p1 == p2) winText.text = "DRAW";

            gameOverMenu.SetActive(true);
        }
    }


}
