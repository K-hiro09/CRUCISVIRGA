using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColor : MonoBehaviour
{
    private SpriteRenderer sp;
    [SerializeField]
    Emmiter em;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ColorChange(0.0f, 0.9f, 1.0f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Wave数に応じて背景の色を変える
        if (em.CurrentWaveNum() == 11)
        {
            ColorChange(1.0f, 0.0f, 0.9f, 0.1f);
        }
        else if (em.CurrentWaveNum() == 21)
        {
            ColorChange(1.0f, 0.0f, 0.0f, 0.1f);
        }
    }

    void ColorChange(float r, float g, float b,float a)
    {
        sp.color = new Color(r, g, b, a);
    }
}
