using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarrier : MonoBehaviour
{
    private SpriteRenderer sp;
    [SerializeField]
    private GameObject barrierObj;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム上のバリアの残りHP状態(色)を連動させる
        if (barrierObj != null)
        {
            sp.color = barrierObj.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
