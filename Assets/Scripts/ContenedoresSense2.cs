using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContenedoresSense2 : MonoBehaviour
{
    public GameObject[] ventiladores = new GameObject[5];
    public GameObject[] pilares = new GameObject[5];
    float[] pilaresPosY = new float[5];
    float[] pilaresPosYI = new float[5];
    public GameObject jugadorA, cont1, cont2, cont3, generadorDeRocas, roca, puerta, panelPuerta;
    public float cantidadMax, cantidadActual, offsetRoca;
    public bool generarRoca, abrirPuerta, dosContenedores, elevarPilar, desactivarVent;
    Vector3 posicionFinalPuerta;
    float rocaPos, posY, posYI;
    bool isDoneRoca, isDonePanelPuerta;

    private void Awake()
    {
        if (elevarPilar)
        {
            for (int i = 0; i < pilares.Length; i++)
            {
                if (pilares[i] != null)
                {
                    pilaresPosYI[i] = pilares[i].transform.position.y;
                    pilaresPosY[i] = pilares[i].transform.position.y + 2.91f;
                }
            }
        }

        posYI = transform.position.y;
        posY = transform.position.y - 0.05f;

        if (puerta != null)
        {
            posicionFinalPuerta = puerta.transform.position + new Vector3(0, 6, 0);
        }
    }
    
    void Update()
    {
        if (dosContenedores)
        {
            cantidadActual = (cont1.GetComponent<ContenedoresSense>().cantidadAct) + (cont2.GetComponent<ContenedoresSense>().cantidadAct);
        }

        else if (!dosContenedores)
        {
            cantidadActual = (cont1.GetComponent<ContenedoresSense>().cantidadAct) + (cont2.GetComponent<ContenedoresSense>().cantidadAct) + (cont3.GetComponent<ContenedoresSense>().cantidadAct);
        }
        
        if (cantidadActual >= cantidadMax)
        {
            if (generarRoca && generadorDeRocas != null)
            {
                if (!isDoneRoca)
                {
                    isDoneRoca = true;
                    GameObject tempRoca = Instantiate(roca, generadorDeRocas.transform.position, Quaternion.identity);
                    rocaPos = tempRoca.transform.position.y;
                    tempRoca.transform.DOMoveY(rocaPos + offsetRoca, 1);
                }
            }

            if (abrirPuerta && puerta != null)
            {
                puerta.transform.DOMove(posicionFinalPuerta, 4);
                if (!isDonePanelPuerta)
                {
                    Invoke("AbrirPanelPuerta", 0);
                    jugadorA.GetComponent<AspiradoraControlador>().audioPuertas.Play();
                }
                Invoke("CerrarPanelPuerta", 5);
            }

            if (desactivarVent)
            {
                for (int i = 0; i < ventiladores.Length; i++)
                {
                    ventiladores[i].GetComponent<Ventilador>().isActive = false;
                }
            }

            if (elevarPilar)
            {
                for (int i = 0; i < pilares.Length; i++)
                {
                    if (pilares[i] != null)
                    {
                        pilares[i].gameObject.transform.DOMoveY(pilaresPosY[i], 1);
                    }
                }
            }
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
}
