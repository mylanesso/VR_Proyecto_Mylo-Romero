using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerVR : MonoBehaviour
{
    [Header("Objetivo")]
    public int objetosNecesarios = 5;

    [Header("UI Victoria")]
    public GameObject panelVictoria;

    [Header("Escena Lobby")]
    public string nombreLobby = "Lobby";

    [Header("Tiempo antes de regresar")]
    public float tiempoEspera = 5f;

    private bool victoriaActivada = false;

    private void Start()
    {
        // Oculta el panel al iniciar
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }
    }

    private void Update()
    {
        // Evita que se active varias veces
        if (victoriaActivada)
            return;

        // Revisa si ya alcanzó los objetos necesarios
        if (DragItem.puntajeGlobal >= objetosNecesarios)
        {
            victoriaActivada = true;

            Debug.Log("¡Victoria!");

            // Mostrar UI de éxito
            if (panelVictoria != null)
            {
                panelVictoria.SetActive(true);
            }

            // Iniciar regreso al lobby
            StartCoroutine(RegresarAlLobby());
        }
    }

    private IEnumerator RegresarAlLobby()
    {
        yield return new WaitForSeconds(tiempoEspera);

        // Reinicia el puntaje
        DragItem.puntajeGlobal = 0;

        // Carga el lobby
        SceneManager.LoadScene(nombreLobby);
    }
}