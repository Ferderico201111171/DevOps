using System.Collections;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject damager;
    [SerializeField] Transform progressValue;
    [SerializeField] AudioSource audioSource;

    [Space(10)] public float duration;

    IEnumerator Magic()
    {
        float time = 0;
        while (time <= duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time/duration);
            progressValue.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }
        progressValue.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, 1);
        particle.gameObject.SetActive(true);
        Camera.main.GetComponent<Shake>().Shake3D();
        audioSource.Play();
        damager.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        progressValue.parent.gameObject.SetActive(false);
        damager.SetActive(false);
        SceneData.score += 1;
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(Magic());
    }
}
