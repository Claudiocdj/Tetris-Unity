using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuSelectScript : MonoBehaviour {

    public Text[] arrows;

    public KeyCode up;
    public KeyCode down;
    public KeyCode select;

    private int currentArrow;

    protected virtual void Start() {
        foreach (var arrow in arrows)
            arrow.enabled = false;

        arrows[0].enabled = true;

        currentArrow = 0;
    }

    protected virtual void Update() {
        if (Input.GetKeyDown(up) && currentArrow > 0) {
            arrows[currentArrow].enabled = false;
            currentArrow--;
            arrows[currentArrow].enabled = true;
        }

        else if (Input.GetKeyDown(down) && currentArrow < arrows.Length - 1) {
            arrows[currentArrow].enabled = false;
            currentArrow++;
            arrows[currentArrow].enabled = true;
        }

        else if (Input.GetKeyDown(select))
            OptionSelected(arrows[currentArrow].transform.parent.name);

    }

    protected abstract void OptionSelected(string n);
}
