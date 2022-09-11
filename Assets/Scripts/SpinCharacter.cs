using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinCharacter : MonoBehaviour
{
    [Header("Input")] public bool isStopPressed;
    public bool isFirePressed;

    [Space(20)] public float reloadDuration;
    public float recoilImpulse;

    [Space(20)] [Tooltip("Запас вращения (В углах Эулера), который тратится при паузе")]
    public float maxStamina;

    public float angularSpeed;
    [Range(0f, 1f)] public float recoveryMultiplier;

    [Space(20)]
    [Tooltip(
        "Результат скалярного произведения направления выстрела и текущего направления персонажа, выше которого будет засчитываться блок")]
    public float blockDot;

    [Space(20)] public Rigidbody characterBody;
    public Transform characterTransform;

    [Space(20)] public ParticleSystem shotEffect;
    public GameObject bulletPrefab;
    public Transform barrelEnd;

    public EventReference ShotEvent;

    [NonSerialized] public float stamina;
    [NonSerialized] public bool dead;

    private float lastShotTime;

    public void OnStop(InputAction.CallbackContext context)
    {
        isStopPressed = context.action.triggered;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        isFirePressed = context.action.triggered;
    }

    private void Update()
    {
        var currentSpeed = isStopPressed && stamina > 0f ? 0f : angularSpeed;
        var delta = Time.deltaTime * currentSpeed;
        characterTransform.Rotate(Vector3.up, delta);

        stamina += Time.deltaTime * (isStopPressed ? -angularSpeed : angularSpeed * recoveryMultiplier);
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        if (isFirePressed)
        {
            if (Time.time >= lastShotTime + reloadDuration)
            {
                RuntimeManager.PlayOneShot(ShotEvent);
                characterBody.AddForce(-characterTransform.forward * recoilImpulse, ForceMode.Impulse);
                shotEffect.Play();
                Instantiate(bulletPrefab, barrelEnd.position, characterTransform.rotation);
                lastShotTime = Time.time;
            }

            isFirePressed = false;
        }
    }

    public bool IsHitBlocked(Vector3 hitDirection)
    {
        var dot = Vector3.Dot(hitDirection, characterTransform.forward);
        return dot > blockDot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillTrigger"))
        {
            dead = true;
        }
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        stamina = maxStamina;
        lastShotTime = Time.time;
        dead = false;
    }
}