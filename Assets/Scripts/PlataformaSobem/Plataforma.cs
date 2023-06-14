using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Plataforma : MonoBehaviour
{
    public int indicePlataforma;
    [SerializeField] Pergunta pergunta;
    [SerializeField] TextMeshProUGUI textoPainelDePerguntas;
    [SerializeField] Button botao1, botao2, botao3;
    [SerializeField] GerenciadorPlataformas gerenciadorPlataformas;

    [SerializeField] GameObject painelPerguntas;

    string[] respostasErradas;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respostasErradas = new string[] { "723", "381", "555", "694", "102", "819", "932", "203", "677", "524" };
    }

    void OnEnable()
    {
        // Remove todos os listeners para não haver duplicação de chamadas entre as plataformas.
        botao1.onClick.RemoveAllListeners();
        botao2.onClick.RemoveAllListeners();
        botao3.onClick.RemoveAllListeners();

        // Adiciona os listeners para cada botão.
        botao1.onClick.AddListener(() => VerificarResposta(1));
        botao2.onClick.AddListener(() => VerificarResposta(2));
        botao3.onClick.AddListener(() => VerificarResposta(3));
    }

    public void AtivarBotoes()
    {
        botao1.gameObject.SetActive(true);
        botao2.gameObject.SetActive(true);
        botao3.gameObject.SetActive(true);

        print($"[{gameObject.name}] Indice plataforma: {indicePlataforma} | Indice plataforma atual: {gerenciadorPlataformas.indicePlataformaAtual} | Indice plataforma anterior: {gerenciadorPlataformas.plataformaAnterior}");

        gerenciadorPlataformas.DerrubarPlataformaAnterior(indicePlataforma);

        ExibirPergunta();
    }

    void ExibirPergunta()
    {
        textoPainelDePerguntas.text = pergunta.pergunta;

        string respostaCorreta = pergunta.resposta;

        List<Button> botoes = new List<Button> { botao1, botao2, botao3 };

        // Atribuir a resposta correta a um botão aleatório
        int indiceBotaoRespostaCorreta = Random.Range(0, botoes.Count);
        botoes[indiceBotaoRespostaCorreta].GetComponentInChildren<TextMeshProUGUI>().text = respostaCorreta;
        botoes.RemoveAt(indiceBotaoRespostaCorreta);

        List<int> indicesJaSorteados = new List<int>();

        // Atribuir respostas erradas aos demais botões
        for (int i = 0; i < botoes.Count; i++)
        {
            int indiceRespostaErrada = Random.Range(0, respostasErradas.Length);

            while (indicesJaSorteados.Contains(indiceRespostaErrada))
                indiceRespostaErrada = Random.Range(0, respostasErradas.Length);

            indicesJaSorteados.Add(indiceRespostaErrada);
            botoes[i].GetComponentInChildren<TextMeshProUGUI>().text = respostasErradas[indiceRespostaErrada];
        }
    }

    void VerificarResposta(int botao)
    {
        DesativarBotoes();

        string textoNoBotao = ObterTextoNoBotao(botao);
        if (textoNoBotao == pergunta.resposta)
        {
            gerenciadorPlataformas.PassarPlataforma();
            painelPerguntas.SetActive(false);
            print($"[{gameObject.name}] Acertou e passou de plataforma!");
        }
        else
        {
            print($"[{gameObject.name}] Errou e caiu! Plataforma: " + this.gameObject.name);
            painelPerguntas.SetActive(false);

            rb.bodyType = RigidbodyType2D.Dynamic;
            //rb.AddForce(new Vector2(0, -400f), ForceMode2D.Impulse);

            Invoke("DesativarGameObject", 8f);
        }
    }

    string ObterTextoNoBotao(int botao)
    {
        string textoNoBotao;

        if (botao == 1)
            textoNoBotao = botao1.GetComponentInChildren<TextMeshProUGUI>().text;
        else if (botao == 2)
            textoNoBotao = botao2.GetComponentInChildren<TextMeshProUGUI>().text;
        else
            textoNoBotao = botao3.GetComponentInChildren<TextMeshProUGUI>().text;
        return textoNoBotao;
    }

    void DesativarBotoes()
    {
        botao1.gameObject.SetActive(false);
        botao2.gameObject.SetActive(false);
        botao3.gameObject.SetActive(false);
    }

    void DesativarGameObject()
    {
        print($"[{gameObject.name}] Desativando o game object!");
        gameObject.SetActive(false);
    }
}
