using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrierType : MonoBehaviour, EnemyController
{
    private Vector3 pos;
    [SerializeField]
    private float speed;

    //バリア
    [SerializeField]
    private GameObject barrier;
    [SerializeField]
    private GameObject breakEffect;
    private bool barrierBreak;
    private int state;
    private float r, g, b;
    public GameObject BarrierObj() { return barrier; }

    // Start is called before the first frame update
    void Start()
    {
        r = 1.0f;
        g = 0.5f;
        b = 0.0f;
        barrier.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        pos = transform.position;
        barrierBreak = false;
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //向いている方向に移動
        pos += transform.up * speed * Time.deltaTime;
        transform.position = pos;

        if (!barrier && !barrierBreak && state == 0)
        {
            barrierBreak = true;
        }
        if (barrierBreak)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
            barrierBreak = false;
            state = 1;
        }
    }

    public int TypeNumber()
    {
        return 2;
    }
}
