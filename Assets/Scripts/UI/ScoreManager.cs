using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public BotSpawner botSpawner;

    public PlayerManager playerManager;

    public PlayerHud[] playersHud;

    public FMODUnity.EventReference KillEvent;
    public FMODUnity.EventReference DamageEvent;
    public FMODUnity.EventReference DeathEvent;

    private void Awake()
    {
        botSpawner.onBotDeath = IncreaseScore;
    }

    private void IncreaseScore()
    {
        // score++;
        FMODUnity.RuntimeManager.PlayOneShot(KillEvent);
    }

    private void Start()
    {
        if (playersHud.Length != PlayerManager.MAX_PLAYERS) return;
        for (var i = 0; i < PlayerManager.MAX_PLAYERS; i++)
        {
            playersHud[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playersHud.Length != PlayerManager.MAX_PLAYERS) return;
        for (var i = 0; i < PlayerManager.MAX_PLAYERS; i++)
        {
            if (playerManager.players[i] != null)
            {
                if (!playersHud[i].isActiveAndEnabled)
                {
                    playersHud[i].gameObject.SetActive(true);
                }

                playersHud[i].UpdateView(playerManager.players[i].livesCount);
            }
            else if (playersHud[i].isActiveAndEnabled)
            {
                playersHud[i].gameObject.SetActive(false);
            }
        }
    }
}