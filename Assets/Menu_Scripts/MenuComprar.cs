using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuComprar : MonoBehaviour
{
    public int meuValor;
    public int meuPoder;
    Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
    }

    private void Update()
    {
        if(meuPoder == 0 && MenuStaticClass.comprouVel)
        {
            btn.interactable = false;
        }
        else if (meuPoder == 1 && MenuStaticClass.comprouTiros)
        {
            btn.interactable = false;
        }
        else if (meuPoder == 2 && MenuStaticClass.comprouRes)
        {
            btn.interactable = false;
        }
        else if (meuValor > MenuStaticClass.menu_dinheiro)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    public void Comprou()
    {
        MenuStaticClass.menu_dinheiro -= meuValor;
        if (meuPoder == 0 && !MenuStaticClass.comprouVel)
        {
            Debug.Log("Comprou velocidade");
            MenuStaticClass.comprouVel = true;
        }
        else if (meuPoder == 1 && !MenuStaticClass.comprouTiros)
        {
            Debug.Log("Comprou tiros");
            MenuStaticClass.comprouTiros = true;
        }
        else if (meuPoder == 2 && !MenuStaticClass.comprouRes)
        {
            Debug.Log("Comprou resistência");
            MenuStaticClass.comprouRes = true;
        }

        PlayerPrefs.SetInt("dinheiro", MenuStaticClass.menu_dinheiro);

        if(MenuStaticClass.comprouVel == true)
        {
            PlayerPrefs.SetInt("comprouVel", 1);
        }
        else
        {
            PlayerPrefs.SetInt("comprouVel", 0);
        }

        if (MenuStaticClass.comprouTiros == true)
        {
            PlayerPrefs.SetInt("comprouTiros", 1);
        }
        else
        {
            PlayerPrefs.SetInt("comprouTiros", 0);
        }

        if (MenuStaticClass.comprouRes == true)
        {
            PlayerPrefs.SetInt("comprouRes", 1);
        }
        else
        {
            PlayerPrefs.SetInt("comprouRes", 0);
        }
    }
}