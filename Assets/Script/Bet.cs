using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bet : MonoBehaviour
{
    //パブリック変数
    public GameObject gamemanagerob = null; //GameManagerオブジェクトを格納する変数
    public GameObject betText = null;//ベット数を表示するテキストを格納する 
    public GameObject myhorse = null;
    public GameObject OddsText = null;//オッズを表示するテキストを格納する変数

    public int betnum = 0; //ベット数
    public float normalodds = 4.0f;
    public float odds = 1.0f; //払い戻し倍率    

    //プライベート変数
    private GameManager gameManagerScript = null; //GameManagerスクリプトを格納する 
    private Horce myhorseScript = null;
    private TextMeshProUGUI betTextUGUI = null;//ベットテキストのTextMeshProUGUIのコンポーネントを取得  
    private TextMeshProUGUI OddsTxt;//オッズを表示させるTextMeshProUGUIのコンポーネントを取得
    void Start()
    {
        StartCoroutine("wait");
        //以下オッズをきめるプログラム
        myhorseScript = myhorse.GetComponent<Horce>();
        switch ((int)myhorseScript.MyAbility)
        {
            case 0:
                normalodds += UnityEngine.Random.Range(3.0f, 4.0f) * 1.5f;
                break;
            case 1:
                normalodds += UnityEngine.Random.Range(0.5f, 2.5f) * 1.5f;
                break;
            case 2:
                normalodds += UnityEngine.Random.Range(0f, 2.0f) * 1.5f;
                break;
            case 3:
                normalodds += UnityEngine.Random.Range(1.5f, 3.0f) * 1.5f;
                break;
        }
        gameManagerScript = gamemanagerob.GetComponent<GameManager>(); //ゲームマネージャーコンポーネントを取得
        OddsTxt = OddsText.GetComponent<TextMeshProUGUI>();//オッズテキストを取得
        odds = (float)Math.Round(normalodds, 1, MidpointRounding.AwayFromZero);
        if (odds < 3.0f)
        {
            OddsTxt.SetText("いいね");
        }
        else if (odds < 6.5f)
        {
            OddsTxt.SetText("まあまあ");
        }
        else
        {
            OddsTxt.SetText("だめかも");
        }
    }

    void Update()
    {
        betTextUGUI = betText.GetComponent<TextMeshProUGUI>();//テキストを取得
        betTextUGUI.SetText("" + betnum);//ベット数のテキストを変更する

    }

    public void betBy(int betvalue)
    {
        if (GameManager.medal - betvalue >= 0 && gameManagerScript.start != true)
        {
            betnum += betvalue;//ベット数を増やす
            GameManager.medal -= betvalue;//ベット数を増やしたのでメダルを減らす
        }
    }
    public void betOne()
    {
        betBy(1);
    }
    public void betTen()
    {
        betBy(10);
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        myhorseScript = myhorse.GetComponent<Horce>();
        switch ((int)myhorseScript.MyAbility)
        {
            case 0:
                normalodds += UnityEngine.Random.Range(3.0f, 4.0f);
                break;
            case 1:
                normalodds += UnityEngine.Random.Range(1.0f, 2.5f);
                break;
            case 2:
                normalodds += UnityEngine.Random.Range(0f, 2.0f);
                break;
            case 3:
                normalodds += UnityEngine.Random.Range(2.0f, 3.0f);
                break;
        }
        normalodds += (8.5f - myhorseScript.abilityValue) * 5.0f;
    }
}
