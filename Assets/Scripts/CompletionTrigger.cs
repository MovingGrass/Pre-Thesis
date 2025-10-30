// CompletionTrigger.cs
using UnityEngine;

public class CompletionTrigger : MonoBehaviour
{
    public GameCompletionManager completionManager;

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang masuk adalah player (asumsi player punya tag "Player")
        if (other.CompareTag("Player"))
        {
            completionManager.CompleteGame();
        }
    }
}