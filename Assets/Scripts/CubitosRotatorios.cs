using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubitosRotatorios : MonoBehaviour
{
    public AspiradoraControlador aspiradora;
    public int indexador;
    bool isDoneAnimation = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && isDoneAnimation)
        {
            if (aspiradora.isSucking)
            {
                if (indexador == 0)
                {
                    indexador = 3;
                }
                else
                {
                    indexador--;
                }

                isDoneAnimation = false;
                transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 90), 1).OnComplete(ResetAnim);
            }
            if (!aspiradora.isSucking)
            {
                if (indexador == 3)
                {
                    indexador = 0;
                }
                else
                {
                    indexador++;
                }

                isDoneAnimation = false;
                transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, -90), 1).OnComplete(ResetAnim);
            }
            
        }
    }

    public void ResetAnim()
    {
        isDoneAnimation = true;
    }

    public void RotarPos()
    {

    }
}
