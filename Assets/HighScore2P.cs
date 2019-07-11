using UnityEngine;
using UnityEngine.UI;

public class HighScore2P : MonoBehaviour {

    public Text highScoreText;

    public Text scoreP1;

    public Text scoreP2;

    private int highScore;

    public void Start() {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.enabled = false;
    }

    public void Update() {
        if (int.Parse(scoreP1.text) >= highScore || int.Parse(scoreP2.text) >= highScore) {
            highScoreText.enabled = true;

            if (int.Parse(scoreP1.text) >= highScore)
                highScore = int.Parse(scoreP1.text);

            else
                highScore = int.Parse(scoreP2.text);
        }
    }

    private void OnDestroy() {
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}
