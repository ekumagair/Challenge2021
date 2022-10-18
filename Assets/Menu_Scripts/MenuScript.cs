using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Camera menuCamera;
    public Image fadeImage;
    bool isFading = false;

    public GameObject tinta_go;
    public GameObject tinta_particle;

    public GameObject[] botoes;
    public GameObject[] botoes_text;

    public Text dinheiro_text;

    public static bool salvar = false;

    AudioSource audioSrc;
    GameObject musica;

    private void Start()
    {
        salvar = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        ApagarTinta();
        IrPara(0, 0, false);
        audioSrc = GetComponent<AudioSource>();
        musica = GameObject.FindGameObjectWithTag("Musica");
        botoes = GameObject.FindGameObjectsWithTag("Botão");
        botoes_text = GameObject.FindGameObjectsWithTag("BotãoText");
        MenuStaticClass.menuFaseInfinita = false;
        MenuStaticClass.menuContinuou = false;
        MenuStaticClass.usandoVel = false;
        MenuStaticClass.usandoTiros = false;
        MenuStaticClass.usandoRes = false;
        MenuStaticClass.inimigosDestruidos = 0;


        if (salvar == false && PlayerPrefs.HasKey("dinheiro")) // Carregar
        {
            MenuStaticClass.menu_dinheiro = PlayerPrefs.GetInt("dinheiro");
            MenuStaticClass.menuFaseDesbloqueada = PlayerPrefs.GetInt("faseDesbloqueada");

            MenuStaticClass.comprouVel = IntParaBool(PlayerPrefs.GetInt("comprouVel"));
            MenuStaticClass.comprouTiros = IntParaBool(PlayerPrefs.GetInt("comprouTiros"));
            MenuStaticClass.comprouRes = IntParaBool(PlayerPrefs.GetInt("comprouRes"));
            MenuStaticClass.jogandoPelaPrimeiraVez = IntParaBool(PlayerPrefs.GetInt("jogandoPelaPrimeiraVez"));
        }

        if (PlayerPrefs.HasKey("config0")) // Carregar configurações
        {
            MenuStaticClass.menuConfigSFX = IntParaBool(PlayerPrefs.GetInt("config0"));
            MenuStaticClass.menuConfigParticulas = IntParaBool(PlayerPrefs.GetInt("config1"));
            MenuStaticClass.menuConfigInverter = IntParaBool(PlayerPrefs.GetInt("config2"));
            MenuStaticClass.menuConfigCentralizar = IntParaBool(PlayerPrefs.GetInt("config3"));
            //MenuStaticClass.menuConfigPosVidas = IntParaBool(PlayerPrefs.GetInt("config4"));
            MenuStaticClass.menuConfigAfastarCam = IntParaBool(PlayerPrefs.GetInt("config5"));
            MenuStaticClass.menuConfigPosSairEsq = IntParaBool(PlayerPrefs.GetInt("config6"));
            MenuStaticClass.menuConfigPosVidas2 = IntParaBool(PlayerPrefs.GetInt("config7"));
            MenuStaticClass.menuConfigJoyStick = IntParaBool(PlayerPrefs.GetInt("config8"));
            MenuStaticClass.menuConfigMusica = IntParaBool(PlayerPrefs.GetInt("config9"));
        }

        PlayerPrefs.Save();

        if (MenuStaticClass.menuIrParaFases == true)
        {
            MenuStaticClass.menuIrParaFases = false;
            IrPara(10, 0, false);
        }

        if (MenuStaticClass.menuFaseDesbloqueada > 17)
        {
            MenuStaticClass.menuFaseDesbloqueada = 17;
        }

        Debug.Log("Jogando pela primeira vez: " + MenuStaticClass.jogandoPelaPrimeiraVez);
    }

    public static bool IntParaBool(int i)
    {
        if(i == 0)
        {
            Debug.Log("returned false");
            return false;
        }
        else
        {
            Debug.Log("returned true");
            return true;
        }
    }

    public static int BoolParaInt(bool b)
    {
        if (b == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void Salvar() // Usado por botões.
    {
        PlayerPrefs.Save();
    }

    // Destruir todos os sprites de tinta do menu.
    void ApagarTinta()
    {
        foreach (GameObject tinta in GameObject.FindGameObjectsWithTag("Splat"))
        {
            Destroy(tinta);
        }
    }

    // Ir para seleção de fases.
    public void ClicouJogar()
    {
        Debug.Log("Clicou jogar");
        EsconderBotao("Jogar", "Jogar_Text");
        IrPara(10, 0, true);
        salvar = true;
    }

    public void ClicouConfig()
    {
        Debug.Log("Clicou config");
        EsconderBotao("Config", "Config_Text");
        IrPara(20, 0, true);
        salvar = true;
    }

    public void ClicouLoja()
    {
        Debug.Log("Clicou loja");
        EsconderBotao("IrParaLoja", "IrParaLoja_Text");
        IrPara(30, 0, true);
    }

    public void ClicouGaleria()
    {
        Debug.Log("Clicou galeria");
        EsconderBotao("IrParaGaleria", "IrParaGaleria_Text");
        IrPara(40, 0, true);
    }

    public void ClicouVoltar()
    {
        Debug.Log("Clicou voltar");
        EsconderBotao("Voltar", "Voltar_Text");
        IrPara(0, 0, true);
    }

    public void ClicouVoltarParaFases()
    {
        Debug.Log("Clicou voltar para fases");
        EsconderBotao("Voltar", "Voltar_Text");
        IrPara(10, 0, true);
    }

    // Usado na tela de fim de jogo.
    public void IrParaMenuPrincipal()
    {
        Debug.Log("Clicou ir para título");
        SceneManager.LoadScene("Menus");
    }

    // Usado na tela de fim de jogo.
    public void IrParaMenuPrincipalFases()
    {
        Debug.Log("Clicou ir para fases");
        PlayerPrefs.SetInt("dinheiro", MenuStaticClass.menu_dinheiro);
        PlayerPrefs.SetInt("faseDesbloqueada", MenuStaticClass.menuFaseDesbloqueada);
        MenuStaticClass.menuIrParaFases = true;
        SceneManager.LoadScene("Menus");
    }

    // Usado na tela de fim de jogo. (REINICIAR)
    public void ReiniciarFase()
    {
        Debug.Log("Clicou reiniciar");
        MenuStaticClass.menuContinuou = false;
        SceneManager.LoadScene("Fase" + MenuStaticClass.menuFaseSelecionada);
    }

    // Usado na tela de slides.
    public void IrParaFaseSelecionada()
    {
        Debug.Log("Clicou pular");
        SceneManager.LoadScene("Fase" + MenuStaticClass.menuFaseSelecionada);
    }

    // Usado na tela de fim de jogo. (CONTINUAR)
    public void FimDeJogoContinuar(int custo)
    {
        Debug.Log("Clicou continuar");
        MenuStaticClass.menuContinuou = true;
        MenuStaticClass.menu_dinheiro -= custo;
        SceneManager.LoadScene("Fase" + MenuStaticClass.menuFaseSelecionada);
    }

    public void SomBotao()
    {
        // Só toca o som do botão se os efeitos sonoros estiverem ativados nas configurações.
        if(MenuStaticClass.menuConfigSFX == true)
        {
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }

    private void Update()
    {
        if(dinheiro_text != null)
        {
            dinheiro_text.text = "Dinheiro: $ " + MenuStaticClass.menu_dinheiro.ToString();
        }

        if (MenuStaticClass.menuFaseDesbloqueada < 1)
        {
            MenuStaticClass.menuFaseDesbloqueada = 1;
        }
        if (MenuStaticClass.menuQuadroSelecionado < 1)
        {
            MenuStaticClass.menuQuadroSelecionado = 1;
        }

        musica.GetComponent<AudioSource>().enabled = MenuStaticClass.menuConfigMusica;

        //Debug.Log("Quadro atual: " + MenuStaticClass.menuQuadroSelecionado);
    }

    public void EsconderBotao(string botao, string botao_text)
    {
        // Se não estiver tocando animação de fade-in e fade-out.
        if (!isFading)
        {
            SomBotao();
            Debug.Log("Escondeu " + botao + " e " + botao_text);

            // Esconder botões
            foreach (GameObject bt in botoes)
            {
                if (bt.name == botao)
                {
                    bt.GetComponent<Image>().enabled = false;

                    Color tinta_color = Random.ColorHSV(0, 1, 1, 1, 0.75f, 1);

                    var tinta_inst = Instantiate(tinta_go, bt.transform.position, transform.rotation);
                    tinta_inst.GetComponent<SpriteRenderer>().color = tinta_color;
                    tinta_inst.transform.eulerAngles = new Vector3(0, 0, Random.Range(0.0f, 360f));

                    if (MenuStaticClass.menuConfigParticulas == true)
                    {
                        var tinta_p = Instantiate(tinta_particle, bt.transform.position, transform.rotation);
                        var main = tinta_p.GetComponent<ParticleSystem>().main;
                        main.startColor = tinta_color;
                        tinta_p.GetComponent<ParticleSystem>().Play();
                        Destroy(tinta_p, 5.0f);
                    }
                }
            }

            // Esconder textos dos botões
            foreach (GameObject bt_txt in botoes_text)
            {
                if (bt_txt.name == botao_text)
                {
                    bt_txt.GetComponent<Text>().enabled = false;
                }
            }
        }
        else
        {
            Debug.Log("Não escondeu");
        }
    }

    public void IrPara(int vx, int vy, bool fade)
    {
        // fade == false --> ir para tela instantaneamente.
        // fade == true --> ir para tela depois de animação de fade-in e fade-out.

        if(!isFading)
        {
            if (fade)
            {
                StartCoroutine(FadeImage(vx, vy, false));
            }
            else
            {
                menuCamera.transform.position = new Vector2(vx, vy);
            }
        }
    }

    IEnumerator FadeImage(int vx, int vy, bool fadeOut)
    {
        // De opaco para transparente
        if (fadeOut)
        {
            if(salvar == true)
            {
                PlayerPrefs.SetInt("faseDesbloqueada", MenuStaticClass.menuFaseDesbloqueada);
                PlayerPrefs.SetInt("dinheiro", MenuStaticClass.menu_dinheiro);
            }

            for (float i = 1; i >= 0; i -= Time.deltaTime * 2)
            {
                // Alpha = i
                fadeImage.color = new Color(1, 1, 1, i);
                yield return null;
            }
            StopCoroutine(FadeImage(vx, vy, true));
            StopCoroutine(FadeImage(vx, vy, false));
            isFading = false;
        }
        // De transparente para opaco
        else
        {
            //yield return new WaitForSeconds(0.1f);
            isFading = true;
            for (float i = 0; i <= 1; i += Time.deltaTime * 2)
            {
                // Alpha = i
                fadeImage.color = new Color(1, 1, 1, i);
                yield return null;
            }

            if(vx == 45) // 45 = Ir para fase
            {
                if (MenuStaticClass.jogandoPelaPrimeiraVez == true && MenuStaticClass.menuFaseSelecionada == 1)
                {
                    SceneManager.LoadScene("Slides");
                }
                else
                {
                    SceneManager.LoadScene("Fase" + MenuStaticClass.menuFaseSelecionada);
                }
            }

            menuCamera.transform.position = new Vector2(vx, vy);
            StartCoroutine(FadeImage(vx, vy, true));
            ApagarTinta();

            salvar = true;

            // Mostrar todos os botões de novo e seus textos.
            foreach (GameObject botao in botoes)
            {
                botao.GetComponent<Image>().enabled = true;
            }

            foreach (GameObject texto in botoes_text)
            {
                texto.GetComponent<Text>().enabled = true;
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false && salvar == true)
        {
            PlayerPrefs.Save();
        }
    }
}