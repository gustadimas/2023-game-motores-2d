using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoDialogo;
    [SerializeField] string[] falas;
    [SerializeField] float delayEntreLetras;
    [SerializeField] GameObject CaixaDeDialogo;
    [SerializeField] GameObject jogador;

    [SerializeField] GerenciadorPlataformas gerenciadorPlataformas;

    bool dialogoAtivo = false;
    int index = 0;

    void Start()
    {
        CaixaDeDialogo.SetActive(false);
        textoDialogo.text = "";
    }

    private void Update()
    {
        if (dialogoAtivo)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    if (textoDialogo.text != falas[index])
                    {
                        StopAllCoroutines();
                        textoDialogo.text = falas[index];
                    }
                    else if (index < falas.Length - 1)
                    {
                        index++;
                        StartCoroutine(TipoDeFala(falas[index]));
                    }
                    else
                    {
                        dialogoAtivo = false;
                        index = 0;
                        CaixaDeDialogo.SetActive(false);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jogador"))
        {
            jogador.GetComponent<Rigidbody2D>().isKinematic = true;
            jogador.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            CaixaDeDialogo.SetActive(true);
            index = 0;
            IniciarDialogo();
        }

        if (collision.CompareTag("jogador"))
        {
            if(gerenciadorPlataformas != null)
                gerenciadorPlataformas.PassarPlataforma();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        jogador.GetComponent<Rigidbody2D>().isKinematic = false;
        jogador.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        CaixaDeDialogo.SetActive(false);
        dialogoAtivo = false;
        index = 0;
    }

    IEnumerator TipoDeFala(string fala)
    {
        textoDialogo.text = "";

        foreach (char letra in fala)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(delayEntreLetras);
        }
    }

    public void IniciarDialogo()
    {
        dialogoAtivo = true;
        StartCoroutine(TipoDeFala(falas[index]));
    }
}
