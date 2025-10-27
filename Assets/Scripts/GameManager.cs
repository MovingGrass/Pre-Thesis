// GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement; // Diperlukan untuk memuat scene

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject confirmationPanel;

    [Header("Player Controllers")]
    // Kita akan set ini secara otomatis
    private FPController playerMouseLook;
    private PlayerMovement playerMovement;

    // State Management
    public static bool isGamePaused = false;
    public static bool isUIOpen = false; // <-- KUNCI untuk mengatasi konflik ESC

    void Awake()
    {
        // Singleton setup
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Cari controller player untuk dinonaktifkan saat pause
        playerMouseLook = FindObjectOfType<FPController>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Pastikan semua panel UI tidak aktif di awal
        pauseMenuPanel.SetActive(false);
        confirmationPanel.SetActive(false);
        Time.timeScale = 1f; // Pastikan game berjalan normal saat dimulai
        isGamePaused = false;
        isUIOpen = false;
    }

    void Update()
    {
        // Periksa input 'Escape'
        // HANYA jika tidak ada UI lain yang sedang terbuka
        if (Input.GetKeyDown(KeyCode.Escape) && !isUIOpen)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isGamePaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu di game (fisika, animasi, dll)
        FreezePlayer(true);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        pauseMenuPanel.SetActive(false);
        confirmationPanel.SetActive(false); // Pastikan panel konfirmasi juga tertutup
        Time.timeScale = 1f; // Mengembalikan waktu ke normal
        FreezePlayer(false);
    }

    // --- Fungsi untuk Tombol-Tombol UI ---

    public void OnClick_ExitButton()
    {
        // Sembunyikan menu pause utama dan tampilkan panel konfirmasi
        pauseMenuPanel.SetActive(false);
        confirmationPanel.SetActive(true);
    }

    public void OnClick_ConfirmExit_No()
    {
        // Kembali ke menu pause
        confirmationPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void OnClick_ConfirmExit_Yes()
    {
        // PENTING: Kembalikan Time.timeScale sebelum pindah scene
        Time.timeScale = 1f;
        // Ganti "MainMenu" dengan nama scene menu utama Anda
        SceneManager.LoadScene("MainMenu");
    }

    // Fungsi helper untuk membekukan/melepaskan player
    private void FreezePlayer(bool freeze)
    {
        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (playerMouseLook != null) playerMouseLook.enabled = false;
            if (playerMovement != null) playerMovement.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (playerMouseLook != null) playerMouseLook.enabled = true;
            if (playerMovement != null) playerMovement.enabled = true;
        }
    }
}