using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private float currentHP;

    private PlayerInput playerInput;
    private Vector2 moveInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerData != null)
        {
            currentHP = playerData.maxHP;
        }
        else
        {
            Debug.LogError("PlayerData belum di-assign!");
        }

        if (playerInput == null)
        {
            Debug.LogError("PlayerInput tidak ditemukan di GameObject!");
        }
    }

    void Update()
    {
        if (playerInput == null || playerData == null) return;

        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) 
            * playerData.moveSpeed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TakeDamage(10f * Time.deltaTime);
        }
    }

    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Player Mati");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.LogError("GameManager belum ada!");
        }

        gameObject.SetActive(false);
    }
}