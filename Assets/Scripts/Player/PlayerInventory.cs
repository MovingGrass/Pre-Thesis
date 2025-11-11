// PlayerInventory.cs

using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Menggunakan HashSet untuk efisiensi dan untuk menghindari duplikat kunci
    private string heldKeyId = null;


    /// <summary>   
    /// Menambahkan ID kunci ke dalam daftar koleksi pemain.
    // </summary>
    public bool AddKey(string keyId)
    {
        // Cek apakah pemain sudah memegang kunci
        if (HasAnyKey())
        {
            // Gagal, karena sudah ada kunci
            return false;
        }

        // Berhasil, simpan ID kunci
        heldKeyId = keyId;
        Debug.Log("Mengambil kunci: " + keyId);
        return true;
    }

    /// <summary>
    /// Memeriksa apakah pemain sudah memiliki kunci dengan ID tertentu.
    /// </summary>
     public bool HasKey(string keyId)
    {
        // Cek apakah ID kunci yang dipegang sama dengan yang dibutuhkan
        return heldKeyId == keyId;
    }

    /// <summary>
    /// Memeriksa apakah pemain sedang memegang kunci apa pun.
    /// </summary>
    public bool HasAnyKey()
    {
        // Jika heldKeyId tidak null (kosong), berarti pemain punya kunci
        return heldKeyId != null;
    }
}