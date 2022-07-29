using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ContenedoresSense : MonoBehaviour
{
    public GameObject[] ventiladores = new GameObject[5];
    public GameObject[] pilares = new GameObject[5];
    float[] pilaresPosY = new float[5];
    float[] pilaresPosYI = new float[5];
    public Image barraContenedor;
    public float cantidadMax, cantidadAct, offsetRoca;
    public bool generarRoca, abrirPuerta, organ, plastico, papel, individual, elevarPilar, desactivarVent, activarPanel;
    public GameObject jugadorA, generadorDeRocas, puerta, roca, vfxBueno, panelTuto, panelPuerta, tablonRoca;
    float rocaPos, posY, posYI;
    bool isDoneRoca, isDonePanelPuerta;
    Vector3 posicionFinalPuerta;
    AudioSource audios;
    public AudioClip sonidoBasurasPop;

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

        audios = GetComponent<AudioSource>();

        if (puerta != null)
        {
            posicionFinalPuerta = puerta.transform.position + new Vector3(0, 6, 0);
        }
    }

    void Update()
    {
        barraContenedor.fillAmount = cantidadAct / cantidadMax;

        if (cantidadAct >= cantidadMax && individual)
        {
            if (generarRoca && generadorDeRocas != null)
            {
                if (!isDoneRoca)
                {
                    isDoneRoca = true;
                    roca.SetActive(true);
                    tablonRoca.SetActive(true);
                    roca.transform.DOMoveY(generadorDeRocas.transform.position.y + offsetRoca, 1);
                }
            }

            if (activarPanel && panelTuto != null)
            {
                panelTuto.SetActive(true);
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
    public void ApagarVFX()
    {
        vfxBueno.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Succionable" && cantidadAct < cantidadMax)
        {
            GameObject tempBasura = other.gameObject;

            if (organ && tempBasura.GetComponent<BasurasIdentificador>().idBasura == 0)
            {
                tempBasura.tag = "Untagged";
                tempBasura.layer = 0;
                cantidadAct += 1f;
                vfxBueno.SetActive(true);
                Invoke(nameof(ApagarVFX), 1);
                audios.clip = sonidoBasurasPop;
                audios.Play();
            }
            else if (organ && tempBasura.GetComponent<BasurasIdentificador>().idBasura != 0)
            {
                tempBasura.transform.position = tempBasura.GetComponent<BasurasIdentificador>().posInicial;
                tempBasura.transform.rotation = tempBasura.GetComponent<BasurasIdentificador>().rotacionInicial;
            }

            if (papel && tempBasura.GetComponent<BasurasIdentificador>().idBasura == 2)
            {
                tempBasura.tag = "Untagged";
                tempBasura.layer = 0;
                cantidadAct += 1f;
                vfxBueno.SetActive(true);
                Invoke(nameof(ApagarVFX), 1);
                audios.clip = sonidoBasurasPop;
                audios.Play();
            }
            else if (papel && tempBasura.GetComponent<BasurasIdentificador>().idBasura != 2)
            {
                tempBasura.transform.position = tempBasura.GetComponent<BasurasIdentificador>().posInicial;
                tempBasura.transform.rotation = tempBasura.GetComponent<BasurasIdentificador>().rotacionInicial;
            }

            if (plastico && tempBasura.GetComponent<BasurasIdentificador>().idBasura == 1)
            {
                tempBasura.tag = "Untagged";
                tempBasura.layer = 0;
                cantidadAct += 1f;
                vfxBueno.SetActive(true);
                Invoke(nameof(ApagarVFX), 1);
                audios.clip = sonidoBasurasPop;
                audios.Play();
            }
            else if (plastico && tempBasura.GetComponent<BasurasIdentificador>().idBasura != 1)
            {
                tempBasura.transform.position = tempBasura.GetComponent<BasurasIdentificador>().posInicial;
                tempBasura.transform.rotation = tempBasura.GetComponent<BasurasIdentificador>().rotacionInicial;
            }
        }
    }
}
