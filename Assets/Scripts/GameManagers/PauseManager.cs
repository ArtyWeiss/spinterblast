using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameModeManager gameManager;
    public InputAction action;

    private void Awake()
    {
        action.performed += ctx => OnPause();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnPause()
    {
        if (gameManager.state == GameModeManager.GameState.Gameplay)
        {
            gameManager.SetPauseState();
        }
        else
        {
            gameManager.SetGamePlayState();
        }
    }

    private void OnDisable()
    {
        action.Disable();
    }
}