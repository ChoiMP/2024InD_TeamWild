using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static FadeSystem Instance { get; private set; }

    [SerializeField] private CanvasGroup canvasGroup; // CanvasGroup ������Ʈ ����
    [SerializeField] private float fadeDuration = 1f; // ���̵� ���� �ð�

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ε�� �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ������Ʈ �ı�
        }

        
    }

    private void OnEnable()
    {
        FadeOut();
    }
    // ���̵� �� �Լ�
    public void FadeIn()
    {
        StartCoroutine(FadeCoroutine(0f, 1f));
    }

    // ���̵� �ƿ� �Լ�
    public void FadeOut()
    {
        StartCoroutine(FadeCoroutine(1f, 0f));
    }

    // �ڷ�ƾ�� �̿��Ͽ� ���̵� ȿ�� ����
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

        // ���� �� ��Ȯ�� ���� ����
        canvasGroup.alpha = endAlpha;
    }
}
