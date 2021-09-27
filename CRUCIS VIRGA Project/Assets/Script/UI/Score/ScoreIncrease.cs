using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIncrease : MonoBehaviour
{
    //スコア加算されるときの倍率(敵が死んだ数)
    private int count;
    private int countSave;
    private bool countFlg;
    private float interval;

    [SerializeField]
    private Score score;

    //スコアの加算倍率
    [SerializeField]
    private Text text;
    private int size;
    private float r, g, b, textAlpha;

    //スコアの加算倍率のテキスト
    [SerializeField]
    private SpriteRenderer textUnder;
    private Vector3 textUscale;
    private Vector3 textUnormalScale;

    //敵の種類
    private int type;

    //SE
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        countSave = 0;
        countFlg = false;
        interval = 0.0f;

        text.enabled = false;
        textUnder.enabled = false;
        size = text.fontSize;

        r = text.color.r;
        g = text.color.g;
        b = text.color.b;
        textAlpha = text.color.a;

        textUscale = textUnder.transform.localScale;
        textUnormalScale = textUscale;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform parent in this.transform)
        {
            foreach (Transform enemyObj in parent)
            {
                if (enemyObj.tag != "Enemy") break;

                var enemy = enemyObj.GetComponent<EnemyCollisionDetection>();
                //敵の種類
                var type = enemyObj.GetComponent<EnemyController>().TypeNumber();

                //Playerの弾に当たったとき
                if (enemy.hit)
                {
                    audioSource.PlayOneShot(audioClip[0]);
                    this.type = type;
                    count++;
                    enemy.hit = false;
                    Destroy(enemy.gameObject);
                }

                //敵が死んだ数によって加算スコアの倍率を変える
                switch (count)
                {
                    case 1:
                        interval += Time.deltaTime;
                        if (interval >= 0.3f)
                        {
                            EnemyScoreChange(this.type);
                        }
                        break;

                    case 2:
                        interval += Time.deltaTime;
                        if (interval >= 0.3f)
                        {
                            EnemyScoreChange(this.type);
                        }
                        break;

                    case 3:
                        interval += Time.deltaTime;
                        if (interval >= 0.3f)
                        {
                            EnemyScoreChange(this.type);
                        }
                        break;

                    case 4:
                        interval += Time.deltaTime;
                        if (interval >= 0.3f)
                        {
                            EnemyScoreChange(this.type);
                        }
                        break;
                }

            }
        }
        if (countFlg) ScoreMagnification();
    }

    //敵の種類ごとにスコアが加算される量を変更
    void EnemyScoreChange(int type)
    {
        switch (type)
        {
            //直進のみの敵
            case 0:
                score.ScorePlus((300 * count) * count);
                break;
            //爆破の敵
            case 1:
                score.ScorePlus((500 * count) * count);
                break;
            //バリアの敵
            case 2:
                score.ScorePlus((1000 * count) * count);
                break;
            //ショットの敵
            case 3:
                score.ScorePlus((750 * count) * count);
                break;
        }
        countSave = count;
        countFlg = true;
        count = 0;
        interval = 0.0f;
    }

    //スコア加算量倍率のText
    void ScoreMagnification()
    {
        text.enabled = true;
        textUnder.enabled = true;

        //テキスト拡大
        text.fontSize += 3;
        textUscale.x += 0.2f;
        textUscale.y += 0.2f;
        textUnder.transform.localScale = textUscale;

        //テキスト透明度
        textAlpha -= 0.05f;
        text.color = new Color(r, g, b, textAlpha);
        textUnder.color = new Color(1.0f, 1.0f, 1.0f, textAlpha);

        text.text = "SCORE×" + countSave.ToString();

        //テキスト透明度が0以下になったら
        if (textAlpha <= 0.0f)
        {
            countSave = 0;

            text.enabled = false;
            textUnder.enabled = false;

            textAlpha = 1.0f;
            text.fontSize = size;
            textUscale = textUnormalScale;

            countFlg = false;
        }

    }
}
