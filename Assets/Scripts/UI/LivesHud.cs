using UnityEngine;
using UnityEngine.UI;

public class LivesHud : MonoBehaviour
{
    public ScoreManager scoreManager;
    public RectTransform livesTransform;
    public Image livePrefab;

    private void Update()
    {
        if (livesTransform.childCount != scoreManager.lives)
        {
            foreach (Transform child in livesTransform.transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 0; i < scoreManager.lives; i++)
            {
                Instantiate(livePrefab, livesTransform);
            }
        }
    }
}