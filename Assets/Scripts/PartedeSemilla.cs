using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PartedeSemilla : MonoBehaviour
{
    bool isOnZoneS, isFading;
    public GameObject jugador, semilla, panelInteraccion;
    public Image botonE;

    private void Awake()
    {
        semilla.transform.DORotate(semilla.transform.rotation.eulerAngles + new Vector3(0, 180, 0), 2f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void Start()
    {
        if (botonE != null)
        {
            PrimeraSecuencia();
        }
    }

    void Update()
    {
        if (isOnZoneS)
        {
            panelInteraccion.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
                panelInteraccion.SetActive(false);
            }
        }
        else
        {
            panelInteraccion.SetActive(false);
        }   
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOnZoneS = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOnZoneS = false;
        }
    }

    void PrimeraSecuencia()
    {
        botonE.rectTransform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.5f).OnComplete(SegundaSecuencia);
    }
    void SegundaSecuencia()
    {
        botonE.rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(PrimeraSecuencia);
    }
}
