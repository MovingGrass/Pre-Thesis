// KeyItem.cs

using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class KeyItem : MonoBehaviour
{
    [Tooltip("ID unik untuk kunci ini. Harus cocok dengan 'Required Key Id' di DoorController.")]
    public string keyId;

    /// <summary>
    /// Fungsi ini akan dipanggil oleh UnityEvent dari Interactable.cs
    /// </summary>
    public void PickupKey()
    {
        // Cari inventory pemain di scene
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

        if (playerInventory != null)
        {
            // Tambahkan kunci ini ke inventory pemain
            playerInventory.AddKey(keyId);

            // Sembunyikan pesan interaksi dari HUD
            HUDController.instance.DisableInteractionText();

            // Hancurkan objek kunci dari scene
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("PlayerInventory tidak ditemukan di scene! Pasang script PlayerInventory pada objek player.");
        }
    }
}