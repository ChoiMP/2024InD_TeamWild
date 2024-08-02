using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGamemanger : MonoBehaviour
{
    public static MapGamemanger Instance { get; private set; }
    public bool _break = true;
    private int _numrespawns=0;
    public Transform[] _respawns;
    public Transform[] _upObejct;
    public Transform[] _downObejct;

    void Fire()
    {
        _numrespawns = UnityEngine.Random.Range(0, _respawns.Length);
        Transform respawn = _respawns[_numrespawns];

        if (_numrespawns == 0)
        {
            Instantiate(_upObejct[UnityEngine.Random.Range(0, _upObejct.Length)], respawn.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_downObejct[UnityEngine.Random.Range(0, _downObejct.Length)], respawn.position, Quaternion.identity);
        }
    }

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
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        int fireCount = 0;

        // �� �̸��� ���� ȣ�� Ƚ�� ����
        switch (sceneName)
        {
            case "break":
                fireCount = 2;
                break;
            default:
                Debug.LogWarning("Unknown scene name");
                break;
        }

        // ȣ�� Ƚ���� ���������� �ڷ�ƾ ����
        if (fireCount > 0)
        {
            StartCoroutine(FireWithDelay(fireCount));
        }
    }

    private IEnumerator FireWithDelay(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Fire(); // Fire �޼��� ȣ��
            yield return new WaitForSeconds(0.5f); // 1.5�� ��ٸ���
        }
    }

    private void Update()
    {
        if (!_break)
        {
            Fire();
            _break = true;
        }
    }
}