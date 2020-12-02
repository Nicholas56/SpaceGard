using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//http://dreamlo.com/lb/2OBb8lRN1EWxpSF3eDQEZQG0WE2afUi0a9ekCT6oBRSQ
//2OBb8lRN1EWxpSF3eDQEZQG0WE2afUi0a9ekCT6oBRSQ
//5fb40ce1eb371a09c42a8b03

public class Score : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreTxt;

    private void Awake()
    {
        InvokeRepeating("ShowScore", 1, 1);
    }

    void ShowScore()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void RaiseScore()
    {
        score++;
    }

    public void EndSession()
    {
        //What happens at gameover
    }
}
