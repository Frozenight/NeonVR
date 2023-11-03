using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Assign this in the inspector with your TextMeshProUGUI element
    private float deltaTime;

    void Update()
    {
        // Calculate deltaTime to get the time it takes to complete each frame
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate the frames per second
        float fps = 1.0f / deltaTime;

        // Update the TextMeshProUGUI text to display the FPS, formatted to show 1 decimal place
        fpsText.text = string.Format("{0:0.0} FPS", fps);
    }
}
