using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSpawner : MonoBehaviour
{
    public PlayerManager playerManager;
    public SpinCharacter characterPrefab;
    public Action onPlayerDeath;
    public float randomRadius;
    public Transform[] spawnPoints;

    private static readonly SpinCharacter[] characters = new SpinCharacter[PlayerManager.MAX_PLAYERS];
    private int currentIndex = -1;

    private void Update()
    {
        for (var i = 0; i < PlayerManager.MAX_PLAYERS; i++)
        {
            if (playerManager.players[i] != null)
            {
                if (characters[i] == null)
                {
                    var newCharacter = Instantiate(characterPrefab);
                    newCharacter.Respawn(GetRandomSpawnPosition(), quaternion.identity);
                    characters[i] = newCharacter;
                    playerManager.players[i].character = newCharacter;
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
                playerManager.players[i].character = null;
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