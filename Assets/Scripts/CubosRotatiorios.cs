using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubosRotatiorios : MonoBehaviour
{
    public bool generarEstatua, encenderVentilador, abrirPuerta;
    public GameObject[] ventiladores = new GameObject[5];
    public GameObject estatuaAMover, puerta;    
    public CubitosRotatorios cubitosRot1, cubitosRot2, cubitosRot3;
    public int indexClave1, indexClave2, indexClave3;
    public float estOffsetGen, puertaOffset = 6;
    Vector3 estatuaPos, posicionFinalPuerta;
    bool isDoneEstatua;

    private void Awake()
    {
        if (estatuaAMover != null)
        {
            estatuaPos = estatuaAMover.transform.position;
        }        

        if (puerta != null)
        {
            posicionFinalPuerta = puerta.transform.position + new Vector3(0, puertaOffset, 0);
        }
    }

    void Start()
    {
           
    }
    
    void Update()
    {
        if (cubitosRot1.indexador == indexClave1 && cubitosRot2.indexador == indexClave2 && cubitosRot3.indexador == indexClave3)
        {
            if (generarEstatua && !isDoneEstatua)
            {
                isDoneEstatua = true;
                estatuaAMover.SetActive(true);
                estatuaAMover.transform.DOMoveY(estatuaPos.y + estOffsetGen, 1);
            }

            if (encenderVentilador && ventiladores[0] != null)
            {
                for (int i = 0; i < ventiladores.Length; i++)
                {
                    ventiladores[i].GetComponent<Ventilador>().isActive = true;
                }
            }

            if (!encenderVentilador && ventiladores[0] != null)
            {
                for (int i = 0; i < ventiladores.Length; i++)
                {
                    ventiladores[i].GetComponent<Ventilador>().isActive = false;
                }
            }

            if (abrirPuerta && puerta != null)
            {
                puerta.transform.DOMove(posicionFinalPuerta, 4);
            }
        }
    }
}
