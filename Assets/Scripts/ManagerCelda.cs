using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ManagerCelda : MonoBehaviour
{
    public int placasActivadas;
    float posYBarrotes, offsetBarrotes = 5;
    bool isDoneAnim;
    public GameObject[] barrotes = new GameObject[16];

    private void Awake()
    {
        posYBarrotes = barrotes[0].transform.position.y;
    }

    void Update()
    {
        if (placasActivadas >= 4 && !isDoneAnim)
        {
            for (int i = 0; i < barrotes.Length; i++)
            {
                barrotes[i].transform.DOMoveY(posYBarrotes - offsetBarrotes, 4);
                if (i >= 3)
                {
                    isDoneAnim = true;
                }
            }
        }
    }
}
