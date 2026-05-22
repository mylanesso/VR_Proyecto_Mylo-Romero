using UnityEngine;
using Oculus.Interaction;

public class DragItem : MonoBehaviour
{
    // PUNTAJE GLOBAL COMPARTIDO ENTRE TODOS LOS OBJETOS
    public static int puntajeGlobal = 0;

    [Header("Nombre del Objeto")]
    public string nombreDelObjeto = "ObjetoSearch";

    private Rigidbody rb;

    private void Awake()
    {
        ConfiguracionComponentes();
    }

    private void ConfiguracionComponentes()
    {
        // ---------- RIGIDBODY ----------
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.isKinematic = true;

        // ---------- GRABBABLE ----------
        if (GetComponent<Grabbable>() == null)
        {
            gameObject.AddComponent<Grabbable>();
        }

        // ---------- GRAB INTERACTABLE ----------
        if (GetComponent<GrabInteractable>() == null)
        {
            var interactable = gameObject.AddComponent<GrabInteractable>();

            // Conecta el Rigidbody al sistema de interacción XR
            interactable.InjectRigidbody(rb);
        }

        // ---------- COLLIDER ----------
        Collider col = GetComponent<Collider>();

        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }

        // IMPORTANTE PARA OnTriggerEnter
        col.isTrigger = true;
    }

    // =====================================================
    // CUANDO ALGO TOCA ESTE OBJETO
    // =====================================================

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo tocó el objeto: " + other.name);

        // SI EL OBJETO QUE TOCÓ TIENE TAG PLAYER
        if (other.CompareTag("MainCamera"))
        {
            RecoleccionObjetos();
        }
    }

    // =====================================================
    // FUNCIÓN DE RECOLECCIÓN
    // =====================================================

    public void RecoleccionObjetos()
    {
        puntajeGlobal++;

        Debug.Log("Objeto Encontrado! Puntaje: " + puntajeGlobal);

        AlSerRecogido();

        Destroy(gameObject);
    }

    // =====================================================
    // MENSAJE EXTRA
    // =====================================================

    public void AlSerRecogido()
    {
        Debug.Log("Has recogido el objeto: " + nombreDelObjeto);
    }
}