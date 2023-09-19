using UnityEngine;

public class Player : MonoBehaviour
{
    public void Kill() 
    {
        Debug.Log("Game Over");
        FindObjectOfType<Main>().GameOver();
    }
}
