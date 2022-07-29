using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasurasIdentificador : MonoBehaviour
{
    public int idBasura, idBasuraIndividual;
    public Vector3 posInicial;
    public Quaternion rotacionInicial;
    Rigidbody rg;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
        rotacionInicial = transform.rotation;
        posInicial = transform.position;        
    }

    private void OnEnable()
    {
        rg.velocity = Vector3.zero;
    }
}
