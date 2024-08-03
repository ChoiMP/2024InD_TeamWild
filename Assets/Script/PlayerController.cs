using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle = 0,
    Running,
    Jumping,
    Sliding
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    public int hp;
    [SerializeField]
    public const int MAXHP = 3;
    [SerializeField]
    private GameObject[] hearts;

    [SerializeField]
    private bool bIsInvincible;

    public PlayerState state;

    private WaitForSeconds WF01 = new WaitForSeconds(0.1f);


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        bIsInvincible = false;
        isGrounded = true;
        state = PlayerState.Idle;

        hp = MAXHP;

        DisableInput();
    }

    private void Start()
    {
        FadeSystem.Instance.FadeOut();
    }
    private void LateUpdate()
    {
        animator.SetInteger("State", (int)state);
    }

    /// <summary> 출발시킬때 호출 <summary>
    public void OnStartRunning()
    {
        state = PlayerState.Running;
    }

    public void OnEndRunning()
    {
        StartCoroutine(EndRunning());
        SoundManager.Instance.PlayArrivalSound();
    }

    IEnumerator EndRunning()
    {
        while (true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 15f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Debug.Log("점프");
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            state = PlayerState.Jumping;
            SoundManager.Instance.PlayJumpSound();
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            if (context.performed)
            {
                if (isGrounded)
                {
                    Debug.Log("슬라이딩 시작");
                    boxCollider.size = new Vector2(1, 1);
                    boxCollider.offset = new Vector2(0, 0f);
                    state = PlayerState.Sliding;
                    SoundManager.Instance.PlaySlideSound();
                }
            }
            else
            {
                if (isGrounded)
                {
                    Debug.Log("슬라이딩 끝");
                    boxCollider.size = new Vector2(1, 2);
                    boxCollider.offset = new Vector2(0, 0.3f);
                    state = PlayerState.Running;
                }
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("esc");
        if (context.performed)
        {
            if (Time.timeScale == 1)
            {
                Debug.Log("일시정지");
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    // <summary> 플레이어 피격시 호출 /summary>
    public void OnHit()
    {
        if (!bIsInvincible)
        {
            Debug.Log("피격");
            StartCoroutine(Hit());
            if (--hp <= 0)
            {
                OnDeath();
            }
            OnChangeHealth();
            SoundManager.Instance.PlayHitSound();
        }
    }

    public void OnGainHealth()
    {
        if (hp < MAXHP)
        {
            hp++;
        }
        OnChangeHealth();
    }

    private void OnChangeHealth()
    {
        for (int i = 0; i < MAXHP; i++)
        {
            if (i < hp)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    IEnumerator Hit()
    {
        bIsInvincible = true;
        bool flag = true;
        for (int i = 0; i < 30; i++)
        {
            if (flag)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0);
                flag = !flag;
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
                flag = !flag;
            }
            yield return WF01;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        bIsInvincible = false;
        yield return null;
    }

    /// <summary> 플레이어 사망시 호출 /summary>
    public void OnDeath()
    {
        Debug.Log("OnDeath");
    }

    /// <summary> 플레이어 입력을 가능하게 /summary>
    public void DisableInput()
    {
        playerInput.enabled = false;
    }

    /// <summary> 플레이어 입력을 불가능하게 /summary>
    public void EnableInput()
    {
        playerInput.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("StartRunning"))
        {
            EnableInput();
        }
        else if (collision.transform.CompareTag("StopRunning"))
        {
            DisableInput();
            OnEndRunning();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            if(state == PlayerState.Sliding)
            {
                return;
            }
            state = PlayerState.Running;
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}