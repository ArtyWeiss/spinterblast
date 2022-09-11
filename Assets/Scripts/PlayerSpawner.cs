using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    public Action onPlayerDeath;
    public float randomRadius;
    public Transform[] spawnPoints;

    private const int MAX_PLAYERS = 4;
    private static readonly SpinCharacter[] currentCharacters = new SpinCharacter[MAX_PLAYERS];

    private int currentIndex = -1;

    public void OnPlayerJoined(PlayerInput input)
    {
        var character = input.gameObject.GetComponent<SpinCharacter>();
        character.Respawn(GetRandomSpawnPosition(), quaternion.identity);
        currentCharacters[input.playerIndex] = character;

        Debug.Log($"Player joined {input.playerIndex}");
    }

    public void OnPlayerLeft(PlayerInput input)
    {
        currentCharacters[input.playerIndex] = null;
        Debug.Log($"Player left {input.playerIndex}");
    }

    private void Update()
    {
        for (var i = 0; i < MAX_PLAYERS; i++)
        {
            if (currentCharacters[i] != null && currentCharacters[i].dead)
            {
                onPlayerDeath?.Invoke();
                // TODO: Добавить случайный начальный поворот
                currentCharacters[i].Respawn(GetRandomSpawnPosition(), quaternion.identity);
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