// ReadablePaper.cs

using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ReadablePaper : MonoBehaviour
{
    [Tooltip("Asset gambar (Sprite) dari kertas yang akan ditampilkan di UI.")]
    public Sprite paperImage;
    [Header("Audio")]
    [Tooltip("Suara yang diputar saat kertas ini dibaca.")]
    public AudioClip readSound;
    
    public void ShowPaper()
    {
        if (readSound != null)
        {
            // Putar suara di posisi kertas ini
            AudioSource.PlayClipAtPoint(readSound, transform.position);
        }
        if (paperImage != null && UIPopupController.instance != null)
        {
            UIPopupController.instance.ShowPopup(paperImage);
            HUDController.instance.DisableInteractionText();
        }
        else
        {
            Debug.LogWarning("Paper Image atau UIPopupController tidak ditemukan!");
        }
    }
}