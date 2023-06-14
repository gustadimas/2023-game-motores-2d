using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrulha : MonoBehaviour
{
    public float velocidade;
    public bool patrulhar;
    public bool virar;
    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    void Start()
    {
        patrulhar = true;   
    }

    void Update()
    {
        if (patrulhar)
            Patrol();
    }

    private void FixedUpdate()
    {
        if (patrulhar)
            virar = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
    }

    private void Patrol()
    {
        if (virar)
        {
            patrulhar = false;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            velocidade *= -1;
            patrulhar = true;
        }

        rb.velocity = new Vector2 (velocidade * Time.fixedDeltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "jogador")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
