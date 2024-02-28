using UnityEngine;

public class TeleportationPowerUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playermovement playerController = other.gameObject.GetComponent<playermovement>();
            if (playerController != null)
            {
                playerController.CollectTeleportPowerUp();
                Destroy(gameObject); // Destroy the power-up object after collecting
            }
        }
    }
}