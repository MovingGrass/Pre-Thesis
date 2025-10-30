// GameCompletionManager.cs (Versi Diperbarui dengan "Press Any Key")

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Wajib ada untuk Coroutine

public class GameCompletionManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject completionPanel;
    public TMP_Text timeCompletionText;
    public TMP_Text pressAnyKeyText; // <-- BARU: Referensi untuk teks yang berkedip

    [Header("Scene Management")]
    public string mainMenuSceneName = "MainMenu";

    private float startTime;
    private bool gameCompleted = false;
    private Coroutine blinkingCoroutine; // <-- BARU: Untuk menyimpan referensi coroutine

    void Start()
    {
        startTime = Time.time;
        completionPanel.SetActive(false);
    }

    // <-- BARU: Fungsi Update untuk mendeteksi input
    void Update()
    {
        // Hanya jalankan jika game sudah selesai dan ada input tombol/mouse
        if (gameCompleted && Input.anyKeyDown)
        {
            // Hentikan coroutine sebelum pindah scene untuk menghindari error
            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine);
            }
            GoToMainMenu();
        }
    }

    public void CompleteGame()
    {
        if (gameCompleted) return;
        gameCompleted = true;

        FreezePlayer(true);

        float elapsedTime = Time.time - startTime;
        int hours = (int)(elapsedTime / 3600);
        int minutes = (int)((elapsedTime % 3600) / 60);
        int seconds = (int)(elapsedTime % 60);
        
        string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        
        timeCompletionText.text = timeString;
        completionPanel.SetActive(true);
        GameManager.isUIOpen = true;

        // Mulai coroutine untuk teks berkedip
        blinkingCoroutine = StartCoroutine(BlinkText());
    }

    // <-- DIUBAH: Fungsi ini sekarang private karena tidak dipanggil oleh tombol
    private void GoToMainMenu()
    {
        GameManager.isUIOpen = false;
        Time.timeScale = 1f; // Pastikan waktu kembali normal sebelum pindah scene
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // <-- BARU: Coroutine untuk efek fade in/out
    private IEnumerator BlinkText()
    {
        while (true) // Loop selamanya sampai dihentikan
        {
            // Fade out
            yield return StartCoroutine(FadeTextTo(0.0f, 0.7f));
            // Fade in
            yield return StartCoroutine(FadeTextTo(1.0f, 0.7f));
        }
    }

    private IEnumerator FadeTextTo(float targetAlpha, float duration)
    {
        Color color = pressAnyKeyText.color;
        float startAlpha = color.a;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, progress);
            pressAnyKeyText.color = color;
            yield return null; // Tunggu frame berikutnya
        }

        color.a = targetAlpha;
        pressAnyKeyText.color = color;
    }

    private void FreezePlayer(bool freeze)
    {
        FPController playerMouseLook = FindObjectOfType<FPController>();
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

        if (freeze)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (playerMouseLook != null) playerMouseLook.enabled = false;
            if (playerMovement != null) playerMovement.enabled = false;
        }
    }
}