using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Text[] arrows;

    public KeyCode up;
    public KeyCode down;
    public KeyCode select;

    public KeyCode up2;
    public KeyCode down2;

    public Image screenTransition;

    private int currentArrow;
    
    private void Start() {
        if(SceneManager.GetActiveScene().name == "Main")
            GameObject.Find("SoundManager").GetComponent<SoundManager>().LoadFirstScreen();

        foreach (var arrow in arrows)
            arrow.enabled = false;

        arrows[0].enabled = true;

        currentArrow = 0;
    }

    private void Update() {
        if ((Input.GetKeyDown(up) || Input.GetKeyDown(up2))
            && currentArrow > 0) {
            arrows[currentArrow].enabled = false;
            currentArrow--;
            arrows[currentArrow].enabled = true;
        }

        else if ((Input.GetKeyDown(down) || Input.GetKeyDown(down2))
            && currentArrow < arrows.Length - 1) {
            arrows[currentArrow].enabled = false;
            currentArrow++;
            arrows[currentArrow].enabled = true;
        }

        else if (Input.GetKeyDown(select))
            StartCoroutine(OptionSelected(arrows[currentArrow].transform.parent.name));

    }

    private IEnumerator OptionSelected(string n) {

        float x = screenTransition.GetComponent<RectTransform>().sizeDelta.x;

        for (int i = 1; i <= 12; i++) {
            screenTransition.GetComponent<RectTransform>().sizeDelta = new Vector2(x, i * 50);

            yield return new WaitForSeconds(.02f);
        }

        if (n == "StartP1")
            SceneManager.LoadScene("SinglePlayerMode");

        if (n == "StartP2")
            SceneManager.LoadScene("TwoPlayersMode");

        if (n == "MainMenu") {
            SceneManager.LoadScene("Main");
        }
    }
}

