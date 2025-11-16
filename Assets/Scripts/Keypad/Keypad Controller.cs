// KeypadController.cs
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections; // Diperlukan untuk Coroutines

public class KeypadController : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Panel UI utama untuk keypad.")]
    public GameObject keypadPanel;
    [Tooltip("Teks untuk menampilkan input.")]
    public TMP_Text displayText;

    [Header("Keypad Settings")]
    [Tooltip("Kode yang benar untuk keypad ini.")]
    public string correctCode = "1234";
    [Tooltip("Batas maksimal digit yang bisa dimasukkan.")]
    public int maxDigits = 8;
    [Header("Audio")]
    public AudioClip keyPressSound;
    public AudioClip correctCodeSound;
    public AudioClip wrongCodeSound;
    private AudioSource audioSource;
    [Header("Feedback Colors")]
    public Color defaultColor = Color.black;
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;

    [Header("Events")]
    [Tooltip("Event yang akan dipanggil saat kode yang dimasukkan benar.")]
    public UnityEvent onCorrectCode;

    // Variabel internal
    private string currentInput = "";
    private bool isKeypadActive = false;
    private FPController playerMouseLook;
    private PlayerMovement playerMovement;

    void Awake() 
    {
        // Dapatkan komponen AudioSource dari objek ini
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Tambahkan AudioSource jika belum ada, untuk mencegah error
            Debug.LogWarning("KeypadController: Tidak ada AudioSource ditemukan. Menambahkan satu secara otomatis.");
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
    void Start()
    {
        playerMouseLook = FindObjectOfType<FPController>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (keypadPanel != null)
        {
            keypadPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Tombol Escape sekarang juga berfungsi sebagai tombol 'Exit'
        if (isKeypadActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeypad();
        }
    }

    // --- Fungsi Publik untuk Tombol UI ---

    /// <summary>
    /// Menambahkan digit ke input. Dipanggil oleh tombol angka 0-9.
    /// </summary>
    public void AddDigit(string digit)
    { 
        PlaySound(keyPressSound);
        if (currentInput.Length < maxDigits)
        {
            
            currentInput += digit;
            UpdateDisplay();
        }
    }

    /// <summary>
    /// Menghapus semua input. Dipanggil oleh tombol 'Clear'.
    /// </summary>
    public void ClearInput()
    {
        PlaySound(keyPressSound);
        currentInput = "";
        UpdateDisplay();
    }

    /// <summary>
    /// Memeriksa kode. Dipanggil oleh tombol 'Execute'.
    /// </summary>
    public void CheckCode()
    {
        PlaySound(keyPressSound);
        if (currentInput == correctCode)
        {
            StartCoroutine(ProcessCorrectCode());
        }
        else
        {
            StartCoroutine(ProcessWrongCode());
        }
    }

    // --- Coroutines untuk Feedback ---

    private IEnumerator ProcessCorrectCode()
    {
        PlaySound(correctCodeSound);
        displayText.text = "CORRECT";
        displayText.color = correctColor;
        onCorrectCode.Invoke(); // Panggil event unlock pintu, dll.

        yield return new WaitForSeconds(2f); // Tunggu 2 detik

        CloseKeypad();
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator ProcessWrongCode()
    {
        PlaySound(wrongCodeSound);
        displayText.text = "INCORRECT";
        displayText.color = incorrectColor;

        yield return new WaitForSeconds(2f); // Tunggu 2 detik

        currentInput = ""; // Reset input
        UpdateDisplay();   // Kembali ke tampilan default
    }


    // --- Fungsi untuk Mengontrol Status Keypad dan Player ---

    public void OpenKeypad()
    {
        isKeypadActive = true;
        keypadPanel.SetActive(true);

        // Reset tampilan
        currentInput = "";
        UpdateDisplay();

        // Bebaskan cursor dan matikan kontrol player
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (playerMouseLook != null) playerMouseLook.enabled = false;
        if (playerMovement != null) playerMovement.enabled = false;
        HUDController.instance.DisableInteractionText();
    }

    /// <summary>
    /// Dipanggil oleh tombol 'Exit'.
    /// </summary>
    public void CloseKeypad()
    {
        isKeypadActive = false;
        keypadPanel.SetActive(false);

        // Kembalikan kontrol ke player
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (playerMouseLook != null) playerMouseLook.enabled = true;
        if (playerMovement != null) playerMovement.enabled = true;
    }

    private void UpdateDisplay()
    {
        if (displayText != null)
        {
            displayText.text = currentInput;
            displayText.color = defaultColor;
        }
    }
}