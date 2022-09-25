using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public RectTransform livesTransform;
    public Image livePrefab;

    public void UpdateView(int lives)
    {
        if (livesTransform.childCount != lives)
        {
            foreach (Transform child in livesTransform.transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 0; i < lives; i++)
            {
                Instantiate(livePrefab, livesTransform);
            }
        }
    }
}