// PuzzleBook.cs

using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PuzzleBook : MonoBehaviour
{
    [Tooltip("Huruf yang ada di buku ini. Atur di Inspector.")]
    public char letter;

    // Referensi ke manajer puzzle
    private BookPuzzleManager puzzleManager;

    void Start()
    {
        // Temukan manajer puzzle di parent object
        puzzleManager = GetComponentInParent<BookPuzzleManager>();
        if (puzzleManager == null)
        {
            Debug.LogError("PuzzleBook tidak bisa menemukan BookPuzzleManager di parent-nya!");
        }
    }

    /// <summary>
    /// Fungsi ini akan dipanggil oleh UnityEvent dari Interactable.cs
    /// </summary>
    public void OnBookClicked()
    {
        // Beritahu manajer bahwa buku ini telah dipilih
        puzzleManager.SelectBook(this);
        Debug.Log("clicked");
    }
}