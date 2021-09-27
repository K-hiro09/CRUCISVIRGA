using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static int scoreNum;
    private static int highScore;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Emmiter emObj;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0;
    }

    public void ScorePlus(int num)
    {
        scoreNum += num;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE:" + scoreNum.ToString();
    }

    //ハイスコア
    public static int HightScore()
    {
        if (scoreNum >= highScore)
        {
            highScore = scoreNum;
        }
        return highScore;
    }

    //終了時のスコア
    public static int NowScore()
    {
        return scoreNum;
    }
}
