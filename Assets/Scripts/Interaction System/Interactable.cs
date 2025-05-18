using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
     // --- Variables ---
    [Tooltip("The outline component on this GameObject.")]
    private Outline outline; // Reference to the Quick Outline component

    [Tooltip("Message to display when the player looks at this object (e.g., 'Pickup Item (F)').")]
    public string message; // The prompt message for the UI

    [Tooltip("Event to invoke when the player interacts with this object.")]
    public UnityEvent onInteraction; // UnityEvent to call custom functions on interaction

    
    void Start()
    {
        
        outline = GetComponent<Outline>();
        if (outline == null)
        {
            Debug.LogWarning($"Interactable '{gameObject.name}' is missing an Outline component. Please add one.");
        }
        DisableOutline();
    }

   

    /// <summary>
    /// Enables the visual outline for this interactable object.
    /// </summary>
    public void EnableOutline()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    /// <summary>
    /// Disables the visual outline for this interactable object.
    /// </summary>
    public void DisableOutline()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    /// <summary>
    /// Called by the PlayerInteraction script when the player interacts with this object.
    /// Invokes the onInteraction UnityEvent.
    /// </summary>
    public void Interact()
    {
        onInteraction.Invoke();
    }
}
