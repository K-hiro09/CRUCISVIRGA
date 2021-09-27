using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float speed;
    private EnemyShotType esType;

    void Start()
    {
        esType = GetComponent<EnemyShotType>();
    }

    // Update is called once per frame
    void Update()
    {
        if (esType.shot)
        {
            GameObject enemyBullet = GameObject.Instantiate(bullet) as GameObject;
            enemyBullet.GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
            enemyBullet.transform.position = transform.position;
        }
    }

}
