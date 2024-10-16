using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject GameManagerObj;
    private GameManager gameManager;
    public List<GameObject> HorseObj;
    public GameObject MedalGetText;
    private TextMeshProUGUI TMP;
    private bool Reable = false;

    void Start()
    {
        gameManager = GameManagerObj.GetComponent<GameManager>();
        TMP = MedalGetText.GetComponent<TextMeshProUGUI>();
        Reable = false;
    }

    void Update()
    {
        if (gameManager.Reable == true && Reable == false)
        {
            HorseObj[gameManager.order[0] - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 115, 0);
            HorseObj[gameManager.order[1] - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(-140, 40, 0);
            HorseObj[gameManager.order[2] - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(140, 20, 0);
            HorseObj[gameManager.order[0] - 1].SetActive(true);
            HorseObj[gameManager.order[1] - 1].SetActive(true);
            HorseObj[gameManager.order[2] - 1].SetActive(true);
            TMP.SetText("えさ" + gameManager.addMedalNum + "こゲット!");
            Reable = true;
        }
    }
}
