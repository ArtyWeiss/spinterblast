using System;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public GameState state;

    public CharacterSpawner characterSpawner;
    public BotSpawner botSpawner;

    public RectTransform titleScreen;

    public static GameModeManager Instance;

    [Serializable]
    public enum GameState
    {
        Title,
        Gameplay,
        Pause
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SetGamePlayState()
    {
        SetState(GameState.Gameplay);
    }

    public void SetPauseState()
    {
        SetState(GameState.Pause);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        UpdateState();
    }

    private void SetState(GameState newGameState)
    {
        if (newGameState == state) return;
        state = newGameState;

        UpdateState();
    }

    private void UpdateState()
    {
        switch (state)
        {
            case GameState.Title:
                titleScreen.gameObject.SetActive(true);
                characterSpawner.gameObject.SetActive(false);
                botSpawner.gameObject.SetActive(false);
                Time.timeScale = 0f;
                break;
            case GameState.Gameplay:
                titleScreen.gameObject.SetActive(false);
                characterSpawner.gameObject.SetActive(true);
                botSpawner.gameObject.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.Pause:
                titleScreen.gameObject.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
    }
}