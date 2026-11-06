using System.Collections; 
using UnityEngine;
using TMPro;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego Instancia;

    [Header("Sistema de Tiros")]
    public GameObject prefabAve;
    public Transform puntoAparicion;
    public int tirosMaximos = 7;
    private int tirosRestantes;

   
    private GameObject aveActual;

    [Header("Sistema de UI y Puntos")]
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoTiros;
    private int puntosTotales = 0;

    void Awake()
    {
        if (Instancia == null) Instancia = this;
    }

    void Start()
    {
        tirosRestantes = tirosMaximos;
        ActualizarInterfaz();
        SpawnearNuevaAve();
    }

    public void SumarPuntos(int cantidad)
    {
        puntosTotales += cantidad;
        ActualizarInterfaz();
    }

    
    public void RegistrarTiro()
    {
        tirosRestantes--;
        ActualizarInterfaz();

        if (tirosRestantes > 0)
        {
            
            StartCoroutine(EsperarQueDesaparezcaAve());
        }
       
    }

    
    IEnumerator EsperarQueDesaparezcaAve()
    {
       
        while (aveActual != null)
        {
            yield return null; 
        }

        
        yield return new WaitForSeconds(1f);

      
        SpawnearNuevaAve();
    }

    

    void SpawnearNuevaAve()
    {
        
        aveActual = Instantiate(prefabAve, puntoAparicion.position, Quaternion.identity);
    }

    void ActualizarInterfaz()
    {
        if (textoPuntos != null) textoPuntos.text = "Puntos: " + puntosTotales;
        if (textoTiros != null) textoTiros.text = "Tiros: " + tirosRestantes;
    }
}