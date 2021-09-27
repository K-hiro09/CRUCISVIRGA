using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstructionLine : MonoBehaviour
{
    //敵出現位置の2本のライン
    [SerializeField]
    private GameObject verticalLine, besideLine;
    //上下左右の敵
    [SerializeField]
    private GameObject[] verticalE, besideE;
    //上下左右の敵の数
    private int num;

    // Start is called before the first frame update
    void Start()
    {
        num = 2;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < num; i++)
        {
            //縦方向の敵が全滅
            if (!verticalE[i])
            {
                Destroy(verticalLine);
            }
            //横方向の敵が全滅
            if (!besideE[i])
            {
                Destroy(besideLine);
            }
        }

        //縦横2本のラインが消えたら
        if (!verticalLine && !besideLine)
        {
            Destroy(gameObject);
        }
    }
}
