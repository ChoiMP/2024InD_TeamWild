using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void OnLoadStageScene(int sceneNum)
    {
        GameManager.Instance.LoadStage(sceneNum);
    }

    public void OnLoadScene(int sceneNum)
    {
        GameManager.Instance.LoadScene(sceneNum);
    }

    public void OnLoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        // 현재 씬의 인덱스 출력하기
        int sceneIndex = currentScene.buildIndex;
        GameManager.Instance.LoadStage(sceneIndex-1);
    }
}
