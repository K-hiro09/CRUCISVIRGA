using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScaleAlpha : MonoBehaviour
{
    private Vector3 scale;
    [SerializeField]
    private float scaleXAdd, scaleYAdd, alphaMinus = 0.1f;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //拡縮しながら透明度を下げ0になると削除
        scale.x += scaleXAdd;
        scale.y += scaleYAdd;
        transform.localScale = scale;

        alpha -= alphaMinus;
        SpriteRenderer sp = this.GetComponent<SpriteRenderer>();
        sp.color = new Color(1.0f, 1.0f, 1.0f, alpha);

        if (alpha <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
