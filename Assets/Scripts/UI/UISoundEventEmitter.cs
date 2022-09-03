using UnityEngine;

public class UISoundEventEmitter : MonoBehaviour
{
    public FMODUnity.EventReference EventReference;

    public void Play()
    {
        FMODUnity.RuntimeManager.PlayOneShot(EventReference);
    }
}