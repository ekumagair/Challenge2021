using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormaMovimentoEspecifico : MonoBehaviour
{
    public bool usandoMovimentoEspecifico = false;
    public Vector2 destinoEspecifico;
    public float velocidade = 4f;

    void Update()
    {
        if(usandoMovimentoEspecifico == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinoEspecifico, velocidade * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Solido" && usandoMovimentoEspecifico == true)
        {
            destinoEspecifico = transform.position;
            usandoMovimentoEspecifico = false;
            Debug.Log("Parou movimento");
        }
    }
}
