using TMPro;
using UnityEngine;

public class ScoreHud : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text scoreText;

    private void Update()
    {
        scoreText.text = scoreManager.score.ToString();
    }
}