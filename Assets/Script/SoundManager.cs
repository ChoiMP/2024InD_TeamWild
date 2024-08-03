using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip slideSound;
    public AudioClip hitSound;
    public AudioClip arrivalSound;

    [Header("Background Music")]
    public AudioClip bgmStage1;
    public AudioClip bgmStage2;
    public AudioClip bgmStage3;

    private AudioSource sfxSource;
    private AudioSource bgmSource;

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

        // Create two audio sources, one for SFX and one for BGM
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();

        sfxSource.priority = 0;

        bgmSource.loop = true; // BGM should loop
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlaySlideSound()
    {
        PlaySound(slideSound);
    }

    public void PlayHitSound()
    {
        PlaySound(hitSound);
    }

    public void PlayArrivalSound()
    {
        PlaySound(arrivalSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(int stage)
    {
        AudioClip selectedBGM = null;

        switch (stage)
        {
            case 1:
                selectedBGM = bgmStage1;
                break;
            case 2:
                selectedBGM = bgmStage2;
                break;
            case 3:
                selectedBGM = bgmStage3;
                break;
        }

        if (selectedBGM != null && bgmSource.clip != selectedBGM)
        {
            bgmSource.clip = selectedBGM;
            bgmSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
