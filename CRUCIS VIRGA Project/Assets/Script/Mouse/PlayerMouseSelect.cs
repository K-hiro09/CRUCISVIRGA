using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseSelect : MonoBehaviour
{
    //マウスの位置
    private Vector3 mousePos;
    private Vector3 cameraPos;

    //Player上
    private Vector3 upP1, upP2, upP3;
    [SerializeField]
    private GameObject upPlayer;
    [SerializeField]
    private SpriteRenderer upTriangle;

    //Player右
    private Vector3 rightP1, rightP2, rightP3;
    [SerializeField]
    private GameObject rightPlayer;
    [SerializeField]
    private SpriteRenderer rightTriangle;

    //Player下
    private Vector3 downP1, downP2, downP3;
    [SerializeField]
    private GameObject downPlayer;
    [SerializeField]
    private SpriteRenderer downTriangle;

    //Player左
    private Vector3 leftP1, leftP2, leftP3;
    [SerializeField]
    private GameObject leftPlayer;
    [SerializeField]
    private SpriteRenderer leftTriangle;

    //どのPlayerObjが選択されているか取得
    private GameObject obj;
    public GameObject SelectPlayer() { return obj; }

    //チュートリアル
    private bool tFlg;
    public bool GetTurorialCheck() { return tFlg; }
    private void SetTutorialCheck(bool f) { tFlg = f; }

    // Start is called before the first frame update
    void Start()
    {
        #region 各三角形の頂点指定
        //上三角形の三点
        upP1.x = -5.4f;
        upP1.y = 0.8f;
        upP2.x = -22.2f;
        upP2.y = 12.9f;
        upP3.x = 11.2f;
        upP3.y = 12.9f;

        //右三角形の三点
        rightP1.x = -4.4f;
        rightP1.y = 0.0f;
        rightP2.x = 12.0f;
        rightP2.y = 12.8f;
        rightP3.x = 11.8f;
        rightP3.y = -12.2f;

        //下三角形の三点
        downP1.x = -5.4f;
        downP1.y = -1.0f;
        downP2.x = 11.4f;
        downP2.y = -12.9f;
        downP3.x = -22.3f;
        downP3.y = -13.0f;

        //左三角形の三点
        leftP1.x = -6.6f;
        leftP1.y = -0.1f;
        leftP2.x = -22.8f;
        leftP2.y = -12.0f;
        leftP3.x = -23.1f;
        leftP3.y = 12.4f;
        #endregion

        //各Playerの下にある三角形のSprite
        upTriangle.enabled = false;
        rightTriangle.enabled = false;
        downTriangle.enabled = false;
        leftTriangle.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //マウスカーソルの位置をワールド座標として取得
        cameraPos = Input.mousePosition;
        cameraPos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(cameraPos);

        UpPlayer();
        RightPlayer();
        DownPlayer();
        LeftPlayer();
    }

    void UpPlayer()
    {
        //マウスカーソルが上のPlayer三角形の内側なら
        if (PointInTriangle(mousePos, upP1, upP2, upP3))
        {
            upTriangle.enabled = true;

            obj = upPlayer;
            
            upPlayer.GetComponent<PlayerEachAction>().MouseEnter();
            if (Input.GetMouseButtonDown(0))
            {
                upPlayer.GetComponent<PlayerEachAction>().MouseDown();
                upPlayer.GetComponent<PlayerEachAction>().coolFlg = true;
            }
        }
        //マウスカーソルが範囲外
        else
        {
            upTriangle.enabled = false;
            upPlayer.GetComponent<PlayerEachAction>().MouseExit();
        }

        if (upPlayer.GetComponent<PlayerEachAction>().coolFlg)
        {
            upPlayer.GetComponent<PlayerEachAction>().CoolTime();
        }

    }

    void RightPlayer()
    {
        //マウスカーソルが右のPlayer三角形の内側なら
        if (PointInTriangle(mousePos, rightP1, rightP2, rightP3))
        {
            rightTriangle.enabled = true;

            obj = rightPlayer;

            rightPlayer.GetComponent<PlayerEachAction>().MouseEnter();
            if (Input.GetMouseButtonDown(0))
            {
                rightPlayer.GetComponent<PlayerEachAction>().MouseDown();
                rightPlayer.GetComponent<PlayerEachAction>().coolFlg = true;
            }
        }
        //マウスカーソルが範囲外
        else
        {
            rightTriangle.enabled = false;
            rightPlayer.GetComponent<PlayerEachAction>().MouseExit();
        }

        if (rightPlayer.GetComponent<PlayerEachAction>().coolFlg)
        {
            rightPlayer.GetComponent<PlayerEachAction>().CoolTime();
        }
    }

    void DownPlayer()
    {
        //マウスカーソルが下のPlayer三角形の内側なら
        if (PointInTriangle(mousePos, downP1, downP2, downP3))
        {
            downTriangle.enabled = true;

            obj = downPlayer;

            downPlayer.GetComponent<PlayerEachAction>().MouseEnter();

            SetTutorialCheck(true);

            if (Input.GetMouseButtonDown(0))
            {
                downPlayer.GetComponent<PlayerEachAction>().MouseDown();
                downPlayer.GetComponent<PlayerEachAction>().coolFlg = true;
            }
        }
        //マウスカーソルが範囲外
        else
        {
            downTriangle.enabled = false;
            downPlayer.GetComponent<PlayerEachAction>().MouseExit();
            SetTutorialCheck(false);
        }

        if (downPlayer.GetComponent<PlayerEachAction>().coolFlg)
        {
            downPlayer.GetComponent<PlayerEachAction>().CoolTime();
        }
    }

    void LeftPlayer()
    {

        //マウスカーソルが下のPlayer三角形の内側なら
        if (PointInTriangle(mousePos, leftP1, leftP2, leftP3))
        {
            leftTriangle.enabled = true;

            obj = leftPlayer;

            leftPlayer.GetComponent<PlayerEachAction>().MouseEnter();
            if (Input.GetMouseButtonDown(0))
            {
                leftPlayer.GetComponent<PlayerEachAction>().MouseDown();
                leftPlayer.GetComponent<PlayerEachAction>().coolFlg = true;
            }
        }
        //マウスカーソルが範囲外
        else
        {
            leftTriangle.enabled = false;
            leftPlayer.GetComponent<PlayerEachAction>().MouseExit();
        }

        if (leftPlayer.GetComponent<PlayerEachAction>().coolFlg)
        {
            leftPlayer.GetComponent<PlayerEachAction>().CoolTime();
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
