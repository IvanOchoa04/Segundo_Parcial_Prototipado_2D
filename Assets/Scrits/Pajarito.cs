using UnityEngine;


public class Pajarito : MonoBehaviour
{
    Rigidbody2D RB;
    SpriteRenderer SR;

    [Header ("Sprites")]
    public Sprite Normal;
    public Sprite Arrastre;
    public Sprite Lastimado;
    public Sprite poof;


    public float AgarreMax;
    public float Velocidad;

    Vector2 PosInicial;
    Vector2 PosActual;
    Vector2 Direccion; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        PosInicial = RB.position;
        if (Normal != null) SR.sprite = Normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        RB.bodyType = RigidbodyType2D.Kinematic;
        RB.linearVelocity = Vector2.zero;

        if (Arrastre != null) SR.sprite = Arrastre;
    }

    private void OnMouseDrag() 
    {

        Vector3 mousePosicion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 PosicionEsp = mousePosicion;

        float Distancia = Vector2.Distance(PosicionEsp, PosInicial);
        if (Distancia > AgarreMax)
        {
            Direccion = PosicionEsp - PosInicial;
            Direccion.Normalize();
            PosicionEsp = PosInicial + (Direccion * AgarreMax);
        }

        if (PosicionEsp.x > PosInicial.x)
        {
            PosicionEsp.x = PosInicial.x;
        }

        RB.position = PosicionEsp;


    }

    private void OnMouseUp()
    {
        RB.bodyType = RigidbodyType2D.Dynamic;
        PosActual = RB.position;
        Direccion = PosInicial - PosActual;
        Direccion.Normalize();
        RB.AddForce(Direccion * Velocidad);

        if (ControladorJuego.Instancia != null)
        {
            ControladorJuego.Instancia.RegistrarTiro();
        }

            Invoke("Poof", 5f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 1f && Lastimado != null)
        {
            SR.sprite = Lastimado;
        }
    }

    void Poof()
    {
        if (poof !=null)
        {
            SR.sprite = poof;
        }

        RB.linearVelocity = Vector2.zero;
        RB.bodyType = RigidbodyType2D.Kinematic;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Destroy(gameObject, 0.3f); 
    }


}
