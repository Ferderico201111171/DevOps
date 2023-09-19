using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI value;

    void LateUpdate()
    {
        value.text = SceneData.score.ToString();
    }
}
