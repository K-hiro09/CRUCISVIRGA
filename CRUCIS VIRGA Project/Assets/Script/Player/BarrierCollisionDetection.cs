using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollisionDetection : MonoBehaviour
{
    private BarrierScript barrier;

    //エフェクト
    [SerializeField]
    private GameObject deadEffect;

    void Start()
    {
        barrier = GetComponent<BarrierScript>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //敵
        if (col.tag == "Enemy")
        {
            var type = col.GetComponent<EnemyController>().TypeNumber();

            switch (type)
            {
                //直進の敵
                case 0:
                    barrier.CountSubtraction(1);
                    Dead(col.transform, col.gameObject);
                    break;
                //爆発する敵
                case 1:
                    barrier.CountSubtraction(2);
                    Dead(col.transform, col.gameObject);
                    break;
                //バリアの敵
                case 2:
                    barrier.CountSubtraction(1);
                    Dead(col.transform, col.gameObject);
                    break;
                //ショットの敵
                case 3:
                    barrier.CountSubtraction(1);
                    Dead(col.transform, col.gameObject);
                    break;
            }
        }

        //敵の弾
        if (col.tag == "EnemyBullet")
        {
            barrier.CountSubtraction(1);
            Dead(col.transform, col.gameObject);
        }
    }
    void EffectIns(GameObject obj, Transform pos)
    {
        Instantiate(obj, pos.position, Quaternion.identity);
    }
    //死亡時
    void Dead(Transform pos, GameObject obj)
    {
        //カメラシェイク
        GameObject shake = GameObject.Find("Main Camera");
        bool flg = shake.GetComponent<ShakeCamera>().GetShakeFlg();
        if (!flg) shake.GetComponent<ShakeCamera>().CatchShake(0.2f, 0.2f, 0.2f, 0.2f);
        EffectIns(deadEffect, pos);
        Destroy(obj);
    }

}
