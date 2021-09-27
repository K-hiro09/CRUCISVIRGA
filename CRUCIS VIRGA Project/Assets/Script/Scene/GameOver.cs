using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //画面中央のハート
    [SerializeField]
    private HartCollisionDetection hart;
    [SerializeField]
    private FadeManager fMg;

    //ゲームオーバーのスプライト
    [SerializeField]
    private SpriteRenderer gmOver;

    void Start()
    {
        gmOver.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ハートが破壊されたら
        if (hart.dead)
        {
            //ゲームオーバーのスプライト表示
            gmOver.enabled = true;
            //同じシーンを呼ぶ
            fMg.fadeOutStart(0, 0, 0, 0, "Score");
            hart.dead = false;
        }

    }
}
