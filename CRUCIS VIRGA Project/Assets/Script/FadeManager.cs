using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeManager : MonoBehaviour
{
    //フェードアウト処理の開始、完了フラグ
    private bool isFadeOut;

    //フェードイン処理の開始、完了フラグ
    private bool isFadeIn;

    //透明度が変わるスピード
    private float fadeSpeed;

    public Image fadeImage;
    float red, green, blue, alfa;

    //シーン遷移のため
    string afterScene;

    // Start is called before the first frame update
    void Start()
    {
        isFadeOut = false;
        isFadeIn = true;
        fadeSpeed = 0.5f;
        DontDestroyOnLoad(this);
        SetRGBA(0, 0, 0, 1);
        //シーン遷移が完了した際にフェードイン
        SceneManager.sceneLoaded += fadeInStart;
    }
    //シーン遷移が完了した際にフェードイン
    void fadeInStart(Scene scene, LoadSceneMode mode)
    {
        isFadeIn = true;
    }
    /// フェードアウト開始時の画像のRGBA値と次のシーン名を指定
    public void fadeOutStart(int red, int green, int blue, int alfa, string nextScene)
    {
        SetRGBA(red, green, blue, alfa);
        SetColor();
        isFadeOut = true;
        afterScene = nextScene;
    }
    // Update is called once per frame
    void Update()
    {
        if (isFadeIn == true)
        {
            //透明度を下げる
            alfa -= fadeSpeed * Time.deltaTime;
            //透明度を反映
            SetColor();
            if (alfa <= 0)
                isFadeIn = false;
        }
        if (isFadeOut == true)
        {
            //透明度を上げる
            alfa += fadeSpeed * Time.deltaTime;
            //変更した透明度を反映
            SetColor();
            if (alfa >= 1)
            {
                isFadeOut = false;
                SceneManager.LoadScene(afterScene);
            }
        }
    }

    void SetColor()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    public void SetRGBA(int r, int g, int b, int a)
    {
        red = r;
        green = g;
        blue = b;
        alfa = a;
    }
}
