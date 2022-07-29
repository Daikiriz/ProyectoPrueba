using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioRapido2 : MonoBehaviour
{
    int nivelActualSense;
    public GameObject spawnNivel2, nivel1, nivel2, jugador;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            nivelActualSense = PlayerPrefs.GetInt("nivelActual");

            if (nivelActualSense == 2 && spawnNivel2 != null)
            {
                SceneManager.LoadScene(2);
                nivel1.SetActive(false);
                nivel2.SetActive(true);
                jugador.transform.position = spawnNivel2.transform.position;
            }
        }
    }

}
