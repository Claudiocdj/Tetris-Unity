using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScreenScript : MenuSelectScript {

    protected override void Start(){
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void OptionSelected(string n) {
        if (n == "SinglePlayer")
            SceneManager.LoadScene("SinglePlayerMode");

        if(n == "TwoPlayers")
            SceneManager.LoadScene("TwoPlayersMode");
    }

}
