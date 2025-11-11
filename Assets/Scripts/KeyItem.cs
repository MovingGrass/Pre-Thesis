// KeyItem.cs (Kode Lengkap)

using UnityEngine;
using System.Collections; // Wajib ada untuk Coroutine

[RequireComponent(typeof(Interactable))]
public class KeyItem : MonoBehaviour
{
    [Header("Key Settings")]
    [Tooltip("ID unik untuk kunci ini. Kunci asli harus cocok dengan 'Required Key Id' di pintu.")]
    public string keyId;

    [Header("Feedback Messages")]
    [Tooltip("Pesan yang muncul jika pemain mencoba mengambil kunci ini saat sudah punya kunci lain.")]
    public string alreadyHoldingKeyMessage = "You can only take 1 key.";

    // Referensi ke komponen Interactable di objek ini
    private Interactable interactable;

    void Awake()
    {
        // Ambil referensi komponen saat game dimulai
        interactable = GetComponent<Interactable>();
    }

    /// <summary>
    /// Fungsi ini dipanggil oleh UnityEvent dari Interactable.cs saat pemain menekan 'F'.
    /// </summary>
    public void PickupKey()
    {
        // Cari inventory pemain di scene
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory tidak ditemukan di scene!");
            return;
        }

        // Coba tambahkan kunci ke inventory
        if (playerInventory.AddKey(keyId))
        {
            // --- BERHASIL DIAMBIL ---
            // Sembunyikan pesan interaksi dari HUD
            HUDController.instance.DisableInteractionText();
            // Hancurkan objek kunci dari scene
            Destroy(gameObject);
        }
        else
        {
            // --- GAGAL DIAMBIL (karena pemain sudah punya kunci lain) ---
            // Tampilkan pesan sementara di HUD
            StartCoroutine(ShowTemporaryMessage(alreadyHoldingKeyMessage, 2.5f));
        }
    }

    /// <summary>
    /// Coroutine untuk menampilkan pesan di HUD selama durasi tertentu.
    /// </summary>
    private IEnumerator ShowTemporaryMessage(string message, float duration)
    {
        // Tampilkan pesan sementara
        HUDController.instance.EnableInteractionText(message);
        interactable.DisableOutline(); // Sembunyikan outline sementara

        yield return new WaitForSeconds(duration);

        // Setelah pesan selesai, kembalikan ke pesan interaksi normal
        // jika pemain masih melihat ke arah objek kunci ini.
        if (PlayerInteraction.instance != null && PlayerInteraction.instance.GetCurrentInteractable() == this.interactable)
        {
            HUDController.instance.EnableInteractionText(interactable.message);
            interactable.EnableOutline();
        }
    }
}