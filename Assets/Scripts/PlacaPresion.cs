using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlacaPresion : MonoBehaviour
{
    public int idEstatuaRequerida;
    float posY, posYI;
    public bool elevarPilar, desactivarVent, requiereID, abrirCelda, isActivated;
    public ManagerCelda managerCelda;
    public GameObject[] ventiladores = new GameObject[5];
    public GameObject[] pilares = new GameObject[5];
    float[] pilaresPosY = new float[5];
    float[] pilaresPosYI = new float[5];

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            transform.DOMoveY(posY, 0.8f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (requiereID && other.tag == "Rocas")
        {
            if (!other.isTrigger && other.GetComponent<Estatua>().idEstatuas == idEstatuaRequerida)
            {
                transform.DOMoveY(posYI, 0.8f);

                if (abrirCelda && isActivated)
                {
                    isActivated = false;
                    managerCelda.placasActivadas--;
                }

                if (desactivarVent)
                {
                    for (int i = 0; i < ventiladores.Length; i++)
                    {
                        if (ventiladores[i] != null)
                        {
                            ventiladores[i].GetComponent<Ventilador>().isActive = true;
                        }
                    }
                }
                if (elevarPilar)
                {
                    for (int i = 0; i < pilares.Length; i++)
                    {
                        if (pilares[i] != null)
                        {
                            pilares[i].gameObject.transform.DOMoveY(pilaresPosYI[i], 1);
                        }
                    }
                }
            }
        }
        else
        {
            if (!other.isTrigger)
            {
                transform.DOMoveY(posYI, 0.8f);
                if (desactivarVent)
                {
                    for (int i = 0; i < ventiladores.Length; i++)
                    {
                        if (ventiladores[i] != null)
                        {
                            ventiladores[i].GetComponent<Ventilador>().isActive = true;
                        }
                    }
                }
                if (elevarPilar)
                {
                    for (int i = 0; i < pilares.Length; i++)
                    {
                        if (pilares[i] != null)
                        {
                            pilares[i].gameObject.transform.DOMoveY(pilaresPosYI[i], 1);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (requiereID && other.tag == "Rocas")
        {
            if (!other.isTrigger && other.GetComponent<Estatua>().idEstatuas == idEstatuaRequerida)
            {
                if (abrirCelda && !isActivated)
                {
                    isActivated = true;
                    managerCelda.placasActivadas++;
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
        else
        {
            if (!other.isTrigger)
            {
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
    }
}
