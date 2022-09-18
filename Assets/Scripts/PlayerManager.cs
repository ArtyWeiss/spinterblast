using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public const int MAX_PLAYERS = 4;
    public readonly Player[] players = new Player[MAX_PLAYERS];

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
}