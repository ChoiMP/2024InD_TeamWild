using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ���� ��ε�� �� ȣ��
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        // Ȱ�� ���� ����� �� ȣ��
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // SoundManager.Instance.StopBGM();
        FadeSystem.Instance.FadeOut();
        string sceneName = SceneManager.GetActiveScene().name;

        // "Stage"�� �� ���ڿ��� �ٲߴϴ�.
        string numberString = sceneName.Replace("Stage", "");

        // ���ڷ� ��ȯ
        int number = int.Parse(numberString);
        Debug.Log(number);
        SoundManager.Instance.PlayBGM(number);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SoundManager.Instance.StopBGM();
    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
    }
    public void LoadScene(int sceneNum)
    {
        StartCoroutine(DelayLoadScene(sceneNum));
    }

    IEnumerator DelayLoadScene(int sceneNum)
    {
        FadeSystem.Instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNum);
    }

    public void LoadStage(int sceneNum)
    {
        StartCoroutine(DelayLoadStage(sceneNum + 2));
        
    }

    IEnumerator DelayLoadStage(int sceneNum)
    {
        FadeSystem.Instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNum);
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
    }
}
