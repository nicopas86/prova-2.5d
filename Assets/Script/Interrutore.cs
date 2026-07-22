using System.Collections;
using UnityEngine;

public class Interruttore : MonoBehaviour
{
    // trascina qui il "Porta_Hinge" (l'oggetto vuoto padre)
    public GameObject portaHinge;
    public float angoloApertura = 90f;
    public float velocitaApertura = 5f;

    private Quaternion rotazioneChiusa;
    private Quaternion rotazioneAperta;
    private Coroutine animazionePorta;

    void Start()
    {
        if (portaHinge != null)
        {
            rotazioneChiusa = portaHinge.transform.localRotation;
            // Calcoliamo la rotazione finale aggiungendo l'angolo sulla Y
            rotazioneAperta = rotazioneChiusa * Quaternion.Euler(0, angoloApertura, 0);
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopPorta();
            animazionePorta = StartCoroutine(MuoviPorta(rotazioneAperta));
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopPorta();
            animazionePorta = StartCoroutine(MuoviPorta(rotazioneChiusa));
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Coroutine per ruotare la porta in modo fluido
    IEnumerator MuoviPorta(Quaternion targetRot)
    {
        while (Quaternion.Angle(portaHinge.transform.localRotation, targetRot) > 0.1f)
        {
            portaHinge.transform.localRotation = Quaternion.Slerp(
                portaHinge.transform.localRotation,
                targetRot,
                Time.deltaTime * velocitaApertura
            );
            yield return null;
        }
        portaHinge.transform.localRotation = targetRot;
    }

    void StopPorta()
    {
        if (animazionePorta != null) StopCoroutine(animazionePorta);
    }
}