// PlayerInventory.cs

using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Menggunakan HashSet untuk efisiensi dan untuk menghindari duplikat kunci
    private HashSet<string> collectedKeys = new HashSet<string>();

    /// <summary>
    /// Menambahkan ID kunci ke dalam daftar koleksi pemain.
    // </summary>
    public void AddKey(string keyId)
    {
        if (!collectedKeys.Contains(keyId))
        {
            collectedKeys.Add(keyId);
            Debug.Log("Mengambil kunci: " + keyId);
        }
    }

    /// <summary>
    /// Memeriksa apakah pemain sudah memiliki kunci dengan ID tertentu.
    /// </summary>
    public bool HasKey(string keyId)
    {
        return collectedKeys.Contains(keyId);
    }
}