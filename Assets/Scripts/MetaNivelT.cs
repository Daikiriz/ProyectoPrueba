using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MetaNivelT : MonoBehaviour
{
    public GameObject puerta, panelInteraccion;
    public Image panelFade, botonE;
    float numeroFade;
    public int escenaIndex;
    Vector3 posIni;
    public bool onZone, isFading;

    private void Awake()
    {
        PlayerPrefs.SetInt("nivelActual", 0);
        posIni = puerta.transform.position;
    }

    private void Start()
    {
        PrimeraSecuencia();
    }

    void Update()
    {
        if (isFading)
        {
            numeroFade += 5 * Time.deltaTime;
        }
        panelFade.color = new Color(0, 0, 0, numeroFade);
        if (puerta.transform.position != posIni && onZone)
        {
            panelFade.gameObject.SetActive(true);
            panelInteraccion.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isFading = true;
                Invoke(nameof(FadeEscena), 2);
            }
        }
        if (!onZone)
        {
            panelFade.gameObject.SetActive(false);
            panelInteraccion.SetActive(false);
        }
    }

    void FadeEscena()
    {
        int semillasTemp = PlayerPrefs.GetInt("Semillas");
        PlayerPrefs.SetInt("semillas", semillasTemp + 1);
        SceneManager.LoadScene(escenaIndex);
        DOTween.KillAll();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            onZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onZone = true;
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
