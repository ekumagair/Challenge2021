using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFimDeJogoContinuar : MonoBehaviour
{
    public int custo = 9000;
    Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
    }

    private void Update()
    {
        if(custo > MenuStaticClass.menu_dinheiro)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }
}
