using System.Collections;
using UnityEngine;

public class GerenciadorPlataformas : MonoBehaviour
{
    [SerializeField] GameObject[] plataformas;
    [SerializeField] Transform posicaoCorreta;
    [SerializeField] float suavizacaoDeMovimento = 5f;

    [HideInInspector] public int indicePlataformaAtual = -1;
    [HideInInspector] public int plataformaAnterior = -1;

    int plataformaParaDerrubar = 0;

    void Start()
    {
        DesativarPlataformas();
    }

    void DesativarPlataformas()
    {
        foreach (GameObject plataforma in plataformas)
            plataforma.SetActive(false);
    }

    void DesativarPrimeirasPlataformas(int quantidadePlataformas)
    {
        for (int i = 0; i < quantidadePlataformas; i++)
            plataformas[i].SetActive(false);
    }

    public void PassarPlataforma()
    {
        plataformaAnterior = indicePlataformaAtual;
        indicePlataformaAtual++;

        if (indicePlataformaAtual >= plataformas.Length)
        {
            indicePlataformaAtual = 0;

            // Caso o jogador passe por todas as plataformas, desativa as duas primeiras.
            DesativarPrimeirasPlataformas(2);
            return;
        }

        plataformas[indicePlataformaAtual].SetActive(true);

        SubirPlataformaParaAlturaDoJogador();
    }

    public void DerrubarPlataformaAnterior(int indicePlataforma)
    {
        int indicePlataformaAnterior = indicePlataforma - 1;
        if (indicePlataformaAnterior < 0)
            return;
        plataformaParaDerrubar = indicePlataformaAnterior;

        print($"Derrubando plataforma - indicePlataformaAnterior: {indicePlataformaAnterior} | indicePlataforma: {indicePlataforma}");

        Rigidbody2D rb = plataformas[plataformaParaDerrubar].GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        //rb.AddForce(new Vector2(0, -400f), ForceMode2D.Impulse);
        Invoke("DesativarGameObject", 8f);
    }

    void DesativarGameObject()
    {
        print($"Desativando o game object {plataformas[plataformaParaDerrubar].name}");
        plataformas[plataformaParaDerrubar].SetActive(false);
    }

    void SubirPlataformaParaAlturaDoJogador()
    {
        StartCoroutine(SubirPlataformaParaAlturaDoJogadorCoroutine());
    }

    IEnumerator SubirPlataformaParaAlturaDoJogadorCoroutine()
    {
        while (Vector3.Distance(plataformas[indicePlataformaAtual].transform.position, posicaoCorreta.position) > 0.1f)
        {
            // Movimenta a plataforma para o mesmo Y do jogador, porém com suavização e sem alterar o X e Z.
            plataformas[indicePlataformaAtual].transform.position = Vector3.Lerp(plataformas[indicePlataformaAtual].transform.position, new Vector3(plataformas[indicePlataformaAtual].transform.position.x, posicaoCorreta.position.y, plataformas[indicePlataformaAtual].transform.position.z), Time.deltaTime * suavizacaoDeMovimento);

            yield return null;
        }
        print("Corrotina acabou para a plataforma " + indicePlataformaAtual);
    }

}
