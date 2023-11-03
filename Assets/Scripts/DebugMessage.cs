using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMessage : MonoBehaviour
{
    public static DebugMessage Instance { get; private set; }

    public TextMeshProUGUI displayText; // Assign this in the inspector

    // Make sure there's only one UIManager instance - Singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object alive when loading new scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this method to update the display text
    public void UpdateDisplayText(string message)
    {
        if (displayText != null)
        {
            displayText.text = message;
        }
        else
        {
            Debug.LogWarning("Display Text not set on UIManager.");
        }
    }
}
