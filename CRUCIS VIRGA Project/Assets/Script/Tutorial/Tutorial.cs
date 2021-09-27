using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //Sprite点滅
    [SerializeField]
    private SpriteRenderer guide;
    private float displayTimer, hideTimer;
    [SerializeField]
    private float displayTime, hideTime;

    //HereSprite,GuideOkSprite,ClickSprite
    [SerializeField]
    private SpriteRenderer guideHere, guideOk, clickSp;

    //敵
    [SerializeField]
    private SpriteRenderer[] enemy;
    private bool enemyIn;

    //マウスカーソルが範囲内にあるか否か
    private GameObject pmS;


    // Start is called before the first frame update
    void Start()
    {
        guide.enabled = true;
        guideHere.enabled = true;
        guideOk.enabled = false;
        clickSp.enabled = false;
        displayTimer = 0.0f;
        hideTimer = 0.0f;

        pmS = GameObject.Find("Mouse");
    }

    // Update is called once per frame
    void Update()
    {
        //カーソルが範囲内に無ければGuideSprite点滅
        if (!pmS.GetComponent<PlayerMouseSelect>().GetTurorialCheck())
        {
            //HereSprite表示
            guideHere.enabled = true;

            if (guide.enabled) hideTimer += Time.deltaTime;
            if (hideTimer >= hideTime)
            {
                guide.enabled = false;
                hideTimer = 0.0f;
            }

            else if (!guide.enabled)
            {
                displayTimer += Time.deltaTime;
                if (displayTimer >= displayTime)
                {
                    guide.enabled = true;
                    displayTimer = 0.0f;
                }
            }
            guideOk.enabled = false;
        }

        //カーソルが範囲内に有れば
        else if (pmS.GetComponent<PlayerMouseSelect>().GetTurorialCheck() && !enemyIn)
        {
            hideTimer = 0.0f;
            displayTimer = 0.0f;
            guide.enabled = false;
            guideHere.enabled = false;

            //OkSprite表示
            guideOk.enabled = true;
        }

        EnemyIsVisible();
    }

    //敵画面内判定
    void EnemyIsVisible()
    {
        //敵が空ではないとき
        if (enemy != null)
        {
            foreach (var ene in enemy)
            {
                if (ene == null)
                {
                    //自身と自身の子を削除
                    foreach (Transform obj in this.transform)
                    {
                        GameObject.Destroy(obj.gameObject);
                    }
                    Destroy(this.gameObject);
                    return;
                }
            }
        }

        //4体の敵が画面内に入ったとき
        if (enemy[0].isVisible && enemy[1].isVisible &&
            enemy[2].isVisible && enemy[3].isVisible)
        {
            enemyIn = true;
        }

        //カーソルが指定範囲内にあるとき
        if (enemyIn && pmS.GetComponent<PlayerMouseSelect>().GetTurorialCheck())
        {
            guideOk.enabled = false;
            guideHere.enabled = false;
            clickSp.enabled = true;
        }

        else if (enemyIn && !pmS.GetComponent<PlayerMouseSelect>().GetTurorialCheck())
        {
            guideHere.enabled = true;
            clickSp.enabled = false;
        }

    }

}
