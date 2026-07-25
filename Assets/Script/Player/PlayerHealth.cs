using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Impostazioni Vita")]
    public int maxHealth = 6; // 6 HP = 3 Cuori interi
    public int currentHealth;

    [Header("Riferimento UI")]
    public UIHealthManager healthUI;

    void Start()
    {
        currentHealth = maxHealth;

        // Inizializza l'HUD dei cuori all'avvio
        if (healthUI != null)
        {
            healthUI.UpdateHealthUI(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Aggiorna la grafica dei cuori
        if (healthUI != null)
        {
            healthUI.UpdateHealthUI(currentHealth, maxHealth);
        }

        Debug.Log($"Lama ha subito {amount} danni! Vita rimasta: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Il Lama è stato sconfitto!");
            // Qui potrai inserire il Game Over o il respawn
        }
    }
}
