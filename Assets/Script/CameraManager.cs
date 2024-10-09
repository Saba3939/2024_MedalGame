using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraManager : MonoBehaviour
{
    public GameObject gameManager = null;//ゲームマネージャオブジェクトを取得
    public float goal = 210f;//ゴールのx座標を取得
    public List<GameObject> Horse = new List<GameObject>();
    private GameManager myGameManager;//ゲームマネージャコンポーネントを定義

    void Start()
    {
        myGameManager = gameManager.GetComponent<GameManager>();//ゲームマネージャコンポーネントをゲームマネージャから取得
    }

    private int num = 0;
    float min = 0, max = 0;
    void Update()
    {
        goal = myGameManager.goalpos;//ゴールのx座標を取得  
        num = 0;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);//今のポジションをposに格納
        foreach (GameObject obj in Horse)
        {
            if (num == 0)
            {
                min = obj.transform.position.x;
                max = obj.transform.position.x;
            }
            else if (min > obj.transform.position.x)
            {
                min = obj.transform.position.x;
            }
            else if (max < obj.transform.position.x)
            {
                max = obj.transform.position.x;
                myGameManager.maxhorse = obj.GetComponent<Horce>().Horsenumber;
            }
            num++;
        }
        if (myGameManager.start == true)
        {
            pos.x = max - 10f;
        }
        if (pos.x > goal)
        {
            pos.x = goal;
        }
        transform.position = pos;//posの座標に移動
    }
}
