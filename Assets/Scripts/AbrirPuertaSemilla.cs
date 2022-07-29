using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AbrirPuertaSemilla : MonoBehaviour
{
    Vector3 posicionFinalPuerta;
    public GameObject jugadorA, puerta, panelPuerta;
    bool isDonePanelPuerta;

    private void Awake()
    {
        if (puerta != null)
        {
            posicionFinalPuerta = puerta.transform.position + new Vector3(0, 6, 0);
        }
    }

    public void CerrarPanelPuerta()
    {
        panelPuerta.SetActive(false);
    }

    public void AbrirPanelPuerta()
    {
        isDonePanelPuerta = true;
        panelPuerta.SetActive(true);
    }

    private void OnDisable()
    {
        puerta.transform.DOMove(posicionFinalPuerta, 4);
        if (!isDonePanelPuerta)
        {
            Invoke("AbrirPanelPuerta", 0);
            jugadorA.GetComponent<AspiradoraControlador>().audioPuertas.Play();
        }
        Invoke("CerrarPanelPuerta", 5);
    }
}
