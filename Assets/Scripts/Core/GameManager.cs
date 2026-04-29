using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;

    public GameState currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            
        }       
    }

     void Start()
    {
        SetState(GameState.MainMenu);
        ResumeGame();
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;

        HandleTimeScale(newState);
        OnStateChanged?.Invoke(newState);

        Debug.Log("State: " + newState);
    }

    private void HandleTimeScale(GameState state)
    {
        Time.timeScale = (state == GameState.Playing) ? 1f : 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        SceneManager.LoadScene("Game");
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
        currentState = GameState.Paused;
        Debug.Log("Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f ;
        currentState = GameState.Playing;
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
        Time.timeScale = 0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetState(GameState.Playing);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        SetState(GameState.MainMenu);
    }
}