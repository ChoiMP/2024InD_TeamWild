using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static FadeSystem Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup; // CanvasGroup 컴포넌트 참조
    [SerializeField] private float fadeDuration = 1f; // 페이드 지속 시간

    private void Awake()
    {
        // 싱글턴 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 로드될 때 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 오브젝트 파괴
        }

        
    }

    private void OnEnable()
    {
        FadeOut();
    }
    // 페이드 인 함수
    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(0f, 1f));
    }

    // 페이드 아웃 함수
    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine(1f, 0f));
    }

    // 코루틴을 이용하여 페이드 효과 구현
    private System.Collections.IEnumerator FadeCoroutine(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        // 종료 시 정확한 값을 설정
        canvasGroup.alpha = endAlpha;
    }
}
