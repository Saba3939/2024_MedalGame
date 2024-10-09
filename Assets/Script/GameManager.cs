using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //publc変数
    public int medalMax = 100; //メダルの初期値  
    public GameObject medalObj = null; //メダルの表示UIオブジェクト
    public int horse_Max = 7; //馬の数
    public List<int> order = new List<int>();//馬の順位のリスト
    public List<GameObject> bets = new List<GameObject>();//ベット数を管理するオブジェクトを格納するリスト
    public int fase = 0;//フェーズ
    public int maxhorse = 0;
    public static int medal = 0; //メダルの現在の数
    public static bool first = true;
    public bool start = false;//スタート
    public bool allGoal = false;//すべてゴール下か判定するフラグ

    //private変数
    private TextMeshProUGUI medalText;//メダル表示のコンポーネント
    public bool Reable = false;//リスタートできるかのフラグ
    private Bet mybet;//ベットコンポーネントの定義
    public GameObject BetScreen; //ベット画面のオブジェクト
    public float goalpos = 0f;
    public GameObject MainScreen; //メイン画面のオブジェクト
    public GameObject ResultScreen;//リザルト画面のオブジェクト
    public int addMedalNum = 0;
    void Start()
    {
        BetScreen.SetActive(true);
        MainScreen.SetActive(false);
        ResultScreen.SetActive(false);
        if (first)
        {
            medal = medalMax;
            first = false;
        }
    }
    void Update()
    {
        //スペースを押したときにスタート
        if (Input.GetKeyDown(KeyCode.Space) && start == false)
        {
            start = true;
            BetScreen.SetActive(false);
            MainScreen.SetActive(true);
        }
        //メダルの表示を変更
        medalText = medalObj.GetComponent<TextMeshProUGUI>();
        medalText.SetText("めだる: " + medal);

        //レースが終わったかを判断するプログラム
        if (order.Count == horse_Max && allGoal == false && Reable == false)
        {
            allGoal = true;
        }

        //すべてゴールしたときの処理
        if (allGoal == true)
        {
            mybet = bets[order[0] - 1].GetComponent<Bet>();//優勝馬のベット数を取得する準備
            addMedalNum = (int)((float)mybet.betnum * mybet.odds);//オッズに応じた配当を渡す
            medal += addMedalNum;
            Debug.Log(medal);
            Debug.Log(order[0]);
            Reable = true;//レースが終わったのでtrueにする
            allGoal = false;//falseにしないとエラーが出る(リストの中身をなくしているため)
        }
        if (Reable == true)
        {
            ResultScreen.SetActive(true);
        }
    }

    //Restartボタンが押されたときに実行される関数
    public void Restart()
    {
        if (Reable == true)
        {
            order.Clear();//リストの中身を削除
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//今のシーンを再度読み込む
        }
    }
}
