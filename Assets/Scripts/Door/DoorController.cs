using System.Collections;
using UnityEngine;

// Pastikan object memiliki komponen Interactable
[RequireComponent(typeof(Interactable))]
public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public bool isLocked = true;    
    public bool needsKey = false;
    public string requiredKeyId = "";
    public bool isOpen = false;

    [Header("Interaction Messages")]
    [Tooltip("Pesan saat pintu bisa dibuka.")]
    public string openMessage = "Open";
    [Tooltip("Pesan saat pintu terkunci.")]
    public string lockedMessage = "Locked";
    [Tooltip("Pesan saat pintu sudah terbuka.")]
    public string closeMessage = "Close";
    public string unlockedMessage = "Unlocked";
    private PlayerInventory playerInventory;
    
    private Interactable interactable;
    private Animator animator; 

    void Awake()
    {
        interactable = GetComponent<Interactable>();
        animator = GetComponent<Animator>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        UpdateInteractionMessage();
    }

    /// <summary>
    /// Fungsi ini akan dipanggil oleh UnityEvent dari Interactable.cs
    /// </summary>
    public void OnDoorInteract()
    {
        if (isOpen)
        {
            CloseDoor();
            return;
        }

        if (isLocked)
        {
            if (needsKey && playerInventory != null && playerInventory.HasKey(requiredKeyId))
            {
                UnlockDoor();
            }
            else
            {
                // Jika tidak punya kunci, tampilkan pesan terkunci
                StartCoroutine(ShowTemporaryMessage(lockedMessage, 2f));
            }
        }
        else
        {
            OpenDoor();
        }
    }

    /// <summary>
    /// Fungsi untuk membuka kunci pintu (bisa dipanggil dari keypad, puzzle, dll.)
    /// </summary>
    public void UnlockDoor()
    {
        isLocked = false;
        Debug.Log("Pintu berhasil dibuka!");
        UpdateInteractionMessage();
    }

    private void CloseDoor()
    {
        isOpen = false;
        if (animator != null)
        {
            animator.SetBool("IsOpen", false);
        }
        UpdateInteractionMessage();
    }

    private void OpenDoor()
    {
        isOpen = true;
        if (animator != null)
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        UpdateInteractionMessage();
    }

    /// <summary>
    /// Mengubah pesan pada komponen Interactable berdasarkan status pintu.
    /// </summary>
    private void UpdateInteractionMessage()
    {
        if (isLocked)
        {
            interactable.message = "Door";
        }
        else
        {
            interactable.message = isOpen ? closeMessage : openMessage;
        }
    }

    /// <summary>
    /// Menampilkan pesan di HUD untuk durasi tertentu.
    /// </summary>
    private IEnumerator ShowTemporaryMessage(string message, float duration)
    {
        HUDController.instance.EnableInteractionText(message);
        interactable.DisableOutline();
        yield return new WaitForSeconds(duration);

        // Setelah pesan sementara selesai, tampilkan pesan interaksi yang seharusnya
        HUDController.instance.EnableInteractionText(interactable.message);
        interactable.EnableOutline();
    }
}