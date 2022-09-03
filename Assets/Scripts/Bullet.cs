using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float hitImpulse;
    public float blockedHitImpulse;
    public ParticleSystem explosionPrefab;
    public ParticleSystem blockPrefab;

    private float startTime;
    private const float lifetime = 3f;

    public FMODUnity.EventReference HitEvent;
    public FMODUnity.EventReference BlockEvent;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
        if (Time.time > startTime + lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var character = other.GetComponent<SpinCharacter>();
        if (character)
        {
            var isBlocked = character.IsHitBlocked(transform.forward);
            var impulse = isBlocked ? blockedHitImpulse : hitImpulse;
            other.attachedRigidbody.AddForce(transform.forward * impulse, ForceMode.Impulse);

            FMODUnity.RuntimeManager.PlayOneShot(isBlocked ? BlockEvent : HitEvent);
            Instantiate(isBlocked ? blockPrefab : explosionPrefab, transform.position, quaternion.identity);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(HitEvent);
            Instantiate(explosionPrefab, transform.position, quaternion.identity);
        }

        Destroy(gameObject);
    }
}