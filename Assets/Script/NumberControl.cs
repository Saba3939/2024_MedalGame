using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberControl : MonoBehaviour
{

    public List<GameObject> uiElements; // 位置を反映するUI要素
    public List<GameObject> horseObjects; // 位置を反映するUI要素
    public GameObject gameManager = null;
    private GameManager myGameManager;
    public float goalpos = 210f;
    // Start is called before the first frame update
    void Start()
    {
        myGameManager = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        goalpos = myGameManager.goalpos;
        for (int i = 0; i < uiElements.Count; i++)
        {
            if (uiElements[i] != null && uiElements[i].GetComponent<RectTransform>())
            {
                Vector3 screenPosition = uiElements[i].GetComponent<RectTransform>().position;
                screenPosition.x = 1600 / goalpos * horseObjects[i].transform.position.x + 200;
                if (screenPosition.x > 1800)
                {
                    screenPosition.x = 1800;
                }
                uiElements[i].GetComponent<RectTransform>().position = screenPosition;
            }
        }
    }
}
