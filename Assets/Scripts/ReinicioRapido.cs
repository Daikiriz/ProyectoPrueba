using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioRapido : MonoBehaviour
{
    public int nivelActualSense;
    public GameObject spawnNivel1, spawnNivel2, nivel1, nivel2, jugador, reinicioText;

    private void Awake()
    {
        nivelActualSense = PlayerPrefs.GetInt("nivelActual");

        if (nivelActualSense == 1)
        {
            nivel1.SetActive(true);
            nivel2.SetActive(false);
            jugador.transform.position = spawnNivel1.transform.position;
        }
        else if (nivelActualSense == 2)
        {
            nivel1.SetActive(false);
            nivel2.SetActive(true);
            jugador.transform.position = spawnNivel2.transform.position;
        }
    }

    private void Start()
    {
        if (reinicioText != null) 
        {
            Invoke(nameof(ReseReinicioText), 30);
        }
        
    }

    void ReseReinicioText()
    {
        reinicioText.SetActive(false);
    }

    void Update()
    {
        Debug.Log("Guardado Actual: " + PlayerPrefs.GetInt("nivelActual"));

        if (Input.GetKeyDown(KeyCode.R))
        {
            nivelActualSense = PlayerPrefs.GetInt("nivelActual");

            if (nivelActualSense == 0)
            {
                SceneManager.LoadScene(1);
            }

            if (nivelActualSense >= 1)
            {
                SceneManager.LoadScene(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("nivelActual", 1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt("nivelActual", 2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("nivelActual", 1);
        }        
    }
}
