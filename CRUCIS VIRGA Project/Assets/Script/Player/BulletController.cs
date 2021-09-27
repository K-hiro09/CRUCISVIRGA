using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 m_velocity; //速度

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += m_velocity;
    }

    public void Init(float angle, float speed)
    {
        //ベクトルに変換
        var direction = GetDirection(angle);

        m_velocity = direction * speed;

        //弾が正位置を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
    }

    //指定された角度（0～360)をベクトルに変換して返す
    public static Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }

    //画面外に行くと削除
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
