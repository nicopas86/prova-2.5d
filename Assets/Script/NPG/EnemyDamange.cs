using UnityEngine;

public class EnemyDamange : MonoBehaviour
{
    [Header("Damage")]
    [Tooltip("1 = Mezzo cuore, 2 = 1 Cuore intero")]
    public int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che entra nel trigger è il Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
