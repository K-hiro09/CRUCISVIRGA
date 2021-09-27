using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトル画面でのシーン遷移
public class TitleScene : MonoBehaviour
{
    //マウスの位置
    private Vector3 mousePos;
    private Vector3 cameraPos;
    private Vector3 p1, p2, p3;
    [SerializeField]
    private PlayerEachAction pEa;
    [SerializeField]
    private FadeManager fMg;
    private GameObject obj;
    [SerializeField]
    private SpriteRenderer guide, clickFont,trFrame;
    [SerializeField]
    private GameObject chFont;
    public GameObject TitlePlayerObj() { return obj; }

    void Start()
    {
        obj = this.gameObject;

        //三角形の三点
        p1.x = 0.1f;
        p1.y = -2.5f;
        p2.x = -8.2f;
        p2.y = 5.2f;
        p3.x = 8.2f;
        p3.y = 4.9f;

        //Sprite表示非表示
        guide.enabled = false;
        chFont.SetActive(true);
        clickFont.enabled = false;
        trFrame.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ESCキーで終了
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //マウスカーソルの位置をワールド座標として取得
        cameraPos = Input.mousePosition;
        cameraPos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(cameraPos);

        //マウスカーソルが上のPlayer三角形の内側なら
        if (PointInTriangle(mousePos, p1, p2, p3))
        {
            guide.enabled = true;
            chFont.SetActive(false);
            clickFont.enabled = true;
            trFrame.enabled = false;

            pEa.GetComponent<PlayerEachAction>().MouseEnter();
            if (Input.GetMouseButtonDown(0))
            {
                pEa.GetComponent<PlayerEachAction>().MouseDown();
                pEa.GetComponent<PlayerEachAction>().coolFlg = true;
            }
        }
        //マウスカーソルが範囲外
        else
        {
            guide.enabled = false;
            clickFont.enabled = false;
            chFont.SetActive(true);
            trFrame.enabled = true;

            pEa.GetComponent<PlayerEachAction>().MouseExit();
        }

        if (pEa.GetComponent<PlayerEachAction>().coolFlg)
        {
            chFont.SetActive(false);
            clickFont.enabled = false;
            trFrame.enabled = false;

            pEa.GetComponent<PlayerEachAction>().CoolTime();
        }

        //弾を撃てばメインシーンへ
        if (pEa.shotFlg)
        {
            fMg.fadeOutStart(0, 0, 0, 0, "Main");
        }
    }

    //外積
    float Sign(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    //マウスカーソルが三角形の内側にあるかどうか
    bool PointInTriangle(Vector3 pt, Vector3 v1, Vector3 v2, Vector3 v3)
    {
        bool b1, b2, b3;

        b1 = Sign(pt, v1, v2) < 0.0f;
        b2 = Sign(pt, v2, v3) < 0.0f;
        b3 = Sign(pt, v3, v1) < 0.0f;

        return ((b1 == b2) && (b2 == b3));
    }

}
