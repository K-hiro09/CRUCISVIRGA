using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScene : MonoBehaviour
{
    [SerializeField]
    Text scoreText, highScoreText;

    private bool check;

    [SerializeField]
    private FadeManager fMg;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ESCキーで終了
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        highScoreText.text = "HighScore:" + Score.HightScore().ToString();
        scoreText.text = "Score:" + Score.NowScore().ToString();

        if (Input.GetMouseButtonDown(0) && !check)
        {
            check = true;
            fMg.fadeOutStart(0, 0, 0, 0, "Title");
        }

    }
}
