using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocidade = 5f;
    GameObject faseEventos;
    bool visivel = false;
    public ParticleSystem impacto;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * velocidade;
        faseEventos = GameObject.Find("FaseEventos");
    }

    private void Awake()
    {
        StartCoroutine(Sumir(25f / velocidade));
    }

    void OnBecameInvisible()
    {
        if (tag == "Tiro")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Tiro colidiu trigger");
        if(collision.gameObject.tag == "Canvas")
        {
            visivel = true;
        }

        if(collision.gameObject.tag == "Solido")
        {
            if(visivel == true)
            {
                if(MenuStaticClass.menuConfigParticulas == true)
                {
                    var impactoP = Instantiate(impacto, transform.position, Quaternion.Euler(transform.rotation.x * -1, 90, -90));
                    Destroy(impactoP, 2f);
                }
                faseEventos.GetComponent<FaseEventosScript>().TocarSom(Random.Range(6, 10));
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Canvas")
        {
            visivel = false;
        }
    }

    IEnumerator Sumir(float t)
    {
        yield return new WaitForSeconds(t);
        for (int i = 0; i < 15; i++)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled; // "!" = Inverter
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
