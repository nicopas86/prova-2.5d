using UnityEngine;

public class UIVisualizer : MonoBehaviour
{
    
    [Header("Pannello da controllare")]
    public GameObject pannelloUI;

    [Header("Impostazioni")]
    public bool mettiInPausa = true;

    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pannelloUI != null) pannelloUI.SetActive(false);
    }

    public void ToggleSchermata()
    {
        if (isOpen) ChiudiSchermata();
        else ApriSchermata();
    }

    public void ApriSchermata()
    {
        if (pannelloUI == null) return;

        pannelloUI.SetActive(true);
        isOpen = true;

        if (mettiInPausa) Time.timeScale = 0f;
    }

    public void ChiudiSchermata()
    {
        if (pannelloUI == null) return;

        pannelloUI.SetActive(false);
        isOpen = false;

        if (mettiInPausa) Time.timeScale = 1f;
    }
}
