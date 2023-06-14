using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirPontos : MonoBehaviour
{
    [SerializeField] GameObject[] pontos;
    int pontosIndex = 0;
    [SerializeField] float speed = 2f;
    bool jogadorNaPlataforma = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Jogador")
            jogadorNaPlataforma = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Jogador")
            jogadorNaPlataforma = false;
    }

    void Update()
    {
        if (jogadorNaPlataforma)
            SeguindoPontos();
    }

    void SeguindoPontos()
    {
        if (Vector2.Distance(pontos[pontosIndex].transform.position, transform.position) < .1f)
        {
            pontosIndex++;
            if (pontosIndex >= pontos.Length)
                pontosIndex = 0;

            if (pontosIndex == 0)
                jogadorNaPlataforma = false;
        }

        if (jogadorNaPlataforma)
            transform.position = Vector2.MoveTowards(transform.position, pontos[pontosIndex].transform.position, Time.deltaTime * speed);
    }
}
