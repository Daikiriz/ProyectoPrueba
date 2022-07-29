using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorSalto : MonoBehaviour
{
    public GameObject panelInfo;
    bool activasiom;

    private void Update()
    {
        if (panelInfo.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            panelInfo.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activasiom)
        {
            panelInfo.SetActive(true);
            activasiom = true;
        }
        
    }
}
