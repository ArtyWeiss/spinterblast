using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public BotSpawner botSpawner;
    public PlayerManager playerManager;
    public int maxLives;
    [NonSerialized] public int lives, score;

    public FMODUnity.EventReference KillEvent;
    public FMODUnity.EventReference DamageEvent;
    public FMODUnity.EventReference DeathEvent;

    private void Awake()
    {
        botSpawner.onBotDeath = IncreaseScore;
        // TODO: Уменьшение жизней конкретного игрока
        // playerManager.onPlayerDeath = DecreaseLives;
        lives = maxLives;
    }

    private void IncreaseScore()
    {
        score++;
        FMODUnity.RuntimeManager.PlayOneShot(KillEvent);
    }

    private void DecreaseLives()
    {
        lives--;
        FMODUnity.RuntimeManager.PlayOneShot(lives == 0 ? DeathEvent : DamageEvent);
        if (lives == 0)
        {
            lives = maxLives;
            score = 0;
        }
    }
}