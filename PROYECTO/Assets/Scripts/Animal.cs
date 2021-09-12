using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool movingRight;
    [SerializeField] GameManager gm;
    [SerializeField] public int PuntosVida;
    bool conta = true;
    int copia;
    float tiempo = 0;
    float timescale = 5;
    float minX, maxX;
    int contador = 1;
    int copia2;



    // Start is called before the first frame update
    void Start()
    {
        copia2 = contador;
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;
        copia = PuntosVida;
    }

    // Update is called once per frame
    void Update()       
    {
        if (conta == true)
        {
            poder();
        }
        if (movingRight)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        if (transform.position.x >= maxX)
        {
            movingRight = false;
        }
        else if (transform.position.x <= minX)
        {
            movingRight = true;
        }
    }   
    void poder()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.unscaledTime >= tiempo)
        {
            Time.timeScale = 0.5f;
            PuntosVida = 1;
            tiempo = Time.unscaledTime + timescale;
            copia2 = copia2 + 1;
        }
        if (tiempo <= Time.unscaledTime)
        {            
            Time.timeScale = 1f;
            PuntosVida = copia;
            if (copia2 == 4)
            {
                conta = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;

        if ( etiqueta == "DISPARO") 
        {
            PuntosVida--;
            if (PuntosVida == 0)
            {
                (GameObject.Find("GameManager").GetComponent<GameManager>()).ReducirNumEnemigos();
                Destroy(this.gameObject);
            }
        }
    }
   
}
