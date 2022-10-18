using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour
{
    public bool ativado;
    public byte minhaOpcao;
    public Text meuTexto;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.1f);

        if (minhaOpcao == 0)
        {
            if (MenuStaticClass.menuConfigSFX == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 1)
        {
            if (MenuStaticClass.menuConfigParticulas == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 2)
        {
            if (MenuStaticClass.menuConfigInverter == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 3)
        {
            if (MenuStaticClass.menuConfigCentralizar == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 4)
        {
            if (MenuStaticClass.menuConfigPosVidas == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 5)
        {
            if (MenuStaticClass.menuConfigAfastarCam == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 6)
        {
            if (MenuStaticClass.menuConfigPosSairEsq == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 7)
        {
            if (MenuStaticClass.menuConfigPosVidas2 == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 8)
        {
            if (MenuStaticClass.menuConfigJoyStick == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
        else if (minhaOpcao == 9)
        {
            if (MenuStaticClass.menuConfigMusica == true)
            {
                ativado = true;
            }
            else
            {
                ativado = false;
            }
        }
    }

    private void Update()
    {
        if (MenuScript.salvar == true)
        {
            if (ativado)
            {
                meuTexto.text = "Ativado";
                meuTexto.color = Color.green;
                MudarMinhaConfig(true);
            }
            else
            {
                meuTexto.text = "Desativado";
                meuTexto.color = Color.red;
                MudarMinhaConfig(false);
            }

            PlayerPrefs.SetInt("config" + minhaOpcao, MenuScript.BoolParaInt(ativado));
        }
    }

    public void ClicouConfig()
    {
        if (ativado)
        {
            ativado = false;
        }
        else
        {
            ativado = true;
        }
        Debug.Log(ativado + " " + minhaOpcao);
        Debug.Log("config" + minhaOpcao);
    }

    private void MudarMinhaConfig(bool b)
    {
        if (minhaOpcao == 0)
        {
            MenuStaticClass.menuConfigSFX = b;
        }
        else if (minhaOpcao == 1)
        {
            MenuStaticClass.menuConfigParticulas = b;
        }
        else if (minhaOpcao == 2)
        {
            MenuStaticClass.menuConfigInverter = b;
        }
        else if (minhaOpcao == 3)
        {
            MenuStaticClass.menuConfigCentralizar = b;
        }
        else if (minhaOpcao == 4)
        {
            MenuStaticClass.menuConfigPosVidas = b;
        }
        else if (minhaOpcao == 5)
        {
            MenuStaticClass.menuConfigAfastarCam = b;
        }
        else if (minhaOpcao == 6)
        {
            MenuStaticClass.menuConfigPosSairEsq = b;
        }
        else if (minhaOpcao == 7)
        {
            MenuStaticClass.menuConfigPosVidas2 = b;
        }
        else if (minhaOpcao == 8)
        {
            MenuStaticClass.menuConfigJoyStick = b;
        }
        else if (minhaOpcao == 9)
        {
            MenuStaticClass.menuConfigMusica = b;
        }
    }
}