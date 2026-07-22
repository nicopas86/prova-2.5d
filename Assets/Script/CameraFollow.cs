using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [Header("Target da seguire")]
    public Transform target; // Trascina qui il tuo lama dall'Inspector

    [Header("Impostazioni")]
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Mantiene la telecamera a distanza Z

    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f; // Più è basso, più l'inseguimento sarà morbido e fluido

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcoliamo la posizione in cui la telecamera *dovrebbe* essere
            Vector3 desiredPosition = target.position + offset;

            // Interpolazione lineare (Lerp) per rendere il movimento fluido
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Applichiamo la posizione
            transform.position = smoothedPosition;
        }
    }
}
