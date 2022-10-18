using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBotãoGaleria : MonoBehaviour
{
    void Update()
    {
        if(MenuStaticClass.menuFaseDesbloqueada > 1) // Só pode acessar a galeria se completou ao menos uma fase.
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
