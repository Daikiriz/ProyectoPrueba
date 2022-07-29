using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspiradoraControlador : MonoBehaviour
{
    public GameObject aspiradora, camara;
    Vector3 aspiradoraDireccion, aspiradoraPos, offsetPos = new Vector3(0.5f, 0, 0);
    GameObject objetoAsp;
    public bool isSucking, isAspFull;
    [Range(0.0f, 100)] public float magnitudFuerza;
    public AudioSource audioAspiradora, audioPersonaje, audioAspiradora2, audioPuertas;
    public AudioClip sonidoAbsorber, sonidoViento;
    public AudioClip[] sonidosPasos = new AudioClip[6];
    public Image reticula, basuraActualImg;
    public Sprite[] basurasImagenes = new Sprite[6];
    public RectTransform reticulaRect;
    public Material indicadorV, indicadorR;
    Rigidbody rg;
    float timerSonidoPasos;
    Camera camarita;
    Ray rayc;
    RaycastHit hit, hitc;
    string tagObjeto;

    private void Awake()
    {
        camarita = camara.GetComponent<Camera>();
        rg = GetComponent<Rigidbody>();
        audioPersonaje = GetComponent<AudioSource>();
        basuraActualImg.sprite = basurasImagenes[5];
    }

    void Update()
    {
        if (isAspFull)
        {
            
            indicadorR.color = Color.gray;
            indicadorV.color = Color.green;
        }
        else
        {
            indicadorR.color = Color.red;
            indicadorV.color = Color.gray;
        }

        if (Physics.Raycast(camarita.transform.position, camarita.transform.forward, out hitc))
        {
            tagObjeto = hitc.transform.tag;

            if (tagObjeto == "Succionable")
            {
                Debug.Log("Mirando Basura");
                reticula.color = Color.green;
                reticulaRect.localScale = new Vector3(0.18f, 0.18f, 0.18f);
            }
            else
            {
                reticula.color = Color.white;
                reticulaRect.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }

        timerSonidoPasos += Time.deltaTime;
        if (timerSonidoPasos >= 0.62f)
        {
            timerSonidoPasos = 0;
        }

        if (rg.velocity != Vector3.zero)
        {
            if (!audioPersonaje.isPlaying && timerSonidoPasos == 0)
            {
                audioPersonaje.clip = sonidosPasos[Random.Range(0, 6)];
                audioPersonaje.Play();
            }
        }

        aspiradoraPos = aspiradora.transform.position - offsetPos;
        
        if (Input.GetMouseButton(0))
        {
            aspiradora.SetActive(true);
            isSucking = true;
        }
        else if(Input.GetMouseButton(1))
        {
            if (isAspFull)
            {
                objetoAsp.transform.SetParent(null);
                objetoAsp.transform.position = aspiradora.transform.position;
                objetoAsp.SetActive(true);
                Rigidbody rgt = objetoAsp.GetComponent<Rigidbody>();
                rgt.AddForce(aspiradora.transform.right * -magnitudFuerza * Time.deltaTime * 30, ForceMode.Impulse);
                objetoAsp = null;
                isAspFull = false;
                basuraActualImg.sprite = basurasImagenes[5];
            }
            else
            {
                aspiradora.SetActive(true);
                isSucking = false;
            }
        }
        else
        {
            aspiradora.SetActive(false);
        }        

        //if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit))
        //{
        //    Debug.DrawRay(camara.transform.position, camara.transform.forward, Color.black);
        //    Debug.Log("Did Hit In: ");
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 13 && isSucking)
        {
            AtraerObjeto(other.gameObject);
        }
        else if (other.gameObject.layer == 13 && !isSucking)
        {
            ExpulsarObjeto(other.gameObject);
        }

        if (other.gameObject.layer == 6 && isSucking)
        {
            AtraerObjeto(other.gameObject);
            //Debug.Log("Detectada Colision");
        }
        else if(other.gameObject.layer == 6 && !isSucking)
        {
            ExpulsarObjeto(other.gameObject);
            //Debug.Log("Expulse");
        }
    }

    public void AtraerObjeto(GameObject objeto)
    {
        GameObject temp = objeto;
        Rigidbody rg = temp.GetComponent<Rigidbody>();
        if (temp.tag == "Rocas")
        {
            rg.AddForce((aspiradora.transform.position - temp.transform.position) * magnitudFuerza * 5 * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            rg.AddForce((aspiradora.transform.position - temp.transform.position) * magnitudFuerza * Time.deltaTime, ForceMode.Impulse);
        }
        if (Vector3.Distance(aspiradora.transform.position, temp.transform.position) <= 1)
        {
            if (temp.tag == "Eliminable")
            {
                Destroy(temp);
                audioAspiradora.clip = sonidoAbsorber;
                audioAspiradora.Play();
            }
            else if (temp.tag == "Succionable" && !isAspFull)
            {
                audioAspiradora.clip = sonidoAbsorber;
                audioAspiradora.Play();
                temp.SetActive(false);
                objetoAsp = temp;
                objetoAsp.transform.SetParent(aspiradora.transform);
                isAspFull = true;
                for (int i = 0; i < basurasImagenes.Length-1; i++)
                {
                    if (objetoAsp.GetComponent<BasurasIdentificador>().idBasuraIndividual == i)
                    {
                        basuraActualImg.sprite = basurasImagenes[i];
                    }
                }
            }
            rg.velocity = Vector3.zero;
        }               
    }

    public void ExpulsarObjeto(GameObject objeto)
    {
        GameObject temp = objeto;
        Rigidbody rg = temp.GetComponent<Rigidbody>();
        if (temp.tag == "Rocas")
        {
            rg.AddForce(aspiradora.transform.right * -magnitudFuerza * 40 * Time.deltaTime, ForceMode.Impulse);
        }
        else
        {
            rg.AddForce(aspiradora.transform.right * -magnitudFuerza * 2 * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
