using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject gameOver;

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
