// MainMenuController.cs

using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada untuk mengelola scene

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject confirmationPanel;

    [Header("Scene To Load")]
    public string gameSceneName = "GameScene"; // Pastikan nama ini sama dengan file scene game Anda

    void Start()
    {
        // Pastikan panel konfirmasi tidak aktif saat mulai
        if(confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }

    // --- Fungsi untuk Tombol-Tombol UI ---

    /// <summary>
    /// Dipanggil oleh tombol Play.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    /// <summary>
    /// Dipanggil oleh tombol Quit utama untuk menampilkan panel konfirmasi.
    /// </summary>
    public void ShowQuitConfirmation()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Dipanggil oleh tombol "No" di panel konfirmasi.
    /// </summary>
    public void HideQuitConfirmation()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Dipanggil oleh tombol "Yes" di panel konfirmasi untuk keluar dari game.
    /// </summary>
    public void QuitGame()
    {
        // Pesan ini akan muncul di console Unity Editor saat Anda menguji
        Debug.Log("Quitting game...");

        // Fungsi ini hanya bekerja di build game yang sudah jadi, tidak di dalam Editor Unity
        Application.Quit();
    }
}