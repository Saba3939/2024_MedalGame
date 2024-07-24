using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public GameObject myGameManager;
    private Horce  HorceScript;
    private GameManager gameManager;

    //ゴールに馬が触れたときの処理
    private void OnTriggerEnter2D(Collider2D other) {
        //馬のスクリプトを取得し、ゲームマネージャーの順位を格納するorderリストに順番に追加していく
        HorceScript = other.GetComponent<Horce>();
        gameManager = myGameManager.GetComponent<GameManager>();                      
        gameManager.order.Add(HorceScript.Horsenumber);
    }
}
