using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Displays score to the Ui.
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        // Players score is rounded down when we display it by using .ToString("F0")
        text.text = ("Distance: " + GameManager.instance.scoreManager.Distance.ToString("F0") + "M");
    }
}