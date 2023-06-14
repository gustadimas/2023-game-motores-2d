using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentoJogador : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    BoxCollider2D col;
    Animator anim;
    [HideInInspector] public float dirX;
    [SerializeField] float movimentoVel;
    [SerializeField] float forcaPulo;
    [SerializeField] LayerMask podePular;
    [SerializeField] GameObject painelPerguntas;
    public GameObject pausarMenuPanel;
    enum EstadoMovimento { parado, andando, pulando, caindo};
    //Vector3 renascer;
    [SerializeField] GameObject detectorCaida;
    bool pausado = false;

    //Variaveis LançamentoPlataforma
    GameObject plataformas;
    [SerializeField] float plataformaGravidade;
    private float x, y, v0, sin, cos, t, v0x, v0y;
    [SerializeField] float angulo;
    private Vector3 s0;
    bool platPodeLancar = false;
    bool platCaindo = false;
    int ultimaPlataformaInteragida = -1;
    [HideInInspector] public bool move = true;

    private void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //renascer = transform.position;

        angulo = angulo * Mathf.PI / 180;
        sin = Mathf.Sin(angulo);
        cos = Mathf.Cos(angulo);
        t = 0;
    }

    private void Update()
    {
        //Resetar a cena do protótipo
        //if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (painelPerguntas != null)
        {
            if (painelPerguntas.activeInHierarchy == true)
                move = false;
            else
                move = true;
        }

        Movimentacao();
        LancamentoPlataforma();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                DespausarJogo();
            else
                PausarJogo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("detectorCaida"))
        {
            //transform.position = renascer;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            move = true;
        }

        if (collision.CompareTag("respostaPlataformas"))
            move = false;

        if (collision.gameObject.CompareTag("area"))
            move = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("area"))
            move = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Verdadeira")
        {
            collision.gameObject.tag = "Untagged";
            PlatVerdadeira();
            Invoke("PodeContinuar", 1f);
        }

        if (collision.gameObject.tag == "Falsa")
            PlatFalsa();

        if (collision.gameObject.tag == "Plataforma")
        {
            Plataforma plataforma = collision.gameObject.GetComponent<Plataforma>();

            //Caso o jogador pule, não ativa duas vezes.
            if (ultimaPlataformaInteragida != plataforma.indicePlataforma)
            {
                plataforma.AtivarBotoes();
                painelPerguntas.SetActive(true);
                ultimaPlataformaInteragida = plataforma.indicePlataforma;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
            painelPerguntas.SetActive(false);
    }

    public void Movimentacao()
    {
        Animacoes();
        if (move)
        {
            rb.gravityScale = 1;
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * movimentoVel, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && NoChao())
            {
                rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);

                if (!pausarMenuPanel.activeSelf)
                    AudioManager.instance.Play("Pulo");
            }
        }

        else if (!platCaindo) 
            rb.velocity = Vector2.zero;

        if (platCaindo) 
            rb.gravityScale = 1;

        detectorCaida.transform.position = new Vector2(transform.position.x, detectorCaida.transform.position.y);
    }

    bool NoChao()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.1f, podePular);
    }

    public void PausarJogo()
    {
        pausado = true;
        Time.timeScale = 0;
        pausarMenuPanel.SetActive(true);
    }

    public void DespausarJogo()
    {
        pausado = false;
        Time.timeScale = 1;
        pausarMenuPanel.SetActive(false);
    }

    public void ContinuarJogo()
    {
        Time.timeScale = 1;
        pausarMenuPanel.SetActive(false);
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //Métodos LançamentoPlataforma
    public void LancamentoPlataforma()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.tag == "Verdadeira" || hit.collider.tag == "Falsa")
                {
                    plataformas = hit.collider.gameObject;
                    platPodeLancar = true;
                }
            }
        }

        if (platPodeLancar)
        {
            rb.gravityScale = 0;
            s0 = transform.position;
            float tt = Mathf.Sqrt(Mathf.Abs(((plataformas.transform.position.x - s0.y) - (plataformas.gameObject.transform.position.y - s0.x) * Mathf.Tan(angulo)) / (-plataformaGravidade)));

            v0 = (plataformas.transform.position.x - s0.x) / (tt * Mathf.Cos(angulo));

            v0x = v0 * cos;
            v0y = v0 * sin;

            t += Time.deltaTime;

            x = s0.x + v0x * t;
            y = s0.y + v0y * t - plataformaGravidade * t * t / 2 * Mathf.Sin(Mathf.PI * t / (2 * tt));
            transform.position = new Vector2(x, y);
        }
    }

    private void Animacoes()
    {
        EstadoMovimento estado;

        if (move == false)
            dirX = 0;
        else
            dirX = Input.GetAxisRaw("Horizontal");

        if (dirX > 0f)
        {
            estado = EstadoMovimento.andando;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        else if (dirX < 0f)
        {
            estado = EstadoMovimento.andando;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else
            estado = EstadoMovimento.parado;

        if (rb.velocity.y > .1f)
            estado = EstadoMovimento.pulando;

        else if (rb.velocity.y < -.1f)
            estado = EstadoMovimento.caindo;

        anim.SetInteger("estado", (int)estado);
    }
        
    public void PlatVerdadeira()
    {
        platPodeLancar = false;
        move = false;

        GameObject[] plataformasFalsas = GameObject.FindGameObjectsWithTag("Falsa");

        foreach (GameObject plataformaFalsa in plataformasFalsas)
        {
            plataformaFalsa.GetComponent<Rigidbody2D>().isKinematic = false;
            plataformaFalsa.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void PlatFalsa()
    {
        platPodeLancar = false;
        move = false;

        Invoke("ErrouPlataforma", 1f);
    }

    public void ErrouPlataforma()
    {
        platCaindo = true;
        rb.gravityScale = 1;
        plataformas.GetComponent<Rigidbody2D>().isKinematic = false;
        plataformas.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void PodeContinuar()
    {
        move = true;
    }
}
