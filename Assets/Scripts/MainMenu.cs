using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject panelVerificar;
    public Button botonContinuar;
    public TextMeshProUGUI textoContinuar;
    Color colorD = new Color(0.35f, 0.35f, 0.35f, 1);
    public Image panelFade;
    int escenaTemp;
    float numeroFade;
    bool isFading;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("nivelActual"))
        {
            textoContinuar.color = colorD;
            botonContinuar.interactable = false;            
        }
    }

    private void Update()
    {
        if (isFading)
        {
            numeroFade += 5 * Time.deltaTime;
        }
        panelFade.color = new Color(0, 0, 0, numeroFade);        
    }

    public void CargarEscena()
    {
        escenaTemp = PlayerPrefs.GetInt("nivelActual");
        SiguienteSceneFade2();
    }

    public void SiguienteSceneV()
    {
        if (PlayerPrefs.HasKey("nivelActual"))
        {
            panelVerificar.SetActive(true);
        }
        else
        {
            SiguienteSceneFade();
        }        
    }

    public void SiguienteSceneFade()
    {
        panelFade.gameObject.SetActive(true);
        isFading = true;
        Invoke("SiguienteEscena", 2);
    }

    void SiguienteEscena()
    {
        SceneManager.LoadScene(1);
    }

    public void SiguienteSceneFade2()
    {
        panelFade.gameObject.SetActive(true);
        isFading = true;
        Invoke("SiguienteEscena2", 2);
    }

    void SiguienteEscena2()
    {
        SceneManager.LoadScene(escenaTemp);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
