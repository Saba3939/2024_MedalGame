using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Horce : MonoBehaviour
{
    public float speed = 0.005f; //スピード
    public int Horsenumber = 1;//馬の番号
    public enum Horseability
    {
        NIGE,
        OIKOMI,
        SENKOU,
        SASHI
    };
    public Horseability MyAbility;
    public float abilityValue = 0;//馬の能力値
    public GameObject goal = null;//ゴールオブジェクトを取得
    public GameObject myGameManager = null;//ゲームマネージャーを取得
    private float goalpos = 60f;//ゴールポジション
    private GameManager gameManager;//ゲームマネージャーコンポーネントを取得
    private float[,] NigeSpeed = new float[4, 2]{
        {0.005f,0.01f},
        {0.01f,0.015f},
        {0.015f,0.02f},
        {0.02f,0.025f}
    };
    private float[,] OikomiSpeed = new float[4, 2]{
        {0.01f,0.015f},
        {0.015f,0.02f},
        {0.02f,0.025f},
        {0.025f,0.03f}
    };
    private float[,] SenkouSpeed = new float[4, 2]{
        {0.015f,0.02f},
        {0.02f,0.025f},
        {0.025f,0.03f},
        {0.03f,0.035f}
    };
    private float[,] SasiSpeed = new float[4, 2]{
        {0.02f,0.025f},
        {0.025f,0.03f},
        {0.03f,0.035f},
        {0.035f,0.04f}
    };
    private List<float[,]> Speeds;
    void Start()
    {
        abilityValue = Random.Range(4.3f, 4.5f);//馬の能力値
        goalpos = goal.transform.position.x;//ゴールポジションを更新
        gameManager = myGameManager.GetComponent<GameManager>();//ゲームマネージャーコンポーネントを取得
        MyAbility = (Horseability)Enum.ToObject(typeof(Horseability), Random.Range(0, 3));//馬の特性をランダムで決定
        Speeds = new List<float[,]>() { NigeSpeed, OikomiSpeed, SenkouSpeed, SasiSpeed };
        StartCoroutine(DelayRepeatingSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;//posを今のポジションにする
        if (gameManager.start == false) speed = 0;//まだスタートしていないならスピードを０に
        pos.x += speed * abilityValue * Time.deltaTime;//スピードと能力値をかけあわせたものをx座標にプラスする
        transform.position = pos;//今のポジションをposにする
    }
    private IEnumerator DelayRepeatingSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            HorseMove(Speeds[(int)MyAbility]);
        }
    }
    //逃げの処理
    void HorseMove(float[,] speeds)
    {
        if (transform.position.x < 10)
        {
            speed = Random.Range(speeds[0, 0], speeds[0, 1]);
        }
        else if (transform.position.x >= 10 && transform.position.x < goalpos / 3)
        {
            speed = Random.Range(speeds[1, 0], speeds[1, 1]);
        }
        else if (transform.position.x >= goalpos / 3 && transform.position.x < goalpos - 15)
        {
            speed = Random.Range(speeds[2, 0], speeds[2, 1]);
        }
        else if (transform.position.x >= goalpos - 15 && transform.position.x < goalpos)
        {
            speed = Random.Range(speeds[3, 0], speeds[3, 1]);
        }
    }
}
