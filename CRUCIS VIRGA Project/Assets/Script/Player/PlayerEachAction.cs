using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEachAction : MonoBehaviour
{
    [SerializeField]
    private GameObject clickEffect, shotEffect;

    private GameObject target;
    private GameObject child;
    //private int enterTimer;
    private int clickTimer;

    private Vector3 scale;
    //元の大きさを保持
    private Vector3 normalScale;

    //SE
    private AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();

    //左クリック時
    [System.NonSerialized]
    public bool shotFlg;

    //クールタイムのフラグ、時間
    [SerializeField]
    private float coolTime;
    [System.NonSerialized]
    public bool coolFlg;
    private float coolTimer;

    //タイトル画面シーン遷移用のフラグ
    [System.NonSerialized]
    public bool shotTitleChange;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        //enterTimer = 0;
        clickTimer = 0;

        scale = transform.localScale;
        normalScale = scale;

        shotFlg = false;

        coolTimer = 0.0f;
        coolFlg = false;

        shotTitleChange = false;
    }

    //カーソルが範囲内に入ったとき
    public void MouseEnter()
    {
        if (!coolFlg)
        {
            if (clickTimer == 0)
            {
                clickTimer = 1;

                //クリック時のエフェクト生成
                Instantiate(clickEffect, this.transform.position, this.transform.rotation);
                audioSource.PlayOneShot(audioClip[1]);

                //拡大
                ExpansionScale(0.2f, 0.2f);

                //色を水色
                ColorChange(0.0f, 0.9f, 1.0f, 1.0f);
                //十字のコライダーをtrue
                LaserPointer(true, true);

                //カメラシェイク
                GameObject shake = GameObject.Find("Main Camera");
                bool flg = shake.GetComponent<ShakeCamera>().GetShakeFlg();
                if (!flg) shake.GetComponent<ShakeCamera>().CatchShake(0.05f, 0.05f, 0.05f, 0.05f);
            }
        }
    }

    //クリックした時
    public void MouseDown()
    {
        if (!coolFlg)
        {
            shotTitleChange = true;

            shotFlg = true;

            clickTimer = 0;

            //ショット時のエフェクト生成
            Instantiate(shotEffect, this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(audioClip[2]);

            //元の色に戻す(白)
            ColorChange(1.0f, 1.0f, 1.0f, 1.0f);

            //十字のレーザーポインタ描画しない
            //十字のコライダーをfalse
            LaserPointer(false, false);

            //拡大していたのを元の大きさに戻す
            NormalScale();

            //カメラシェイク
            GameObject shake = GameObject.Find("Main Camera");
            bool flg = shake.GetComponent<ShakeCamera>().GetShakeFlg();
            if (!flg) shake.GetComponent<ShakeCamera>().CatchShake(0.1f, 0.1f, 0.1f, 0.1f);
        }
    }

    //カーソルが離れた時
    public void MouseExit()
    {
        if (!coolFlg)
        {
            clickTimer = 0;

            //十字のレーザーポインタ描画しない
            //十字のコライダーをfalse
            LaserPointer(false, false);

            //拡大していたのを元の大きさに戻す
            NormalScale();

            //元の色に
            ColorChange(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    //クールタイム
    public void CoolTime()
    {
        coolFlg = true;
        ColorChange(1.0f, 1.0f, 1.0f, 0.5f);
        LaserPointer(false, false);
        coolTimer += Time.deltaTime;
        if (coolTimer >= coolTime)
        {
            //元の色に
            ColorChange(1.0f, 1.0f, 1.0f, 1.0f);
            coolTimer = 0.0f;
            coolFlg = false;
        }
    }

    //拡大
    void ExpansionScale(float addX, float addY)
    {
        scale.x += addX;
        scale.y += addY;
        this.transform.localScale = scale;
    }

    //元の大きさ
    void NormalScale()
    {
        scale.x = normalScale.x;
        scale.y = normalScale.y;
        this.transform.localScale = scale;
    }

    //色を変更
    void ColorChange(float r, float g, float b, float a)
    { this.GetComponent<SpriteRenderer>().color = new Color(r, g, b, a); }

    //レーザーポインターの描画、コライダー
    void LaserPointer(bool spriteFlg, bool coliderFlg)
    {
        //子供
        foreach (Transform childObj in this.transform)
        {
            childObj.GetComponent<SpriteRenderer>().enabled = spriteFlg;

            //孫
            foreach (Transform grandChildObj in childObj)
            {
                grandChildObj.GetComponent<BoxCollider2D>().enabled = coliderFlg;
            }
        }
    }

}
