using UnityEngine;

public class Cerdos: MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("Configuración de Vida")]
    public int vidaMaxima = 3;
    private int vidaActual;

    [Header("Sprites de Estados")]

    public Sprite Normal;
    public Sprite lastimado; 
    public Sprite spritePoof;  

    [Header("Puntaje")]
    public int puntosAlMorir = 100;
    public float fuerzaNecesariaParaGolpe = 2f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMaxima;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.relativeVelocity.magnitude > fuerzaNecesariaParaGolpe)
        {
            vidaActual--; 

             if (vidaActual == 2)
            { 
                if (Normal != null)
                {
                    spriteRenderer.sprite = Normal;
                }
            }

            else if (vidaActual == 1)
            {
                
                if (lastimado != null)
                {
                    spriteRenderer.sprite = lastimado;
                }
            }
            else if (vidaActual <= 0)
            {
                
                Poof();
            }
        }
    }

    void Poof()
    {
        
        if (ControladorJuego.Instancia != null)
        {
            ControladorJuego.Instancia.SumarPuntos(puntosAlMorir);
        }

        
        if (spritePoof != null)
        {
            spriteRenderer.sprite = spritePoof;
        }

      
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        
        Destroy(gameObject, 0.3f);
    }
}