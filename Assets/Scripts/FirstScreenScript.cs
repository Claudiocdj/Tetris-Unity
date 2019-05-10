using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScreenScript : MonoBehaviour{

    public Text[] arrows;

    public KeyCode upInput;
    public KeyCode downInput;
    public KeyCode actionInput;

    private int currentSelected;

    private void Start(){
        
        foreach (var arrow in arrows)
            arrow.enabled = false;

        arrows[0].enabled = true;

        currentSelected = 0;
    }

    private void Update() {

        if (Input.GetKeyDown(upInput) && currentSelected > 0) {
            arrows[currentSelected].enabled = false;
            currentSelected--;
            arrows[currentSelected].enabled = true;
        }

        else if (Input.GetKeyDown(downInput) && currentSelected < arrows.Length-1) {
            arrows[currentSelected].enabled = false;
            currentSelected++;
            arrows[currentSelected].enabled = true;
        }

        if (Input.GetKeyDown(actionInput)) {
            ButtonClick(currentSelected);
        }
    }

    private void ButtonClick(int buttonIndex) {
        if (buttonIndex == 0)
            SceneManager.LoadScene("SinglePlayerMode");

        else if (buttonIndex == 1)
            SceneManager.LoadScene("TwoPlayersMode");
    }

}
