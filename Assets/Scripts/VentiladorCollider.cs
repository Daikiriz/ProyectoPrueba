using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VentiladorCollider : MonoBehaviour
{
    [Range(0.0f, 100)] public float alcanceViento;
    CapsuleCollider colliderVient;

    private void Update()
    {
        colliderVient = GetComponent<CapsuleCollider>();
        colliderVient.height = alcanceViento;
        colliderVient.center = new Vector3((alcanceViento / 2), 0, 0);
    }
}
