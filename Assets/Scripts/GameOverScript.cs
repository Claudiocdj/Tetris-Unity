using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MenuSelectScript {
    
    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void OptionSelected(string n) {
        if(n == "Restart")
            SceneManager.LoadScene("SinglePlayerMode");

        if (n == "MainMenu")
            SceneManager.LoadScene("Main");

        if (n == "RestartP2")
            SceneManager.LoadScene("TwoPlayersMode");
    }
}
