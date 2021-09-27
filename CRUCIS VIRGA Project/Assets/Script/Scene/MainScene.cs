using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //ESCキーで終了
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
