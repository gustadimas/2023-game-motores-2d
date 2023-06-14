using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovimentando : MonoBehaviour
{
    [SerializeField] GameObject[] pontos;
    int pontosIndex = 0;
    [SerializeField] float speed = 2f;

    void Update()
    {
        SeguindoPontos();
    }

    void SeguindoPontos()
    {
        if (Vector2.Distance(pontos[pontosIndex].transform.position, transform.position) < .1f)
        {
            pontosIndex++;
            if (pontosIndex >= pontos.Length)
                pontosIndex = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, pontos[pontosIndex].transform.position, Time.deltaTime * speed);
    }
}
