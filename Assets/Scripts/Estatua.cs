using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Estatua : MonoBehaviour
{
    public int idEstatuas;
    public AudioClip[] sonidosEstatua = new AudioClip[3];
    Rigidbody rg;
    AudioSource audios;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rg.velocity != Vector3.zero)
        {
            if (!audios.isPlaying)
            {
                audios.clip = sonidosEstatua[Random.Range(0, 3)];
                audios.Play();
            }
        }
        else
        {
            if (audios.isPlaying)
            {
                audios.Stop();
            }
        }    
    }
}
