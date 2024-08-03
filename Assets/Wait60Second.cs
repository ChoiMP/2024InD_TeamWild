using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wait60Second : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait60Secounds());
    }

    IEnumerator Wait60Secounds()
    {
        yield return new WaitForSeconds(60f);
        string sceneName = SceneManager.GetActiveScene().name;

        // "Stage"¸¦ ºó ¹®ÀÚ¿­·Î ¹Ù²ß´Ï´Ù.
        string numberString = sceneName.Replace("Stage", "");
        int sceneNum = int.Parse(numberString);
        GameManager.Instance.SaveStageInfo(sceneNum);
        
        if(sceneNum == 1 || sceneNum == 2)
        {
            GameObject.FindObjectOfType<PlayerController>().OnEndRunning();
            GameObject.FindGameObjectWithTag("ClearUI").GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            GameObject.FindObjectOfType<PlayerController>().OnMeetGGumdol();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
