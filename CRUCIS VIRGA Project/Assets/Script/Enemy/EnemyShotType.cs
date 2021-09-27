using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotType : MonoBehaviour, EnemyController
{
    //画面内に入ったかどうか
    private bool invasion;
    [SerializeField]
    private float invasionTime;
    private float invasionTimer;

    //撃つまでの時間
    private bool shotCharge;
    [System.NonSerialized]
    public bool shot;
    [SerializeField]
    private float shotTime;
    private float shotTimer;

    //再度撃つまでの時間
    [SerializeField]
    private float waitTime;
    private float waitTimer;

    //本体の下にある赤丸の影
    [SerializeField]
    private GameObject underObj;
    private Vector3 scale;
    private Vector3 normalScale;
    [SerializeField]
    private float scaleRate;

    [SerializeField]
    private float speed;

    private Vector3 pos;

    //SE
    private AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();

    private enum STATE
    {
        MOVE,
        SHOT_CHARGE,
        SHOT,
        WAIT
    }
    private STATE state;

    // Start is called before the first frame update
    void Start()
    {
        invasion = false;
        invasionTimer = 0.0f;

        shotTimer = 0.0f;

        waitTimer = 0.0f;

        scale = underObj.transform.localScale;
        normalScale = scale;

        pos = transform.position;

        state = STATE.MOVE;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    //カメラ内に入ったとき
    private void OnBecameVisible()
    {
        invasion = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.MOVE:
                Move(invasion, Time.deltaTime);
                break;

            case STATE.SHOT_CHARGE:
                ShotCharge(shotCharge, Time.deltaTime);
                break;

            case STATE.SHOT:
                Shot();
                break;

            case STATE.WAIT:
                Wait(Time.deltaTime);
                break;
        }
        underObj.transform.localScale = scale;
    }

    //画面内に入ってから一定時間まで移動
    void Move(bool moveFlg, float deltaTime)
    {
        if (moveFlg) invasionTimer += deltaTime;

        if (invasionTimer <= invasionTime)
        {
            //向いている方向に移動
            pos += transform.up * speed * deltaTime;
            transform.position = pos;
        }

        //ショット準備
        else if (invasionTimer >= invasionTime)
        {
            moveFlg = false;
            invasionTimer = 0.0f;
            shotCharge = true;
            state = STATE.SHOT_CHARGE;
        }
    }

    //撃つまでの時間
    void ShotCharge(bool chrge, float deltaTime)
    {
        if (chrge)
        {
            shotTimer += deltaTime;
            if (shotTimer <= shotTime)
            {
                scale.x += scaleRate * deltaTime;
                scale.y += scaleRate * deltaTime;
            }
            else if (shotTimer >= shotTime)
            {
                scale = normalScale;
                shotTimer = 0.0f;
                state = STATE.SHOT;
            }
        }
    }

    void Shot()
    {
        shot = true;
        audioSource.PlayOneShot(audioClip[0]);
        state = STATE.WAIT;
    }

    //再度撃つまでの待ち時間
    void Wait(float deltaTime)
    {
        shot = false;
        waitTimer += deltaTime;
        if (waitTimer >= waitTime)
        {
            state = STATE.SHOT_CHARGE;
            waitTimer = 0.0f;
        }

    }


    public int TypeNumber()
    {
        return 3;
    }

}
