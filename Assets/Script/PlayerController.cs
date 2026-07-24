using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Riferimenti")]
    public Camera mainCamera;

    [Header("Sprite per le Direzioni")]
    public Sprite spriteUp;    
    public Sprite spriteDown;  
    public Sprite spriteLeft;    
    public Sprite spriteRight;  

    [Header("Impostazioni Movimento")]
    public float normalMoveSpeed = 5f;
    public float runMoveSpeed = 10f;

    // Componenti interni
    private Rigidbody rb;
    private SpriteRenderer mySpriteRenderer;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // Se la telecamera non è stata assegnata nell'Inspector, prova a cercarla
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        // Evitiamo che la fisica faccia ruotare lo sprite 3D
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        // Impostiamo lo sprite iniziale (Giù)
        if (mySpriteRenderer != null && spriteDown != null)
        {
            mySpriteRenderer.sprite = spriteDown;
        }
    }

    // Questa funzione viene invocata automaticamente dal componente "Player Input"
    public void OnMove(InputValue value)
    {
        // Legge contemporaneamente asse X e Y (supporta WASD, Frecce e Controller)
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        // 1. BILLBOARD: Mantiene lo sprite inclinato e orientato verso la Main Camera
        if (mainCamera != null)
        {
            Vector3 cameraRotation = mainCamera.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
        }

        // 2. CAMBIO SPRITE: Aggiorna l'immagine in base alla direzione in cui ci stiamo muovendo
        AggiornaSpriteDirezione();
    }

    void FixedUpdate()
    {
        // 3. MOVIMENTO FLUIDO CONTINUO: Applichiamo la velocità sul piano 3D (X, Z)
        if (rb != null)
        {
            // Controlliamo specificamente lo Shift Sinistro (o il tasto Shift generico)
            bool staCorrendo = Keyboard.current != null &&
                              (Keyboard.current.leftShiftKey.isPressed || Keyboard.current.shiftKey.isPressed);

            // Selezioniamo la velocità corretta
            float velocitaAttuale = staCorrendo ? runMoveSpeed : normalMoveSpeed;

            // Calcoliamo la velocità target
            Vector3 targetVelocity = new Vector3(moveInput.x * velocitaAttuale, rb.linearVelocity.y, moveInput.y * velocitaAttuale);

            rb.linearVelocity = targetVelocity;
        }
    }

    private void AggiornaSpriteDirezione()
    {
        if (mySpriteRenderer == null || moveInput == Vector2.zero) return;

        // Se ci stiamo muovendo prevalentemente in orizzontale (X)
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            if (moveInput.x > 0) CambiaSpriteDirezione("left");
            else if (moveInput.x < 0) CambiaSpriteDirezione("right");
        }
        // Se ci stiamo muovendo prevalentemente in verticale (Y)
        else
        {
            if (moveInput.y > 0) CambiaSpriteDirezione("up");
            else if (moveInput.y < 0) CambiaSpriteDirezione("down");
        }
    }

    void CambiaSpriteDirezione(string direzione)
    {
        switch (direzione.ToUpper())
        {
            case "UP":
                mySpriteRenderer.sprite = spriteUp;
                break;
            case "DOWN":
                mySpriteRenderer.sprite = spriteDown;
                break;
            case "LEFT":
                mySpriteRenderer.sprite = spriteLeft;
                break;
            case "RIGHT":
                mySpriteRenderer.sprite = spriteRight;
                break;
        }
    }
}
