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
        // ���� ���� �ε��� ����ϱ�
        int sceneIndex = currentScene.buildIndex;
        GameManager.Instance.LoadStage(sceneIndex-1);
    }
}
