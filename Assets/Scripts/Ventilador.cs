using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ventilador : MonoBehaviour
{
    [Range(0.0f, 1000)] public float magnitudFuerza;
    public GameObject aspas, efectoVent;
    public AudioSource audioVent;
    public bool isActive = true;
    Tween aspasGirarTweener;

    private void Awake()
    {
        aspasGirarTweener = aspas.transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 90, 180), 0.2f).SetLoops(-1).SetEase(Ease.Linear);

        audioVent.time = Random.value * audioVent.clip.length;
        audioVent.Play();
    }

    private void Update()
    {
        if (isActive)
        {
            aspasGirarTweener.Play();
        }
        else if (!isActive)
        {
            aspasGirarTweener.Pause();
            audioVent.Stop();
            efectoVent.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive && !other.isTrigger)
        {
            GameObject temp = other.gameObject;
            Rigidbody rg = temp.GetComponent<Rigidbody>();
            if (temp.tag == "Player")
            {
                Vector3 direccionFuerza = transform.position - new Vector3(temp.transform.position.x, 0, temp.transform.position.z); ;
                rg.AddForce(direccionFuerza * -magnitudFuerza * 3 * Time.deltaTime, ForceMode.Impulse);
            }
            else
            {
                Vector3 direccionFuerza = transform.position - temp.transform.position;
                rg.AddForce(direccionFuerza * -magnitudFuerza * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
}
