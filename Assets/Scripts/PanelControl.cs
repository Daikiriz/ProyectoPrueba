using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelControl : MonoBehaviour
{
    public GameObject panelGlobal;
    public GameObject[] panelesTexto = new GameObject[2];
    public int indexador, tamanioMax;
    public Image botonE;

    private void Start()
    {
        tamanioMax = panelesTexto.Length;
        PrimeraSecuencia();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && indexador < tamanioMax-1 && panelesTexto[indexador].gameObject.activeInHierarchy)
        {
            panelesTexto[indexador].gameObject.SetActive(false);
            indexador++;
            panelesTexto[indexador].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && panelesTexto[indexador].gameObject.activeInHierarchy && indexador >= tamanioMax-1)
        {
            panelGlobal.SetActive(false);
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
