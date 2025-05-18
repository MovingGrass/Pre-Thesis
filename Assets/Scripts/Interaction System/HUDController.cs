using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    // --- Singleton Instance ---
    public static HUDController instance; // Singleton pattern

    // --- UI References (assign in Inspector) ---
    [SerializeField]
    [Tooltip("The TextMeshProUGUI element for displaying interaction prompts.")]
    private TMP_Text interactionText;

    // --- Unity Methods ---
    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Another instance of HUDController found. Destroying this one.");
            Destroy(gameObject);
            return;
        }

        // Ensure interactionText is assigned
        if (interactionText == null)
        {
            Debug.LogError("InteractionText not assigned in HUDController. Please assign it in the Inspector.");
            enabled = false; // Disable script if not set up
            return;
        }
        interactionText.gameObject.SetActive(false); // Hide text by default
    }

    

    /// <summary>
    /// Enables the interaction prompt text and sets its content.
    /// </summary>
    /// <param name="text">The message to display.</param>
    public void EnableInteractionText(string text)
    {
        if (interactionText != null)
        {
            interactionText.text = text + " (F)"; // Appends "(F)" to indicate interact key
            interactionText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Disables (hides) the interaction prompt text.
    /// </summary>
    public void DisableInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}
