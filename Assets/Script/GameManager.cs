using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TMPro;
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
    public GameObject Restartbutton;//リスタートボタン
    public static int medal = 1000; //メダルの現在の数
    public bool start = false;//スタート
    public bool allGoal = false;//すべてゴール下か判定するフラグ

    //private変数
    private TextMeshProUGUI medalText;//メダル表示のコンポーネント
    private bool Reable = false;//リスタートできるかのフラグ
    private Bet mybet;//ベットコンポーネントの定義
    void Start()
    {
        Application.targetFrameRate = 60;//フレームレートを６０に制限(じゃないと早くなるから) 
    }
    void Update()
    {
        //スペースを押したときにスタート
        if(Input.GetKeyDown(KeyCode.Space) && start == false){
            start = true;
        }

        //メダルの表示を変更
        medalText = medalObj.GetComponent<TextMeshProUGUI>();
        medalText.SetText("Medal: " +medal);

        //レースが終わったかを判断するプログラム
        if(order.Count == horse_Max && allGoal == false){
            allGoal = true;
        }

        //すべてゴールしたときの処理
        if(allGoal == true){
            mybet = bets[order[0] - 1].GetComponent<Bet>();//優勝馬のベット数を取得する準備
            medal += (int)((float)mybet.betnum * mybet.odds);//オッズに応じた配当を渡す
            Debug.Log(order[0]);
            order.Clear();//orderリストをクリア
            Reable = true;//レースが終わったのでtrueにする
            allGoal = false;//falseにしないとエラーが出る(リストの中身をなくしているため)
        }
        if(Reable == true){
            Restartbutton.SetActive(true);//Restartボタンを表示させる
        }
    }

    //Restartボタンが押されたときに実行される関数
    public void Restart(){
        if(Reable == true){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//今のシーンを再度読み込む
        }
    }
}
