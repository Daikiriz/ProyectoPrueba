using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuerzaContenedores : MonoBehaviour
{
    Rigidbody rgT;
    Vector3 fuerzaDir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Succionable")
        {
            rgT = other.gameObject.GetComponent<Rigidbody>();
            fuerzaDir = transform.position - other.gameObject.transform.position;
            rgT.AddForce(fuerzaDir * 5, ForceMode.Impulse);
        }        
    }
}
