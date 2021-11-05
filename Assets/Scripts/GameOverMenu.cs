using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public void OnPlayerDied()
    {
        gameObject.SetActive(true);
    }
}
