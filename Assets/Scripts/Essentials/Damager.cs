using UnityEngine;

public class Damager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Kill();
            gameObject.SetActive(false);
        }
    }
}
