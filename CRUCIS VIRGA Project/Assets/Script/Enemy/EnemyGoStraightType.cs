using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoStraightType : MonoBehaviour, EnemyController
{
    private Vector3 pos;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //向いている方向に移動
        pos += transform.up * speed * Time.deltaTime;
        transform.position = pos;
    }

    public int TypeNumber()
    {
        return 0;
    }

}
