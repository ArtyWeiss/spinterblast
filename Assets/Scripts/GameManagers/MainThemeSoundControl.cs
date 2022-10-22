using UnityEngine;

public class MainThemeSoundControl : MonoBehaviour
{
    public FMODUnity.EventReference mainThemeEvent;
    [Range(0f, 1f)] public float equalizer;
    public string parameterName;

    private FMOD.Studio.EventInstance instance;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(mainThemeEvent);
        instance.start();
    }

    private void Update()
    {
        equalizer = IsGameplayState() ? 0 : 1;
        instance.setParameterByName(parameterName, equalizer);
    }

    private static bool IsGameplayState()
    {
        var currentState = GameModeManager.Instance.state;
        return currentState == GameModeManager.GameState.PvE || currentState == GameModeManager.GameState.PvP;
    }
}