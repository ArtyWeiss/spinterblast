using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    public SpinCharacter characterPrefab;
    [Space(20)] public Action onPlayerDeath;
    public float randomRadius;
    public Transform[] spawnPoints;

    private const int MAX_PLAYERS = 4;
    private static readonly Player[] players = new Player[MAX_PLAYERS];
    private static readonly SpinCharacter[] characters = new SpinCharacter[MAX_PLAYERS];

    private int currentIndex = -1;

    public void OnPlayerJoined(PlayerInput input)
    {
        var player = input.gameObject.GetComponent<Player>();
        players[input.playerIndex] = player;
        Debug.Log($"Player joined {input.playerIndex}");
    }

    public void OnPlayerLeft(PlayerInput input)
    {
        Destroy(players[input.playerIndex].gameObject);
        players[input.playerIndex] = null;
        Debug.Log($"Player left {input.playerIndex}");
    }

    private void Update()
    {
        // TODO: Перенести логику спавна в отдельный компонент (чтобы можно было отключить ее отдельно от добавления игроков)
        for (var i = 0; i < MAX_PLAYERS; i++)
        {
            if (players[i] != null)
            {
                if (characters[i] == null)
                {
                    var newCharacter = Instantiate(characterPrefab);
                    newCharacter.Respawn(GetRandomSpawnPosition(), quaternion.identity);
                    characters[i] = newCharacter;
                    players[i].character = newCharacter;
                }
                else if (characters[i].dead)
                {
                    onPlayerDeath?.Invoke();
                    // TODO: Добавить случайный начальный поворот
                    characters[i].Respawn(GetRandomSpawnPosition(), quaternion.identity);
                }
            }
            else if (characters[i] != null)
            {
                players[i].character = null;
                Destroy(characters[i].gameObject);
                characters[i] = null;
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        currentIndex = GetNewRandomIndex();
        var randomOffset = Random.insideUnitCircle * randomRadius;
        return spawnPoints[currentIndex].position + new Vector3(randomOffset.x, 0f, randomOffset.y);
    }

    private int GetNewRandomIndex()
    {
        if (spawnPoints.Length > 1)
        {
            var spawnIndex = currentIndex;
            while (spawnIndex == currentIndex)
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            }

            return spawnIndex;
        }

        return 0;
    }

    private void OnDrawGizmos()
    {
        foreach (var point in spawnPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(point.position, randomRadius);
        }
    }
}