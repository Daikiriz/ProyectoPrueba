using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MetaNivel : MonoBehaviour
{
    public GameObject parteSemilla, puertaFinal, panelInteraccion, spawnLevelSiguiente, player, nivel1, nivel2;
    public Image panelFade, botonE;
    float numeroFade;
    bool onZone, isFading;


    //private void OnEnable()
   // {
   //     PlayerPrefs.SetInt("nivelActual", 1);
    //}

    private void Start()
    {
        if (botonE != null)
        {
            PrimeraSecuencia();
        }
    }

    void Update()
    {
        if (isFading)
        {
            numeroFade += 5 * Time.deltaTime;
        }

        panelFade.color = new Color(0, 0, 0, numeroFade);

        if (!parteSemilla.activeInHierarchy && onZone)
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
        nivel2.SetActive(true);
        player.transform.position = spawnLevelSiguiente.transform.position;
        nivel1.SetActive(false);
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
            onZone = false;
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
