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

        // 씬이 언로드될 때 호출
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        // 활성 씬이 변경될 때 호출
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // SoundManager.Instance.StopBGM();
        FadeSystem.Instance.FadeOut();
        string sceneName = SceneManager.GetActiveScene().name;

        // "Stage"를 빈 문자열로 바꿉니다.
        string numberString = sceneName.Replace("Stage", "");

        // 숫자로 변환
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
