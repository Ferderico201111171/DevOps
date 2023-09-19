using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneData : MonoBehaviour
{
    public static int score;
    [SerializeField] Image fade;

    Color dimColor;

    void Awake()
    {
        score = 0;
        dimColor = fade.color;
    }

    async void Start()
    {
        Time.timeScale = 1;
        await Dim(1, 0);
    }

    public async Task Dim(float start, float end) 
    {
        float time = 0;
        float duration = 0.75f;
        fade.gameObject.SetActive(true);
        while (time <= duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time/duration);
            float v = Mathf.Lerp(start, end, t);
            fade.color = Color.Lerp(Color.clear, dimColor, v);
            await Task.Yield();
        }
        fade.color = Color.Lerp(Color.clear, dimColor, 1);
        fade.gameObject.SetActive(end == 1? true : false);
        await Task.Yield();
    }

    public async static void LoadScene(string name)
    {   
        SceneData sceneData = FindObjectOfType<SceneData>();
        AsyncOperation progres = SceneManager.LoadSceneAsync(name);
        progres.allowSceneActivation = false;
        await sceneData.Dim(0, 1);
        while (!progres.isDone)
        {
            if (progres.progress >= 0.9f) break;
            await Task.Yield();
        }
        progres.allowSceneActivation = true;
    }

    public static void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void Quit() 
    {
        Application.Quit();
    }
}
