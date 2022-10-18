using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaseEventosScript : MonoBehaviour
{
    public Camera cam; // Main camera.
    bool camChegouNoZoom = false;
    public float zoomFaseCompleta; // Zoom da câmera quando a fase é completada.
    public float zoomAjuda = 10f;
    public int inimigosRestantes;
    public int inimigosRestantesSetas; // Quantos inimigos precisam estar vivos para gerar as setas.
    public static bool inimigosRestantesSetasCriou = false;
    public GameObject seta; // Game Object da seta.
    public bool completouFase;
    public static bool completouFaseExecutou = false;
    public bool perdeuFase;
    public static bool perdeuFaseExecutou = false;

    public bool ajudaZoomOutIr = false;
    public bool ajudaZoomOutVoltar = false;

    GameObject jogador; // Game Object do jogador.
    GameObject[] tiros;
    GameObject[] tirosIni;
    GameObject[] solidos;
    GameObject[] itens;
    GameObject[] manchas;
    GameObject[] inimigos;
    GameObject[] hudSair;
    public GameObject destruir; // Game Object que destroi inimigos. (Usado na opção de continuar)
    public Text contadorIni;
    public Text contadorFase;
    public Text contadorDinheiro;

    public Vector2[] posicoesInimigosFase; // Usado na opção de continuar.

    AudioSource _as;
    public AudioClip[] audioClips; // Lista com todos os efeitos sonoros.
    GameObject musica;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        solidos = GameObject.FindGameObjectsWithTag("Solido");
        itens = GameObject.FindGameObjectsWithTag("Item");
        musica = GameObject.FindGameObjectWithTag("Musica");
        hudSair = GameObject.FindGameObjectsWithTag("HudSair");
        _as = GetComponent<AudioSource>();
        cam.orthographicSize = 6.5f; // Zoom inicial da câmera.
        camChegouNoZoom = false;
        completouFase = false;
        completouFaseExecutou = false;
        inimigosRestantesSetasCriou = false;
        perdeuFase = false;
        perdeuFaseExecutou = false;
        ajudaZoomOutIr = false;
        ajudaZoomOutVoltar = false;
        Debug.Log(zoomFaseCompleta);
        Debug.Log("Partículas: " + MenuStaticClass.menuConfigParticulas);
        Debug.Log("SFX: " + MenuStaticClass.menuConfigSFX);

        inimigosRestantesSetas = 10;

        if(MenuStaticClass.menuFaseInfinita == false)
        {
            contadorFase.text = "Fase " + MenuStaticClass.menuFaseSelecionada.ToString();
        }
        else
        {
            contadorFase.text = "Fase Infinita";
        }

        if (MenuStaticClass.menuContinuou == true)
        {
            StartCoroutine(Continuou());
        }

        if(MenuStaticClass.menuConfigMusica == false)
        {
            Destroy(musica);
        }

        PlayerPrefs.Save();
    }

    IEnumerator Continuou()
    {
        // Destruir inimigos após continuar jogo.
        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < MenuStaticClass.posicoesInimigos.Length; i++)
        {
            var triggerCriado = Instantiate(destruir, MenuStaticClass.posicoesInimigos[i], transform.rotation);
            triggerCriado.transform.position = MenuStaticClass.posicoesInimigos[i];
            Debug.Log("Criou trigger " + i + " ; " + MenuStaticClass.posicoesInimigos[i]);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        // Mínimo de zoom da câmera.
        if(zoomFaseCompleta < 6.5)
        {
            zoomFaseCompleta = 6.5f;
        }

        // Vencer fase se todos os inimigos são destruídos.
        if(inimigosRestantes <= 0 && MenuStaticClass.menuFaseInfinita == false)
        {
            completouFase = true;
        }

        // Destruir tiros se não estiver jogando.
        if (completouFase || perdeuFase)
        {
            ajudaZoomOutIr = false;
            ajudaZoomOutVoltar = false;

            if (tiros == null)
            {
                tiros = GameObject.FindGameObjectsWithTag("Tiro");
                tirosIni = GameObject.FindGameObjectsWithTag("TiroInimigo");
            }
            jogador.GetComponent<SpriteRenderer>().enabled = false;
            foreach (GameObject tiro in tiros)
            {
                Destroy(tiro);
            }
            foreach (GameObject tiroini in tirosIni)
            {
                Destroy(tiroini);
            }
            Hud.tocou = true;
        }

        // Isto só executa uma vez, no momento em que a fase é vencida.
        if (completouFase && !completouFaseExecutou)
        {
            completouFaseExecutou = true;
            MenuStaticClass.menuContinuou = false;
            PlayerPrefs.SetInt("jogandoPelaPrimeiraVez", 0);
            MenuStaticClass.jogandoPelaPrimeiraVez = false;

            if (manchas == null)
            {
                manchas = GameObject.FindGameObjectsWithTag("Splat");
            }
            foreach (GameObject s in solidos)
            {
                Destroy(s);
            }
            foreach (GameObject i in itens)
            {
                Destroy(i);
            }
            foreach (GameObject m in manchas)
            {
                Color opaco = m.GetComponent<SpriteRenderer>().color;
                opaco.a = 255f;
                m.GetComponent<SpriteRenderer>().color = opaco;
            }
            foreach (GameObject sair in hudSair)
            {
                Destroy(sair);
            }

            if ((MenuStaticClass.menuFaseSelecionada == MenuStaticClass.menuFaseDesbloqueada) && MenuStaticClass.menuFaseInfinita == false)
            {
                MenuStaticClass.menuFaseDesbloqueada++;
            }

            PlayerPrefs.SetInt("comprouVel", 0);
            PlayerPrefs.SetInt("comprouTiros", 0);
            PlayerPrefs.SetInt("comprouRes", 0);
            PlayerPrefs.SetInt("faseDesbloqueada", MenuStaticClass.menuFaseDesbloqueada);
            PlayerPrefs.SetInt("dinheiro", MenuStaticClass.menu_dinheiro);
        }

        if (MenuStaticClass.menuFaseInfinita == false)
        {
            contadorIni.text = "Inimigos restantes: " + inimigosRestantes.ToString();
        }
        else
        {
            contadorIni.text = "Número de inimigos: " + inimigosRestantes.ToString();
        }

        // Contador de dinheiro.
        contadorDinheiro.text = "$: " + MenuStaticClass.menu_dinheiro.ToString();
    }

    void LateUpdate()
    {
        // Zoom da câmera
        if(completouFase)
        {
            Zoom(zoomFaseCompleta, 1f, true, true);
        }
        if (ajudaZoomOutIr)
        {
            Zoom(zoomAjuda, 1f, false, false);
        }
        if (ajudaZoomOutVoltar)
        {
            Zoom(6.5f, 1f, false, false);
        }
    }

    void Zoom(float zoomDestino, float vel, bool centralizarCam, bool completarFase)
    {
        if (centralizarCam == true)
        {
            cam.transform.position = new Vector3(0, 0, cam.transform.position.z); // Centralizar a câmera.
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomDestino, Time.deltaTime * vel); // Afastar a câmera.

        // * 0.98f = Margem de erro de 2% para o valor do zoom.
        if (cam.orthographicSize >= zoomDestino * 0.98f && camChegouNoZoom == false) // Isto só executa uma vez.
        {
            if(completarFase == true) 
            {
                camChegouNoZoom = true;
                StartCoroutine(CompletouFase(5f));
            }
        }
    }

    IEnumerator CompletouFase(float t)
    {
        yield return new WaitForSeconds(t);
        MenuStaticClass.menuIrParaFases = true;
        MenuStaticClass.menuFaseInfinita = false;
        SceneManager.LoadScene("Menus");
    }

    public void TocarSom(int s)
    {
        if (MenuStaticClass.menuConfigSFX == true)
        {
            _as.clip = audioClips[s];
            _as.PlayOneShot(_as.clip);
        }
    }

    public void GerarSetas()
    {
        inimigos = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject ini in inimigos)
        {
            GerarSetaApontandoPara(ini, false);
        }
    }

    public void GerarSetaApontandoPara(GameObject go, bool temp)
    {
        var seta_go = Instantiate(seta, jogador.transform.position, jogador.transform.rotation);
        seta_go.transform.SetParent(jogador.transform);
        seta_go.GetComponent<Seta>().alvo = go;
        seta_go.GetComponent<Seta>().temporario = temp;
    }

    public void PausarOuContinuarJogo()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
        else
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
    }
}
