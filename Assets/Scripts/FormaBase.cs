using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormaBase : MonoBehaviour
{
    SpriteRenderer _sr;
    Collider2D _collider;
    public byte corR = 255;
    public byte corG = 255;
    public byte corB = 255;

    public int vida = 1;
    bool destruido = false;

    public GameObject alvo;
    Vector2 alvoAngulo;
    public float distanciaAlvo;

    public Vector2 posInicial;

    public GameObject tiro;

    GameObject faseEventos;

    public GameObject tinta;
    public GameObject tintaParticle;

    public float tintaMult = 1f;
    public int valor = 0;

    public bool seisTiros = false;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color32(corR, corG, corB, 255);
        _collider = GetComponent<Collider2D>();
        posInicial = transform.position;
        alvo = GameObject.Find("Jogador");
        faseEventos = GameObject.Find("FaseEventos");
        faseEventos.GetComponent<FaseEventosScript>().inimigosRestantes++;
        destruido = false;
        StartCoroutine(Atirar());
    }

    private void Update()
    {
        if (faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false && alvo != null)
        {
            // Mirar no jogador - INÍCIO

            Vector3 targetPosition = alvo.transform.position;
            targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);

            Vector2 direction = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);

            alvoAngulo = direction;

            //transform.up = alvoAngulo;

            // Mirar no jogador - FIM

            distanciaAlvo = Vector2.Distance(transform.position, alvo.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tiro" && vida > 0 && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false)
        {
            Debug.Log("Destruiu com tiro.");
            vida--;
            Destroy(collision.gameObject);

            ChecarMorte(false);
        }

        if (collision.gameObject.tag == "Destruir")
        {
            Debug.Log("Destruiu com trigger.");
            vida = -1;
            destruido = false;
            ChecarMorte(true);
        }
    }

    void ChecarMorte(bool silencio)
    {
        if (vida <= 0 && destruido == false)
        {
            destruido = true;
            faseEventos.GetComponent<FaseEventosScript>().inimigosRestantes--;

            if(MenuStaticClass.menuConfigSFX == true && silencio == false)
            {
                faseEventos.GetComponent<FaseEventosScript>().TocarSom(2);
            }

            var tinta_inst = Instantiate(tinta, transform.position, transform.rotation);
            tinta_inst.GetComponent<SpriteRenderer>().color = new Color32(corR, corG, corB, 128);
            tinta_inst.transform.eulerAngles = new Vector3(0, 0, Random.Range(0.0f, 360f));
            tinta_inst.transform.localScale = new Vector3(0.6f * tintaMult, 0.6f * tintaMult, 0.6f * tintaMult);

            if (MenuStaticClass.menuConfigParticulas == true && silencio == false)
            {
                var tinta_p = Instantiate(tintaParticle, transform.position, transform.rotation);
                tinta_p.GetComponent<ParticleSystem>().startColor = new Color32(corR, corG, corB, 255);
                tinta_p.GetComponent<ParticleSystem>().Play();
                Destroy(tinta_p, 5.0f);
            }

            // Faz o pentágono gerar quadrados.
            if(GetComponent<FormaPentagono>() != null)
            {
                GetComponent<FormaPentagono>().GerarInimigos();
            }

            // Se há poucos inimigos, mostrar setas que apontam para os inimigos restantes.
            if (faseEventos.GetComponent<FaseEventosScript>().inimigosRestantes <= faseEventos.GetComponent<FaseEventosScript>().inimigosRestantesSetas && FaseEventosScript.inimigosRestantesSetasCriou == false && MenuStaticClass.menuFaseInfinita == false)
            {
                FaseEventosScript.inimigosRestantesSetasCriou = true;
                faseEventos.GetComponent<FaseEventosScript>().GerarSetas();
            }

            // Ganhar dinheiro por destruir o inimigo.
            if (silencio == false)
            {
                MenuStaticClass.menu_dinheiro += valor;
                MenuStaticClass.inimigosDestruidosTotal++;

                if(alvo.GetComponent<Jogador>() != null)
                {
                    alvo.GetComponent<Jogador>().tempoSemDestruir = 0;
                }

                PlayerPrefs.SetInt("dinheiro", MenuStaticClass.menu_dinheiro);
            }

            // Adicionar posição inicial à lista de posições.
            if(MenuStaticClass.menuFaseInfinita == false)
            {
                MenuStaticClass.inimigosDestruidos++;
                faseEventos.GetComponent<FaseEventosScript>().posicoesInimigosFase[MenuStaticClass.inimigosDestruidos - 1] = posInicial;
            }

            Destroy(gameObject);
        }
        
        if(vida > 0)
        {
            faseEventos.GetComponent<FaseEventosScript>().TocarSom(12);
            StartCoroutine(Piscar());
        }
    }

    public IEnumerator Piscar()
    {
        if (_sr == null && vida > 0)
        {
            _sr = GetComponent<SpriteRenderer>();
        }

        for (int i = 0; i < 10; i++)
        {
            if (_sr != null)
            {
                _sr.enabled = !_sr.enabled;
            }
            yield return new WaitForSeconds(0.05f);
        }

        if (_sr != null)
        {
            _sr.enabled = true;
        }
    }

    IEnumerator Atirar()
    {
        yield return new WaitForSeconds(Random.Range(0f, 5f));
        while (destruido == false)
        {
            yield return new WaitForSeconds(3.5f);
            if (faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false && alvo != null && distanciaAlvo < 6f)
            {
                Debug.Log("Inimigo atirou");

                if (seisTiros == false)
                {
                    var meuTiro = Instantiate(tiro, transform.position, transform.rotation);
                    meuTiro.GetComponent<Tiro>().velocidade = -3.0f;
                    meuTiro.transform.rotation = RotateTowards(alvo.transform.position);
                }
                else // Hexágono
                {
                    CriarTiroDoInimigo(0f);
                    CriarTiroDoInimigo(45f);
                    CriarTiroDoInimigo(-45f);
                    CriarTiroDoInimigo(135f);
                    CriarTiroDoInimigo(-135f);
                    CriarTiroDoInimigo(180f);
                }
            }
        }
    }

    void CriarTiroDoInimigo(float rot)
    {
        var tiroIni = Instantiate(tiro, transform.position, gameObject.transform.rotation);
        tiroIni.transform.Rotate(0, 0, rot);
        tiroIni.GetComponent<Tiro>().velocidade = 3.0f;
    }

    private Quaternion RotateTowards(Vector2 target)
    {
        var offset = 90f;
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(Vector3.forward * (angle + offset));
    }
}
