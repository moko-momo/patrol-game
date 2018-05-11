using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder
{
    public Text scoreText;
	private int score = -1;


    public void resetScore()
    {
        score = -1;
    }

    public void addScore(int addscore)
    {
        score += addscore;
        scoreText.text = "Score:" + score;
    }

    public void setDisActive()
    {
        scoreText.text = "";
    }

    public void setActive()
    {
        scoreText.text = "Score:" + score;
    }
}

