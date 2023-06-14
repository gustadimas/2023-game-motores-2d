using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarArma : MonoBehaviour
{
    [SerializeField] GameObject tiroPrefab;
    [SerializeField] Transform mao;
    [SerializeField] Transform pontoDeTiro;
    [SerializeField] float tempoDeDestruicaoDaArma = 2f;

    bool pegouArma = false;
    [HideInInspector] public bool podeDestruir = false;
    bool atirou = false;
    Rigidbody2D rb;
    Collider2D col;

    MovimentoJogador jogador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (pegouArma && !atirou && Input.GetKeyDown(KeyCode.Z))
            Atirar();

        if (pegouArma)
        {
            transform.position = mao.position;
            transform.rotation = mao.rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jogador"))
        {

            pegouArma = true;
            rb.isKinematic = true;
            col.enabled = false;

            mao = collision.transform.Find("Mao");
        }

        podeDestruir = true;
    }

    private void Atirar()
    {
        Instantiate(tiroPrefab, new Vector2(transform.position.x, transform.position.y), transform.rotation);

        atirou = true;

        if (podeDestruir)
            Invoke("DestruirArma", tempoDeDestruicaoDaArma);
    }

    private void DestruirArma()
    {
        Destroy(gameObject);
    }
}
