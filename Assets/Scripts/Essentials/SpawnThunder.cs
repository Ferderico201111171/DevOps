using UnityEngine;
using System.Collections;

public class SpawnThunder : MonoBehaviour
{
    [SerializeField] MagicEffect magicEffect;
    [SerializeField] float spawnInterval = 1;

    Transform target;

    void Awake()
    {
        target = FindObjectOfType<Player>().transform;
    } 

    public void Spawn(Vector3 position)
    {
        Instantiate(magicEffect.gameObject, position, Quaternion.identity);
    }

    IEnumerator Engine() 
    {
        Vector3 PredictPos() {
            Vector3 velocity = target.GetComponent<Rigidbody>().velocity * magicEffect.duration;
            Vector3 pos = target.position + new Vector3(velocity.x, 0, velocity.z);
            Vector3 final = pos;
            if(Physics.Raycast(pos + Vector3.up * 5, Vector3.down, out RaycastHit hit, 5*2, LayerMask.GetMask("Ground")))
            {
                final = hit.point;
            }
            return final + Vector3.up * 0.3f;
        }

        yield return new WaitForSeconds(3);
        while (true)
        {
            Spawn(PredictPos());
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Start()
    {
        StartCoroutine(Engine());
    }
}
