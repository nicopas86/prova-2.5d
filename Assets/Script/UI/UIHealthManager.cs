using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Sprite dei Cuori")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Riferimenti UI")]
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartContainer;

    // Lista per tenere traccia delle immagini create a schermo
    private List<Image> spawnedHearts = new List<Image>();

    /// <summary>
    /// Aggiorna o rigenera la UI dei cuori.
    /// Ogni cuore vale 2 HP (Pieno = 2, Mezzo = 1, Vuoto = 0).
    /// </summary>
   

    private void Start()
    {
        // Esempio: 5 HP attuali su 6 Max (mostrerà 2 cuori pieni e 1 mezzo)
        UpdateHealthUI(6, 6);
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        // Calcola quanti cuori servono in totale (es. 5 HP max = 3 cuori)
        int totalHeartsNeeded = Mathf.CeilToInt(maxHealth / 2f);

        // Se servono più cuori (es. hai aumentato la vita max), li crea
        while (spawnedHearts.Count < totalHeartsNeeded)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartContainer);
            spawnedHearts.Add(newHeart.GetComponent<Image>());
        }

        // Se la vita max dovesse ridursi, rimuove i cuori in eccesso
        while (spawnedHearts.Count > totalHeartsNeeded)
        {
            Destroy(spawnedHearts[spawnedHearts.Count - 1].gameObject);
            spawnedHearts.RemoveAt(spawnedHearts.Count - 1);
        }

        // Assegna lo sprite corretto ad ogni cuore
        for (int i = 0; i < spawnedHearts.Count; i++)
        {
            int heartHPThreshold = (i + 1) * 2;

            if (currentHealth >= heartHPThreshold)
            {
                // Cuore Pieno
                spawnedHearts[i].sprite = fullHeart;
            }
            else if (currentHealth == heartHPThreshold - 1)
            {
                // Mezzo Cuore
                spawnedHearts[i].sprite = halfHeart;
            }
            else
            {
                // Cuore Vuoto
                spawnedHearts[i].sprite = emptyHeart;
            }
        }
    }
}
