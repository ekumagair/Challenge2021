using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
    public GameObject tiro; // Tiro do jogador
    public LayerMask floor_layer_mask;
    public LayerMask ui_layer_mask;
    public LayerMask uibtn_layer_mask;
    Vector3 posicao_input; // Posição do toque ou do mouse.
    public float velocidade = 5; // Velocidade em que o jogador anda. Pode ser modificado.
    public float velocidadeMult = 1;
    bool seMovendo = false;

    public bool multiplosTiros = false;
    public bool invulneravel = false;

    GameObject faseEventos;
    public GameObject tintaParticle;
    public Transform jTransform;
    public GameObject centroMovimento;
    public GameObject rastro;

    public Sprite[] spriteList;
    SpriteRenderer _sr;
    Collider2D _collider;
    Rigidbody2D _rb;

    public float camOffsetY = 0;
    public float anguloMult = 1;

    public int tempoSemDestruir = 0;

    public int vida = 2; // Quantos ataques o jogador sobrevive.

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        faseEventos = GameObject.Find("FaseEventos");
        _sr.enabled = true;
        _collider.enabled = true;
        StartCoroutine(Atirar());
        StartCoroutine(Piscar(2));

        if(MenuStaticClass.menuConfigParticulas == true)
        {
            StartCoroutine(Rastro());
        }

        if(MenuStaticClass.comprouRes == true)
        {
            ColetarPoderRes(35f);
            MenuStaticClass.comprouRes = false;
            PlayerPrefs.SetInt("comprouRes", 0);
        }
        if (MenuStaticClass.comprouTiros == true)
        {
            ColetarPoderTiros(35f);
            MenuStaticClass.comprouTiros = false;
            PlayerPrefs.SetInt("comprouTiros", 0);
        }
        if (MenuStaticClass.comprouVel == true)
        {
            ColetarPoderVelocidade(35f);
            MenuStaticClass.comprouVel = false;
            PlayerPrefs.SetInt("comprouVel", 0);
        }

        if(MenuStaticClass.menuContinuou == true)
        {
            transform.position = MenuStaticClass.perdeuPos;
            transform.rotation = MenuStaticClass.perdeuRot;

            if (MenuStaticClass.usandoRes == true)
            {
                ColetarPoderRes(10f);
            }
            if (MenuStaticClass.usandoTiros == true)
            {
                ColetarPoderTiros(15f);
            }
            if (MenuStaticClass.usandoVel == true)
            {
                ColetarPoderVelocidade(15f);
            }
        }

        if(MenuStaticClass.menuConfigInverter == true)
        {
            anguloMult = -1;
        }
        else
        {
            anguloMult = 1;
        }
        Debug.Log(MenuStaticClass.menuConfigInverter);

        if (MenuStaticClass.menuConfigCentralizar == false)
        {
            camOffsetY = 2.65f;
        }
        else
        {
            camOffsetY = 0;
        }
        Debug.Log(MenuStaticClass.menuConfigCentralizar);

        if(MenuStaticClass.menuConfigAfastarCam == true)
        {
            StartCoroutine(TempoSemDestruir());
        }

        if (MenuStaticClass.menuConfigJoyStick == false)
        {
            centroMovimento = gameObject;
        }
        else
        {
            centroMovimento = GameObject.FindGameObjectWithTag("JoyStickCentro");
        }

    }

    void Update()
    {/*
        if (faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false && Time.timeScale > 0)
        {
            // A Unity considera o Toque e o Mouse como duas coisas diferentes. Para podermos testar no computador, temos que criar duas condições para cada ação de movimento: Uma para o mouse e outra para o toque.
            if (Input.touchCount > 0) // Detectar toque na tela
            {
                seMovendo = true;

                Touch touch = Input.GetTouch(0); // Se houver vários toques, o jogo só leva em conta o primeiro toque.

                Movimento(touch.position); // Função principal de movimento
            }
            else if (Input.GetButton("Fire1")) // Detectar clique do mouse
            {
                seMovendo = true;

                Movimento(Input.mousePosition); // Função principal de movimento
            }
            else
            {
                seMovendo = false;
            }


            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + camOffsetY, Camera.main.transform.position.z); // Faz a câmera seguir o jogador (Posições X e Y). Ela mantém a própria posição Z.
        }
        else
        {
            seMovendo = false;
        }
        */
        // Morrer
        if (vida <= 0 && FaseEventosScript.perdeuFaseExecutou == false && faseEventos.GetComponent<FaseEventosScript>().completouFase == false && Time.timeScale > 0)
        {
            Debug.Log("Perdeu");
            faseEventos.GetComponent<FaseEventosScript>().TocarSom(2);
            FaseEventosScript.perdeuFaseExecutou = true;
            faseEventos.GetComponent<FaseEventosScript>().perdeuFase = true;

            if (MenuStaticClass.menuConfigParticulas == true)
            {
                var tinta_p = Instantiate(tintaParticle, transform.position, transform.rotation);
                var main = tinta_p.GetComponent<ParticleSystem>().main;
                main.startColor = new Color(255, 0, 0);
                tinta_p.GetComponent<ParticleSystem>().Play();
            }
            _sr.enabled = false;
            _collider.enabled = false;
            Hud.tocou = true;

            if (MenuStaticClass.menuFaseInfinita == false) // Se NÃO está na fase infinita.
            {
                StartCoroutine(PerdeuFase(3f));
            }
            else // Se ESTÁ na fase infinita.
            {
                faseEventos.GetComponent<FaseEventosScript>().completouFase = true;
            }
        }

        // Mudar sprite se o jogador tem o poder de resistência.
        if(vida > 2)
        {
            _sr.sprite = spriteList[1];
        }
        else
        {
            _sr.sprite = spriteList[0];
        }
    }

    private void FixedUpdate() // O FixedUpdate é usado para a velocidade do jogador ser consistente no celular e no pc.
    {
        if (faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false && Time.timeScale > 0)
        {
            // A Unity considera o Toque e o Mouse como duas coisas diferentes. Para podermos testar no computador, temos que criar duas condições para cada ação de movimento: Uma para o mouse e outra para o toque.
            if (Input.touchCount > 0) // Detectar toque na tela
            {
                seMovendo = true;

                Touch touch = Input.GetTouch(0); // Se houver vários toques, o jogo só leva em conta o primeiro toque.

                Movimento(touch.position); // Função principal de movimento
            }
            else if (Input.GetButton("Fire1")) // Detectar clique do mouse
            {
                seMovendo = true;

                Movimento(Input.mousePosition); // Função principal de movimento
            }
            else
            {
                seMovendo = false;
            }


            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + camOffsetY, Camera.main.transform.position.z); // Faz a câmera seguir o jogador (Posições X e Y). Ela mantém a própria posição Z.
        }
        else
        {
            seMovendo = false;
        }
    }

    IEnumerator Atirar()
    {
        while(faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false)
        {
            yield return new WaitForSeconds(0.5f / velocidadeMult);

            if (Time.timeScale > 0)
            {
                faseEventos.GetComponent<FaseEventosScript>().TocarSom(Random.Range(3, 6));
                CriarTiroDoJogador(0f);

                // Poder de tiros extras
                if (multiplosTiros == true)
                {
                    CriarTiroDoJogador(-90f);
                    CriarTiroDoJogador(90f);
                }

                print("Atirou");
            }
        }
    }

    IEnumerator TempoSemDestruir()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (MenuStaticClass.inimigosDestruidos > 0)
            {
                tempoSemDestruir++;
                Debug.Log("+1s");

                if (faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false)
                {
                    if (tempoSemDestruir > 15)
                    {
                        faseEventos.GetComponent<FaseEventosScript>().ajudaZoomOutVoltar = false;
                        faseEventos.GetComponent<FaseEventosScript>().ajudaZoomOutIr = true;
                    }
                    else
                    {
                        faseEventos.GetComponent<FaseEventosScript>().ajudaZoomOutIr = false;
                        faseEventos.GetComponent<FaseEventosScript>().ajudaZoomOutVoltar = true;
                    }
                }
                else
                {
                    StopCoroutine(TempoSemDestruir());
                    break;
                }
            }
        }
    }

    void CriarTiroDoJogador(float rot)
    {
        Debug.Log(gameObject.transform.rotation.z);
        var tiroJogador = Instantiate(tiro, transform.position, gameObject.transform.rotation);
        tiroJogador.transform.Rotate(0, 0, rot);
        tiroJogador.GetComponent<Tiro>().velocidade = 10.0f * velocidadeMult;
    }

    void Movimento(Vector3 posInput) // Função principal de movimento
    {
        Hud.tocou = true;

        if (faseEventos.GetComponent<FaseEventosScript>().completouFase == false && faseEventos.GetComponent<FaseEventosScript>().perdeuFase == false)
        {
            posicao_input = posInput; // Posição do input (toque/mouse)
            OlharParaInput(centroMovimento); // Mudar o ângulo do jogador

            LayerMask solidoMask = LayerMask.GetMask("SolidoLayer");
            RaycastHit2D solidoHit = Physics2D.Raycast(jTransform.position, transform.up, 0.6f, solidoMask);
            //Debug.Log("HIT: " + solidoHit);

            if (solidoHit.collider == null)
            {
                transform.position += transform.up * (velocidade * velocidadeMult * Time.deltaTime); // Faz o jogador andar na direção do ângulo.
            }
        }
    }

    void OlharParaInput(GameObject go) // Mudar o ângulo do jogador
    {
        Vector3 mousePosition = posicao_input;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - go.transform.position.x,
            mousePosition.y - go.transform.position.y
        ) ;

        go.transform.up = direction * anguloMult;

        if(go != gameObject)
        {
            gameObject.transform.rotation = go.transform.rotation;
        }
    }

    IEnumerator Rastro()
    {
        while(true)
        {
            while(seMovendo && MenuStaticClass.usandoVel == true)
            {
                 var rInstance = Instantiate(rastro, transform.position, transform.rotation);
                 rInstance.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                 yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TiroInimigo") // Se colidiu com tiro do inimigo.
        {
            if(!invulneravel)
            {
                vida--;
                faseEventos.GetComponent<FaseEventosScript>().TocarSom(11);

                if (vida > 0)
                {
                    StartCoroutine(Piscar(3));
                }
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Item") // Se colidiu com power-up.
        {
            if(collision.gameObject.GetComponent<ItemPoder>().poder == 0)
            {
                ColetarPoderVelocidade(10f);
            }
            else if (collision.gameObject.GetComponent<ItemPoder>().poder == 1)
            {
                ColetarPoderTiros(15f);
            }
            else if (collision.gameObject.GetComponent<ItemPoder>().poder == 2)
            {
                ColetarPoderRes(15f);
            }

            faseEventos.GetComponent<FaseEventosScript>().TocarSom(1);
            Destroy(collision.gameObject);

            if (invulneravel == false)
            {
                StartCoroutine(Piscar(4));
            }
        }
    }

    //DANO
    IEnumerator Piscar(int segundos)
    {
        Debug.Log("INÍCIO PISCAR");

        invulneravel = true;
        int segundos_reais = segundos * 10;
        for (int i = 0; i < segundos_reais; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled; // "!" = Inverter
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        invulneravel = false;

        Debug.Log("FIM PISCAR");
    }

    // Poder de VELOCIDADE
    void ColetarPoderVelocidade(float t) // t = tempo, em segundos
    {
        StopCoroutine(PoderVelocidade(35f));
        StopCoroutine(PoderVelocidade(t)); // 10s
        StartCoroutine(PoderVelocidade(t));
    }

    IEnumerator PoderVelocidade(float t)
    {
        Debug.Log("INÍCIO VELOCIDADE");
        MenuStaticClass.usandoVel = true;
        velocidadeMult = 1.5f;
        yield return new WaitForSeconds(t);
        velocidadeMult = 1.0f;
        MenuStaticClass.usandoVel = false;
        Debug.Log("FIM VELOCIDADE");
    }

    // Poder de MÚLTIPLOS TIROS
    void ColetarPoderTiros(float t)
    {
        StopCoroutine(PoderTiros(35f));
        StopCoroutine(PoderTiros(t)); // 15s
        StartCoroutine(PoderTiros(t));
    }

    IEnumerator PoderTiros(float t)
    {
        Debug.Log("INÍCIO TIROS");
        MenuStaticClass.usandoTiros = true;
        multiplosTiros = true;
        yield return new WaitForSeconds(t);
        multiplosTiros = false;
        MenuStaticClass.usandoTiros = false;
        Debug.Log("FIM TIROS");
    }

    // Poder de RESISTÊNCIA
    void ColetarPoderRes(float t)
    {
        StopCoroutine(PoderRes(35f));
        StopCoroutine(PoderRes(t)); // 15s
        StartCoroutine(PoderRes(t));
    }

    IEnumerator PoderRes(float t)
    {
        Debug.Log("INÍCIO RES");
        MenuStaticClass.usandoRes = true;

        vida = 3;

        yield return new WaitForSeconds(t);

        if(vida > 2)
        {
            vida--;
            if (invulneravel == false)
            {
                StartCoroutine(Piscar(1));
            }
        }

        MenuStaticClass.usandoRes = false;
        Debug.Log("FIM RES");
    }

    // Perdeu
    IEnumerator PerdeuFase(float t)
    {
        Debug.Log("Perdeu - Corrotina - " + t);

        MenuStaticClass.posicoesInimigos = faseEventos.GetComponent<FaseEventosScript>().posicoesInimigosFase;
        MenuStaticClass.perdeuPos = transform.position;
        MenuStaticClass.perdeuRot = transform.rotation;
        MenuStaticClass.comprouRes = false;
        MenuStaticClass.comprouTiros = false;
        MenuStaticClass.comprouVel = false;

        yield return new WaitForSeconds(t);

        PlayerPrefs.Save();
        MenuStaticClass.menuIrParaFases = false;
        SceneManager.LoadScene("FimDeJogo");
    }
}
