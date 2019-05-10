using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text scoreText;
    public Text levelText;
    public Text linesText;
    public GameObject nextPiece;

    private int score;
    private int level;
    private int lines;
    private int nextLevel;

    void Start() {
        scoreText.text = "000000";
        levelText.text = "000001";
        linesText.text = "000000";

        score = lines = 0;
        level = 1;
        nextLevel = 10;
    }

    public int Level {
        get { return level; }
    }

    public void AddLines(int n) {
        lines += n;

        SetToScreen(linesText, lines);

        AddScore(n);

        AddLevel();
    }

    private void AddScore(int lines) {
        if (lines == 1)      score += (level + 1) * 40;
        else if (lines == 2) score += (level + 1) * 100;
        else if (lines == 3) score += (level + 1) * 300;
        else if (lines == 4) score += (level + 1) * 1200;

        SetToScreen(scoreText, score);
    }

    private void AddLevel() {
        if (lines > nextLevel) {
            level++;

            nextLevel += (level * 10);

            SetToScreen(levelText, level);
        }
    }

    private void SetToScreen(Text text, int n) {
        if (n < 10)
            text.text = "00000" + n.ToString();

        else if (n < 100)
            text.text = "0000" + n.ToString();

        else if (n < 1000)
            text.text = "000" + n.ToString();

        else if (n < 10000)
            text.text = "00" + n.ToString();

        else if (n < 100000)
            text.text = "0" + n.ToString();

        else if (n < 100000)
            text.text = n.ToString();
    }
}
