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

    private bool isOutlineLocked = false;
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
        if (outline != null && !isOutlineLocked)
        {
            outline.enabled = true;
        }
    }

    /// <summary>
    /// Disables the visual outline for this interactable object.
    /// </summary>
    public void DisableOutline()
    {
        if (outline != null && !isOutlineLocked)
        {
            outline.enabled = false;
        }
    }

    public void SetPersistentOutline(bool active, Color color)
    {
        isOutlineLocked = active;
        if (outline != null)
        {
            if (active)
            {
                outline.OutlineColor = color;
                outline.enabled = true;
            }
            else
            {
                // Saat kunci dilepas, matikan outline.
                // PlayerInteraction akan menyalakannya lagi jika crosshair masih di atasnya.
                outline.enabled = false;
                // Kembalikan warna default untuk hover berikutnya
                outline.OutlineColor = Color.white;
            }
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
