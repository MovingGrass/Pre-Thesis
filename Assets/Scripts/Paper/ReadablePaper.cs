// ReadablePaper.cs

using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ReadablePaper : MonoBehaviour
{
    [Tooltip("Asset gambar (Sprite) dari kertas yang akan ditampilkan di UI.")]
    public Sprite paperImage;

    
    public void ShowPaper()
    {
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