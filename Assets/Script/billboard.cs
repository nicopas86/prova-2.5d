using System.Collections;
using UnityEngine;

public class billboard : MonoBehaviour
{

    [Header("Sprite per le Direzioni")]
    // Trascina qui i tuoi PNG nell'Inspector!
    public Sprite spriteGiù;   // Il PNG originale
    public Sprite spriteSu;    // Il PNG visto da dietro
    // Nota: Per Destra/Sinistra useremo lo stesso spriteGiù, ma faremo il "Flip"

    public float moveSpeed = 5f; // Velocità di movimento
    private bool isMoving = false; // Stato del movimento

    public LayerMask obstacleLayer; // Selezioneremo "Obstacles" nell'Inspector

    // Componente SpriteRenderer sulla lama
    private SpriteRenderer mySpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Otteniamo il componente SpriteRenderer all'avvio
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // Impostiamo lo sprite iniziale (quello che guarda verso il basso)
        if (mySpriteRenderer != null && spriteGiù != null)
        {
            mySpriteRenderer.sprite = spriteGiù;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        // Otteniamo la rotazione della camera in gradi (0-360)
        Vector3 cameraRotation = Camera.main.transform.eulerAngles;

        // Applichiamo la rotazione: 
        // X = 90 (per stare dritti), Y = quella della camera, Z = 0
        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);

        if (!isMoving) 
        {

            Vector3 inputDir = Vector3.zero;

            // Rileviamo la direzione dell'input
            if (Input.GetKey(KeyCode.W))
            {
                inputDir = new Vector3(0, 0, 1);  // Avanti (Su)
                CambiaSpriteDirezione("Su");
            }
            else if (Input.GetKey(KeyCode.S))
            {
                inputDir = new Vector3(0, 0, -1); // Indietro (Giù)
                CambiaSpriteDirezione("Giù");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                inputDir = new Vector3(-1, 0, 0); // Sinistra
                CambiaSpriteDirezione("Sinistra");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                inputDir = new Vector3(1, 0, 0);  // Destra
                CambiaSpriteDirezione("Destra");
            }
            // Se abbiamo un input e la strada è libera, partiamo!
            if (inputDir != Vector3.zero)
            {
                Vector3 targetPos = transform.position + inputDir;

                if (!IsPathBlocked(targetPos))
                {
                    StartCoroutine(MovePlayer(targetPos));
                }
            }

        }
    }

    IEnumerator MovePlayer(Vector3 targetPos)
    {
        isMoving = true;

        // Finché la distanza tra la posizione attuale e quella target è maggiore di un soffio...
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // Muovi la posizione verso il target in base alla velocità
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            // Aspetta il prossimo frame prima di continuare il ciclo
            yield return null;
        }

        // Una volta arrivati, fissiamo la posizione precisa e liberiamo il movimento
        transform.position = targetPos;
        isMoving = false;
    }

    bool IsPathBlocked(Vector3 targetPos)
    {
        // Creiamo una piccola sfera invisibile nel punto di destinazione (alzata di 0.5f)
        // Se tocca qualcosa nel layer degli ostacoli, restituisce true
        return Physics.CheckSphere(targetPos + new Vector3(0, 0.5f, 0), 0.2f, obstacleLayer);
    }

    // Funzione dedicata a scambiare gli sprite e gestire il Flip
    void CambiaSpriteDirezione(string direzione)
    {
        if (mySpriteRenderer == null) return;

        switch (direzione)
        {
            case "Su":
                mySpriteRenderer.sprite = spriteSu;   // Cambiamo PNG
                mySpriteRenderer.flipX = false;
                break;
            case "Giù":
                mySpriteRenderer.sprite = spriteGiù;  // PNG originale
                mySpriteRenderer.flipX = false;
                break;
            case "Sinistra":
                // mySpriteRenderer.sprite = spriteGiù;  // Usiamo il PNG "Giù" (o creane uno specifico)
                mySpriteRenderer.flipX = true;       // Lo specchiamo
                break;
            case "Destra":
                //mySpriteRenderer.sprite = spriteGiù;  // Usiamo il PNG "Giù"
                mySpriteRenderer.flipX = false;      // Non specchiato
                break;
        }
    }
}
