using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;
    [SerializeField] private TMP_Text scoreText;
    void Start()
    {
        score = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowScore()
    {
        scoreText.enabled = true;
    }

    public void HideScore()
    {
        scoreText.enabled = false;
    }

    public void addScoopPoints()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void addStackPoints()
    {
        score += 100;

        scoreText.text = "Score: " + score;
    }

    public void removeScoopPoints()
    {
        score -= 10;

        scoreText.text = "Score: " + score;
    }

    public void getFinalScore(int altScore)
    {
        TMP_Text finalText = FindObjectOfType<TMP_Text>();

        if(altScore == -999)
        {
            finalText.text = "FINAL SCORE: " + score + "\npress space to restart";
        }
        else
        {
            finalText.text = "FINAL SCORE: " + altScore + "\npress space to restart";
        }
    }
}
