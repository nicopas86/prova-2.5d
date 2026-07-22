using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInteraction : MonoBehaviour
{
    [Header("UI Prompt (Apri [E])")]
    public GameObject promptInterazione;

    [Header("Evento da scatenare")]
    public UnityEvent onInterazione;

    private bool inZona = false;

    void Start()
    {
        promptInterazione?.SetActive(false);
    }

    void Update()
    {
        if (inZona && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Invochiamo qualsiasi cosa sia stata collegata nell'Inspector
            onInterazione?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZona = true;
            promptInterazione?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZona = false;
            promptInterazione?.SetActive(false);
        }
    }
}
