using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionForTitleScene : MonoBehaviour
{
    //エフェクト
    [SerializeField]
    private GameObject deadEffect, laserPointEffect;

    //照準の表示非表示
    [SerializeField]
    private SpriteRenderer aimSp;

    //SE
    private AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    private Vector3 playPos;

    // Start is called before the first frame update
    void Start()
    {
        aimSp.enabled = false;
        audioSource = gameObject.AddComponent<AudioSource>();
        playPos.x = 0.0f;
        playPos.y = 0.0f;
        playPos.z = -10.0f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //プレイヤーの弾
        if (col.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(audioClip[1], playPos);
            EffectIns(deadEffect);
            Destroy(gameObject);
        }

        //レーザーポインター
        if (col.tag == "Laser")
        {
            audioSource.PlayOneShot(audioClip[0]);
            EffectIns(laserPointEffect);
            aimSp.enabled = true;
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
