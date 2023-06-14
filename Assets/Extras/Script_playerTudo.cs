using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_playerTudo : MonoBehaviour
{
    public float angulo;
    [SerializeField] float g;
    GameObject obj_plat;
    private float x, y, v0, sin, cos, t, v0x, v0y;
    private Vector3 s0;
    bool pode = false;
    Rigidbody2D rb;
    bool move = true;
    bool caindo = false;

    void Start()
    {
        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody2D>();

        angulo = angulo * Mathf.PI / 180;
        sin = Mathf.Sin(angulo);
        cos = Mathf.Cos(angulo);
        t = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (_hit.collider)
            {
                obj_plat = _hit.collider.gameObject;
                pode = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (pode)
        {
            rb.gravityScale = 0;
            s0 = transform.position;
            float tt = Mathf.Sqrt(Mathf.Abs(((obj_plat.transform.position.x - s0.y) - (obj_plat.gameObject.transform.position.y - s0.x) * Mathf.Tan(angulo)) / (-g)));

            v0 = (obj_plat.transform.position.x - s0.x) / (tt * Mathf.Cos(angulo));

            v0x = v0 * cos;
            v0y = v0 * sin;

            t += Time.deltaTime;

            x = s0.x + v0x * t;
            y = s0.y + v0y * t - g * t * t / 2 * Mathf.Sin(Mathf.PI * t / (2 * tt));
            transform.position = new Vector2(x, y);
        }

        if (move)
        {
            rb.gravityScale = 1;
            if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 5, rb.velocity.y);
        }
        else if (!caindo) rb.velocity = Vector2.zero;
        
        if (caindo) rb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Verdadeira")
        {
            pode = false;
            move = false;

            GameObject[] _obj_platsFalsas = GameObject.FindGameObjectsWithTag("Falsa");

            foreach (GameObject _obj_platFalsa in _obj_platsFalsas)
            {
                _obj_platFalsa.GetComponent<Rigidbody2D>().isKinematic = false;
                _obj_platFalsa.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            Invoke("PodeContinuar", 1f);
        }

        if (collision.gameObject.tag == "Falsa")
        {
            pode = false;
            move = false;

            Invoke("Errou", 1f);
        }
    }

    void Errou()
    {
        caindo = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        obj_plat.GetComponent<Rigidbody2D>().isKinematic = false;
        obj_plat.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    void PodeContinuar()
    {
        move = true;
    }
}