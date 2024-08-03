using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
