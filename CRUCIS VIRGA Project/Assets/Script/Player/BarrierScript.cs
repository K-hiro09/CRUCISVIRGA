using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    //敵に8回当たると破壊
    private int count;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        count = 8;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CountColorChange();
    }

    public void CountSubtraction(int num)
    {
        count -= num;
    }

    //バリアの色
    void CountColorChange()
    {
        //緑→黄色→オレンジ→赤→消滅
        if (count >= 7)
        {
            ColorChange(0.0f, 1.0f, 0.0f);
        }

        else if (count >= 5)
        {
            ColorChange(1.0f, 0.9f, 0.0f);
        }
        else if (count >= 3)
        {
            ColorChange(1.0f, 0.5f, 0.0f);

        }
        else if (count >= 1)
        {
            ColorChange(1.0f, 0.0f, 0.0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ColorChange(float r,float g,float b)
    {
        sp.color = new Color(r, g, b);
    }
}
