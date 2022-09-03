using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class StaminaHud : MonoBehaviour
{
    public SpinCharacter character;
    public Image fill;
    public CanvasGroup hud;
    public LookAtConstraint constraint;

    [Space(20)] public float appearSpeed;

    private float appearProgress;

    private void Start()
    {
        constraint.SetSource(0, new ConstraintSource {sourceTransform = Camera.main.transform, weight = 1f});
        hud.alpha = 0;
    }

    private void Update()
    {
        var isStaminaFull = character.stamina == character.maxStamina;
        appearProgress += Time.deltaTime * (isStaminaFull ? -appearSpeed : appearSpeed);
        appearProgress = Mathf.Clamp01(appearProgress);

        hud.alpha = appearProgress;
        fill.fillAmount = character.stamina / character.maxStamina;
    }
}