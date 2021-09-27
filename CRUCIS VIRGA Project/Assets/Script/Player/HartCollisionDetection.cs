using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HartCollisionDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject deadEffect;

    //SE
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();

    //死亡時のフラグ
    [System.NonSerialized]
    public bool dead;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        dead = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="Enemy")
        {
            dead = true;
            EffectIns(deadEffect,col.transform);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (col.tag == "EnemyBullet")
        {
            dead = true;
            EffectIns(deadEffect, col.transform);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    //エフェクト生成
    void EffectIns(GameObject obj,Transform pos)
    {
        Instantiate(obj, pos.position, Quaternion.identity);
        audioSource.PlayOneShot(audioClip[0]);
    }
}
