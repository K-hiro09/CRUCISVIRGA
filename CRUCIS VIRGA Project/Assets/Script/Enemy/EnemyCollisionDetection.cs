using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDetection : MonoBehaviour
{
    //エフェクト
    [SerializeField]
    private GameObject deadEffect, laserPointEffect, scoreEffect;

    //照準の表示非表示
    [SerializeField]
    private SpriteRenderer aimSp;

    //SE
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    //再生位置
    private Vector3 playPos;

    //画面内に入ったかどうか
    private bool invasion;

    //Playerの弾の当たり判定
    [System.NonSerialized]
    public bool hit;

    [System.NonSerialized]
    public bool shotBodyHit;

    //敵の種類
    private int enemyType;

    // Start is called before the first frame update
    void Start()
    {
        aimSp.enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();

        invasion = true;
        enemyType = this.GetComponent<EnemyController>().TypeNumber();

        playPos.x = -5.0f;
        playPos.y = 0.0f;
        playPos.z = -10.0f;
    }

    //カメラ内に入ったとき
    private void OnBecameVisible()
    {
        if (invasion)
        {
            audioSource.PlayOneShot(audioClip[1]);
            invasion = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //プレイヤーの弾
        if (col.tag == "Bullet")
        {
            Destroy(col.gameObject);

            switch (enemyType)
            {
                //直進の敵
                case 0:
                    this.hit = true;
                    EffectIns(deadEffect);
                    EffectIns(scoreEffect);
                    break;

               //爆破の敵
                case 1:
                    this.hit = true;
                    EffectIns(deadEffect);
                    EffectIns(scoreEffect);
                    break;

                //バリアの敵
                case 2:
                    //バリアが破壊されたら本体にPlayerの弾が当たる
                    var barrier = this.GetComponent<EnemyBarrierType>().BarrierObj();
                    if (barrier != null)
                    {
                        audioSource.PlayOneShot(audioClip[2]);
                        audioSource.PlayOneShot(audioClip[3]);
                        //EffectIns(deadEffect);
                        Destroy(barrier);
                    }

                    else if (barrier == null)
                    {
                        this.hit = true;
                        EffectIns(deadEffect);
                        EffectIns(scoreEffect);
                    }
                    break;

                //ショットの敵
                case 3:
                    this.hit = true;
                    shotBodyHit = true;
                    EffectIns(scoreEffect);
                    EffectIns(deadEffect);
                    break;
            }

        }

        //レーザーポインター
        if (col.tag == "Laser")
        {
            audioSource.PlayOneShot(audioClip[0]);
            EffectIns(laserPointEffect);
            aimSp.enabled = true;
        }

        //バリア
        if (col.tag == "Barrier")
        {
            AudioSource.PlayClipAtPoint(audioClip[2], playPos);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Laser")
        {
            aimSp.enabled = false;
        }
    }

    //エフェクト生成
    void EffectIns(GameObject obj)
    {
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
