using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionChange : MonoBehaviour
{
    [System.NonSerialized]
    public int dir;
    private bool normal, diagonal;
    [SerializeField]
    private float angleSpeed;
    private Vector3 angle;
    // Start is called before the first frame update
    void Start()
    {
        dir = 0;
        normal = false;
        diagonal = false;
        angle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //右クリックをした時
        if (Input.GetMouseButtonDown(1))
        {    
            //0:正位置 1:斜め
            if (dir == 0)
            {
                diagonal = true;
            }
            else if (dir == 1)
            {
                normal = true;
            }
        }

        //角度が45度になるまで回転
        if (diagonal && !normal)
        {
            angle.z += angleSpeed;
            if (angle.z >= 45.0f)
            {
                angle.z = 45.0f;
                dir = 1;
                diagonal = false;
            }
        }

        //角度が0度になるまで回転
        if (normal && !diagonal)
        {
            angle.z -= angleSpeed;
            if (angle.z <= 0.0f)
            {
                angle.z = 0.0f;
                dir = 0;
                normal = false;
            }
        }

        transform.eulerAngles = angle;
    }
}
