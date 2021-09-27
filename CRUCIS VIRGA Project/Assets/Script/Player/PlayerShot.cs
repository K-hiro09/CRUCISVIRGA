using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseObj;
    private PlayerMouseSelect psMs;
    private GameObject target = null;
    private PlayerEachAction psEa;

    [SerializeField]
    private BulletController bulletObj;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private float shotAngleRange;
    [SerializeField]
    private int shotCount;

    //タイトルシーンのPlayer
    [SerializeField]
    private GameObject titlePlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        psMs = mouseObj.GetComponent<PlayerMouseSelect>();
        psEa = GetComponent<PlayerEachAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!titlePlayer) target = psMs.SelectPlayer();
        else target = titlePlayer.GetComponent<TitleScene>().TitlePlayerObj();

        //選択されているPlayer
        if (target != null)
        {
            if (psEa.shotFlg)
            {
                ShootNWay(target.transform.eulerAngles.z, shotAngleRange, shotSpeed, shotCount);
            }
            psEa.shotFlg = false;
        }

    }

    private void ShootNWay(float angleBase, float angleRange, float speed, int count)
    {
        var pos = target.transform.localPosition;
        var rot = target.transform.localRotation;

        //弾を複数発射するとき
        if (1 < count)
        {
            for (int i = 0; i < count; ++i)
            {
                //弾の発射角度
                var angle = angleBase + angleRange * ((float)i / (count - 1));

                var shot = Instantiate(bulletObj, pos, rot);

                shot.Init(angle, speed);
            }
        }
    }

}
