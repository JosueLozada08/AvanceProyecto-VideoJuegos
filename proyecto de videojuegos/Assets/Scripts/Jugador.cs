using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento lateral
    public float fuerzaSalto = 2f; // Fuerza del salto
    public LayerMask capaSuelo; //para esto se creo el layer en personaje 
    public float MaxSaltos;
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private bool puedeSaltar = true; // Para controlar si el jugador puede saltar
    private float suelo = -2.791826f; // Posición Y del suelo
    private bool derecha = true; //direccion inicial del personaje 
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float RestSaltos; //mantiene la cuenta de los saltos restantes que tiene el jugador cuando esta en el aire 


   // private float epsilon = 0.1f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener la referencia al Rigidbody2D
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        RestSaltos = MaxSaltos;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento lateral
        float movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        //condicion para que detecte el movimiento del jugador 

        if (movimiento != 0f)
        {
            animator.SetBool("Running", true);

        }
        else
        {
            animator.SetBool("Running", false);
        }



       /* if (Mathf.Abs(transform.position.x - 1.04f) <= epsilon && Mathf.Abs(transform.position.y - -7.18f) <= epsilon)
        {
            // Si es así, mueve al jugador a la posición x=-4, y=-5.3
            transform.position = new Vector2(-4, -5.3f);
        }

        */




        /*  // Salto
          if (Input.GetKeyDown(KeyCode.Space) && puedeSaltar)
          {
              rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
              puedeSaltar = false;
          }*/


        //orientacion del jugarod 
        Orientacion(movimiento);


        //funcion del salto 

        salto();

       



    }

    /* void OnCollisionEnter2D(Collision2D collision)
     {
         // Detectar si el jugador toca el suelo
         if (collision.gameObject.tag == "Suelo" && transform.position.y <= suelo)
         {
             Debug.Log("Tocando el suelo");
             puedeSaltar = true;
         }
     }*/


    //funcion para la orientacion segun el movimiento 
    /*  void Orientacion(float movimiento)
      {

          if ((derecha=true && movimiento<0)||(derecha = false && movimiento >0))
          {
              derecha = !derecha;
              transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
          }
      }*/


    void Orientacion(float movimiento)
    {
        // Obtener la escala actual del jugador
        Vector3 escalaActual = transform.localScale;

        // Si el movimiento es hacia la izquierda y el jugador no está orientado hacia la izquierda
        if (movimiento < 0 && escalaActual.x > 0)
        {
            // Voltear el sprite hacia la izquierda
            escalaActual.x = -Mathf.Abs(escalaActual.x);
        }
        // Si el movimiento es hacia la derecha y el jugador no está orientado hacia la derecha
        else if (movimiento > 0 && escalaActual.x < 0)
        {
            // Voltear el sprite hacia la derecha
            escalaActual.x = Mathf.Abs(escalaActual.x);
        }

        // Aplicar la nueva escala al jugador
        transform.localScale = escalaActual;
    }


    //funcion booleana para que salte solo cuando esta tocando el suelo 
    //La función realiza la misma verificación: si el objeto colisiona con el suelo, devuelve true; de lo contrario, devuelve false.
    bool TocaSuelo()
    {
      RaycastHit2D raycastHit =  Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x , boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);

        return raycastHit.collider != null;

        

    }

    
    //funcion salto del personaje 

    void salto()
    {

        if (TocaSuelo())
        {
            RestSaltos = MaxSaltos;
        }
        if (Input.GetKeyDown(KeyCode.Space) && RestSaltos >0 )
        {
            RestSaltos--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f); //arreglar el bug de la gravedad con los saltos del personaje 
            rigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse );
            
        }
    }

    

   
}