using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public SpinCharacter characterPrefab;

    private SpinCharacter currentCharacter;

    public Action onPlayerDeath;

    private void Update()
    {
        if (currentCharacter == null)
        {
            currentCharacter = Instantiate(characterPrefab, transform.position, quaternion.identity);
            return;
        }

        if (currentCharacter != null && currentCharacter.dead)
        {
            onPlayerDeath?.Invoke();
            Destroy(currentCharacter.gameObject);
            currentCharacter = Instantiate(characterPrefab, transform.position, quaternion.identity);
        }
    }
}