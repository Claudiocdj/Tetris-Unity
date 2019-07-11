using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public Text highScoreText;

    public Text score;

    public bool reset;

    private int highScore;

    public void Start() {
        if(reset)
            PlayerPrefs.SetInt("HighScore", 500);

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        SetToScreen(highScoreText, highScore);
    }

    public void Update() {
        if(score != null && int.Parse(score.text) > highScore) {
            highScoreText.color = Color.green;

            SetToScreen(highScoreText, int.Parse(score.text));
        }
    }

    private void OnDestroy() {
        PlayerPrefs.SetInt("HighScore", int.Parse(highScoreText.text));
    }

    private void SetToScreen(Text text, int n) {
        if (n < 10)
            text.text = "000000" + n.ToString();

        else if (n < 100)
            text.text = "00000" + n.ToString();

        else if (n < 1000)
            text.text = "0000" + n.ToString();

        else if (n < 10000)
            text.text = "000" + n.ToString();

        else if (n < 100000)
            text.text = "00" + n.ToString();

        else if (n < 1000000)
            text.text = "0" + n.ToString();

        else if (n < 10000000)
            text.text = n.ToString();
    }
}
