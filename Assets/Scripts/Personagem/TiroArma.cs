using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroArma : MonoBehaviour
{
    [SerializeField] float velocidadeDoTiro = 10f;

    PegarArma arma;

    BotaoResposta resposta;

    void Awake()
    {
        arma = GameObject.FindObjectOfType<PegarArma>();
    }

    void Update()
    {
        transform.Translate(new Vector2(velocidadeDoTiro * Time.deltaTime, 0));
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("barreira") && arma.podeDestruir == true)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject.FindObjectOfType<Inimigo>().BarreiraCaiu();
        }
    }
}