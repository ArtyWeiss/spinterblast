using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotSpawner : MonoBehaviour
{
    public SpinCharacter characterPrefab;
    [Space(20)] public int maxBotsCount;
    public float spawnCooldown;
    public float randomRadius;
    public Transform[] spawnPoints;

    private float lastSpawnTime;
    private List<SpinCharacter> currentCharacters = new();
    private List<SpinCharacter> deadCharacters = new();
    private int currentIndex = -1;

    public Action onBotDeath;

    private void Update()
    {
        if (Time.time > lastSpawnTime + spawnCooldown)
        {
            if (currentCharacters.Count < maxBotsCount)
            {
                currentIndex = GetNewRandomIndex();
                var randomOffset = Random.insideUnitCircle * randomRadius;
                var position = spawnPoints[currentIndex].position + new Vector3(randomOffset.x, 0f, randomOffset.y);
                currentCharacters.Add(Instantiate(characterPrefab, position, Quaternion.identity));
                lastSpawnTime = Time.time;
            }
        }

        DestroyDeadCharacters();
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

    private void DestroyDeadCharacters()
    {
        foreach (var character in currentCharacters)
        {
            if (character.dead)
            {
                deadCharacters.Add(character);
                onBotDeath?.Invoke();
            }
        }

        foreach (var deadCharacter in deadCharacters)
        {
            currentCharacters.Remove(deadCharacter);
            Destroy(deadCharacter.gameObject);
        }

        deadCharacters.Clear();
    }

    private void OnDrawGizmos()
    {
        foreach (var point in spawnPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point.position, randomRadius);
        }
    }
}