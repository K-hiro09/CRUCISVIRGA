using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmiter : MonoBehaviour
{
    //プレハブWaveを格納
    public GameObject[] waves;
    //現在の敵のインデックス
    private int currentWave;
    [SerializeField]
    private FadeManager fMg;
    public int CurrentWaveNum() { return currentWave; }

    IEnumerator Start()
    {
        //配列が空であれば（Waveが存在しなければ）
        if (waves.Length == 0)
        {
            yield break;
        }

        while (true)
        {
            //配列に格納されたプレハブからインスタンスを生成してwaveに格納
            GameObject wave = (GameObject)Instantiate(waves[currentWave],
                transform.position, Quaternion.identity);

            //敵機waveを子要素にする
            wave.transform.parent = transform;

            //子要素のwaveの数が0でなければ
            while (wave.transform.childCount != 0)
            {
                //削除されるまで待機
                yield return new WaitForEndOfFrame();
            }

            Destroy(wave);

            if (currentWave == waves.Length - 1)
            {
                fMg.fadeOutStart(0, 0, 0, 0, "Score");
                yield break;
            }

            //現在の敵機のインデックスに加算し、配列に格納された数以上になれば
            if (waves.Length <= ++currentWave)
            {
                currentWave = 0;
            }
        }
    }
}
