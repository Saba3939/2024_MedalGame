using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
public class Horce : MonoBehaviour
{
    public float speed = 1f; //スピード
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
    private float goalpos = 210f;//ゴールポジション
    private GameManager gameManager;//ゲームマネージャーコンポーネントを取得
    private float[,] NigeSpeed = new float[4, 2]{
        {1.52f,1.6f},
        {1.7f,2.2f},
        {1.2f,1.3f},
        {1.2f,1.8f}
    };
    private float[,] OikomiSpeed = new float[4, 2]{
        {1.52f,1.6f},
        {1.2f,1.44f},
        {1.5f,1.6f},
        {2f,2.2f}
    };
    private float[,] SenkouSpeed = new float[4, 2]{
        {1.52f,1.6f},
        {1.5f,1.7f},
        {1.28f,1.7f},
        {1.8f,2.15f}
    };
    private float[,] SasiSpeed = new float[4, 2]{
        {1.52f,1.6f},
        {1.44f,1.6f},
        {1.6f,1.7f},
        {1.92f,2.28f}
    };
    private List<float[,]> Speeds;
    private int nowfase = 0;
    void Start()
    {
        abilityValue = Random.Range(8.0f, 8.5f);//馬の能力値
        goalpos = goal.transform.position.x;//ゴールポジションを更新
        gameManager = myGameManager.GetComponent<GameManager>();//ゲームマネージャーコンポーネントを取得
        gameManager.goalpos = goalpos;
        MyAbility = (Horseability)Enum.ToObject(typeof(Horseability), Random.Range(0, 3));//馬の特性をランダムで決定
        Speeds = new List<float[,]>() { NigeSpeed, OikomiSpeed, SenkouSpeed, SasiSpeed };
        this.transform.DOLocalMoveY(Random.Range(-0.8f, 0.8f), Random.Range(1.0f, 2.0f)).SetEase(Ease.Linear).SetLink(gameObject).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
        this.transform.DORotateQuaternion(new Quaternion(Vector3.forward.x, Vector3.forward.y, 0.05f, Vector3.forward.z), Random.Range(0.12f, 0.15f)).SetLink(gameObject).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
        StartCoroutine(DelayRepeatingSpeed());
    }

    void Update()
    {
        Vector2 pos = transform.position;//posを今のポジションにする
        if (gameManager.start == false) speed = 0;//まだスタートしていないならスピードを０に
        pos.x += speed * abilityValue * Time.deltaTime;//スピードと能力値をかけあわせたものをx座標にプラスする
        if (pos.x > goalpos + 30)
        {
            pos.x = goal.transform.position.x + 30;
        }
        transform.position = pos;//今のポジションをposにする
        if (gameManager.maxhorse == Horsenumber)
        {
            if (transform.position.x <= 20)
            {
                gameManager.fase = 0;
            }
            else if (transform.position.x <= goalpos / 2 && transform.position.x > 20)
            {
                gameManager.fase = 1;
            }
            else if (transform.position.x > goalpos / 2 && transform.position.x <= goalpos / 4 * 3)
            {
                gameManager.fase = 2;
            }
            else
            {
                gameManager.fase = 3;
            }
        }
    }
    private IEnumerator DelayRepeatingSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            HorseMove(Speeds[(int)MyAbility]);
        }
    }
    //逃げの処理
    void HorseMove(float[,] speeds)
    {
        nowfase = gameManager.fase;
        if (nowfase == 0)
        {
            speed = Random.Range(speeds[0, 0], speeds[0, 1]);
        }
        else if (nowfase == 1)
        {
            speed = Random.Range(speeds[1, 0], speeds[1, 1]);
        }
        else if (nowfase == 2)
        {
            speed = Random.Range(speeds[2, 0], speeds[2, 1]);
        }
        else if (nowfase == 3)
        {
            speed = Random.Range(speeds[3, 0], speeds[3, 1]);
        }
    }
}
