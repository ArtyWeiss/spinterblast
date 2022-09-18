using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int livesCount;
    public int score;
    public SpinCharacter character;

    public void OnStop(InputAction.CallbackContext context)
    {
        if (character == null) return;
        character.isStopPressed = context.action.triggered;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (character == null) return;
        character.isFirePressed = context.action.triggered;
    }
}