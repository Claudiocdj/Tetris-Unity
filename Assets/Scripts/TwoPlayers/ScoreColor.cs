using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreColor : MonoBehaviour {

    public Text p1Score;
    public Text p2Score;
    
    void Update() {
        if(int.Parse(p1Score.text) == int.Parse(p2Score.text)) {
            p1Score.color = Color.white;
            p2Score.color = Color.white;
        }
        else if (int.Parse(p1Score.text) > int.Parse(p2Score.text)) {
            p1Score.color = Color.green;
            p2Score.color = Color.red;
        }
        else if (int.Parse(p1Score.text) < int.Parse(p2Score.text)) {
            p1Score.color = Color.red;
            p2Score.color = Color.green;
        }
    }
}
