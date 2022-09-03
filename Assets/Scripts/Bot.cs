using UnityEngine;

public class Bot : MonoBehaviour
{
    public SpinCharacter character;

    public float maxAimDistance;
    public float maxBorderDistance;
    [Range(0f, 1f)] public float shotChance;

    private const string PlayerTag = "Player";
    private const string LevelBorder = "LevelBorder";

    private void Update()
    {
        // Если сзади слишком близко находится край, то выходим сразу
        var backRay = new Ray(character.transform.position, -character.characterTransform.forward);
        Debug.DrawRay(character.transform.position, -character.characterTransform.forward * maxBorderDistance, Color.green);
        if (Physics.Raycast(backRay, out var backHit, maxBorderDistance))
        {
            if (backHit.collider.gameObject.layer == LayerMask.NameToLayer(LevelBorder))
            {
                Debug.DrawRay(backHit.point, backHit.normal, Color.red);
                return;
            }
        }

        var aimRay = new Ray(character.transform.position, character.characterTransform.forward);
        Debug.DrawRay(character.transform.position, character.characterTransform.forward * maxAimDistance, Color.cyan);
        if (Physics.Raycast(aimRay, out var hit, maxAimDistance))
        {
            Debug.DrawRay(hit.point, hit.normal, Color.magenta);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(LevelBorder))
            {
                if (hit.distance <= maxBorderDistance && Vector3.Dot(hit.normal, aimRay.direction) < -0.97f)
                {
                    character.isFirePressed = true;
                }
            }
            else
            {
                var targetChar = hit.collider.transform.GetComponent<SpinCharacter>();
                var shotRoll = Random.value < shotChance;

                if (shotRoll && targetChar != null && targetChar.gameObject.CompareTag(PlayerTag))
                {
                    character.isFirePressed = true;
                }
            }
        }
    }
}