using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject deleteEffect;
    // 変数
    private AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    //再生位置
    private Vector3 playPos;

    private EnemyShotType esType;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        playPos.x = 0.0f;
        playPos.y = 0.0f;
        playPos.z = -10.0f;
    }

    void Update()
    {
        //ショット本体が死ぬと弾も消す
        if (FindObjectOfType<EnemyCollisionDetection>().shotBodyHit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //プレイヤーの弾
        if (col.tag == "Bullet")
        {
            Instantiate(deleteEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(audioClip[0], playPos);
        }
        //バリア
        if (col.tag == "Barrier")
        {
            Instantiate(deleteEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(audioClip[0], playPos);
        }
    }
}
