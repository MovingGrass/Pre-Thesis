using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // --- Variables ---
    [Tooltip("How far the player can reach to interact with objects.")]
    public float playerReach = 3f; // Max distance for interaction

    private Interactable currentInteractable; // The interactable object currently being looked at
    private Camera playerCamera; // Reference to the player's camera

    
    void Start()
    {
        playerCamera = Camera.main; // Assumes the main camera is the player's view
        if (playerCamera == null)
        {
            Debug.LogError("PlayerInteraction: Main camera not found! Please tag your player camera as 'MainCamera'.");
            enabled = false; // Disable script if no camera
        }
    }

    void Update()
    {
        CheckInteraction();

        // Check for interaction input (e.g., F key)
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    

    /// <summary>
    /// Checks what the player is looking at and updates the currentInteractable.
    /// </summary>
    private void CheckInteraction()
    {
        if (playerCamera == null) return;

        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Interactable newInteractable = null; // The interactable detected in this frame

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            // Check if the hit object has an "Interactable" tag
            if (hit.collider.CompareTag("Interactable"))
            {
                // Try to get the Interactable component from the hit object
                newInteractable = hit.collider.GetComponentInParent<Interactable>();
            }
        }

        // --- Logic to handle changing focus ---
        
        // If we are looking at a new interactable (or null) and it's different from the current one
        if (newInteractable != currentInteractable)
        {
            // If there was a previously focused interactable, disable its outline and UI
            if (currentInteractable != null)
            {
                currentInteractable.DisableOutline();
                HUDController.instance.DisableInteractionText(); // Assuming HUDController singleton
            }

            currentInteractable = newInteractable; // Update the current interactable

            // If the new interactable is not null (i.e., we are looking at something interactable)
            if (currentInteractable != null)
            {
                // Check if the interactable itself is enabled (MonoBehaviour.enabled)
                if (currentInteractable.enabled)
                {
                    currentInteractable.EnableOutline();
                    HUDController.instance.EnableInteractionText(currentInteractable.message); // Show its message
                }
                else // If the interactable component is disabled, treat it as not interactable
                {
                    currentInteractable = null; // Clear current interactable
                    HUDController.instance.DisableInteractionText();
                }
            }
        }
    }
}
