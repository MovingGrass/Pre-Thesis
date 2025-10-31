// BookPuzzleManager.cs

using UnityEngine;
using UnityEngine.Events; // Diperlukan untuk UnityEvent

public class BookPuzzleManager : MonoBehaviour
{
    [Tooltip("Seret 7 objek buku ke sini sesuai urutan mereka di rak dari kiri ke kanan.")]
    public PuzzleBook[] currentBookOrder;

    [Tooltip("Kata yang benar untuk menyelesaikan puzzle.")]
    public string correctWord = "LIBRARY";

    [Tooltip("Event yang akan dipicu saat puzzle selesai.")]
    public UnityEvent onPuzzleSolved;

    private PuzzleBook selectedBook1; // Menyimpan buku pertama yang diklik
    private bool puzzleIsSolved = false;
    [Tooltip("Warna outline saat sebuah buku dipilih.")]
    public Color selectionColor = Color.yellow;
    /// <summary>
    /// Dipanggil oleh PuzzleBook saat diklik.
    /// </summary>
    public void SelectBook(PuzzleBook book)
    {
        if (puzzleIsSolved) return; // Jika sudah selesai, jangan lakukan apa-apa

        if (selectedBook1 == null)
        {
            // Ini adalah buku pertama yang dipilih
            selectedBook1 = book;
            selectedBook1.GetComponent<Interactable>().SetPersistentOutline(true, selectionColor); // Beri highlight
        }
        else
        {
            selectedBook1.GetComponent<Interactable>().SetPersistentOutline(false, Color.white);
            // Ini adalah buku kedua, saatnya menukar
            if (selectedBook1 == book) // Jika mengklik buku yang sama lagi
            {
                // Batal memilih
                selectedBook1.GetComponent<Interactable>().DisableOutline();
                selectedBook1 = null;
            }
            else
            {
                // Tukar posisi buku
                SwapBooks(selectedBook1, book);

                // Reset pilihan
                selectedBook1.GetComponent<Interactable>().DisableOutline();
                selectedBook1 = null;

                // Periksa apakah puzzle sudah selesai
                CheckForWinCondition();
            }
        }
    }

    private void SwapBooks(PuzzleBook bookA, PuzzleBook bookB)
    {
        // Temukan posisi mereka di array
        int indexA = -1, indexB = -1;
        for (int i = 0; i < currentBookOrder.Length; i++)
        {
            if (currentBookOrder[i] == bookA) indexA = i;
            if (currentBookOrder[i] == bookB) indexB = i;
        }

        if (indexA != -1 && indexB != -1)
        {
            // Tukar posisi di dalam array
            PuzzleBook tempBook = currentBookOrder[indexA];
            currentBookOrder[indexA] = currentBookOrder[indexB];
            currentBookOrder[indexB] = tempBook;

            // Tukar posisi fisik di dunia game
            Vector3 tempPosition = bookA.transform.position;
            bookA.transform.position = bookB.transform.position;
            bookB.transform.position = tempPosition;
        }
    }

    private void CheckForWinCondition()
    {
        string currentWord = "";
        foreach (PuzzleBook book in currentBookOrder)
        {
            currentWord += book.letter;
        }

        if (currentWord == correctWord)
        {
            SolvePuzzle();
        }
    }

    private void SolvePuzzle()
    {
        puzzleIsSolved = true;
        Debug.Log("PUZZLE SELESAI! Kata yang benar terbentuk.");

        // Matikan interaksi semua buku
        foreach (PuzzleBook book in currentBookOrder)
        {
            book.GetComponent<Interactable>().enabled = false;
            book.GetComponent<Interactable>().DisableOutline();
        }

        // Panggil event (yang akan membuka pintu)
        onPuzzleSolved.Invoke();
    }
}