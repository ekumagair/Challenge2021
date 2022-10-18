using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    Canvas _c;
    int foto = 0;
    public static bool tocou;
    public Text mensagemTexto;

    public GameObject[] hs;
    public GameObject vidasFundo2;
    public GameObject vidasFundo3;

    public GameObject sairDir;
    public GameObject sairEsq;

    public GameObject joyStickFundo;
    public GameObject joyStickCentro;

    public GameObject pausarDir;
    public GameObject pausarEsq;

    private void Start()
    {
        _c = GetComponent<Canvas>();
        tocou = false;

        if (MenuStaticClass.menuConfigPosVidas2 == false && vidasFundo3 != null)
        {
            Destroy(vidasFundo3);
        }
        else if(vidasFundo2 != null)
        {
            Destroy(vidasFundo2);
        }

        if (MenuStaticClass.menuConfigPosSairEsq == false && pausarEsq != null)
        {
            Destroy(pausarEsq);
        }
        else if(pausarDir != null)
        {
            Destroy(pausarDir);
        }

        if (MenuStaticClass.menuConfigPosSairEsq == false && sairEsq != null)
        {
            Destroy(sairEsq);
        }
        else if(sairDir != null)
        {
            Destroy(sairDir);
        }

        if (MenuStaticClass.menuConfigJoyStick == false)
        {
            Destroy(joyStickFundo);
            Destroy(joyStickCentro);
        }
    }

    private void Update()
    {
        // Comandos de teste.
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _c.enabled = false;
            EsconderSpritesHud(false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _c.enabled = true;
            EsconderSpritesHud(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MenuStaticClass.menu_dinheiro += 9000;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            MenuStaticClass.menuFaseDesbloqueada--;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MenuStaticClass.menuFaseDesbloqueada++;
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            MenuStaticClass.menuFaseDesbloqueada = 1;
            MenuStaticClass.menu_dinheiro = 0;
            MenuStaticClass.jogandoPelaPrimeiraVez = true;
            MenuStaticClass.comprouVel = false;
            MenuStaticClass.comprouTiros = false;
            MenuStaticClass.comprouRes = false;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        // Tirar captura de tela. Comando de teste.
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Capturou tela");
            ScreenCapture.CaptureScreenshot("foto geometry painter " + foto + " " + Random.Range(0, 10000) + ".png");
            foto++;
        }
        
        // Fim dos comandos de teste.

        if (mensagemTexto != null)
        {
            if (tocou == false)
            {
                mensagemTexto.text = "Toque para se mover!";
            }
            else if(Time.timeScale == 0 && FaseEventosScript.completouFaseExecutou == false)
            {
                mensagemTexto.text = "Jogo pausado.";
            }
            else
            {
                mensagemTexto.text = "";
            }
        }
    }

    private void EsconderSpritesHud(bool yn)
    {
        hs = GameObject.FindGameObjectsWithTag("HudSprite");

        if (hs != null)
        {
            foreach (GameObject s in hs)
            {
                s.GetComponent<SpriteRenderer>().enabled = yn;
            }
        }
    }
}
