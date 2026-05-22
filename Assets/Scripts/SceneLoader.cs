using System.Collections; // Necesario para Coroutines
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para el componente Slider

public class SceneLoader : MonoBehaviour
{
    [Tooltip("Escribe aquí el nombre de la escena a cargar si usas el método LoadTargetScene().")]
    public string sceneToLoad;

    // Referencias a los objetos de la UI para la pantalla de carga. Asígnalos desde el Inspector.
    public GameObject loadingScreen;
    public Slider loadingBar;

    /// <summary>HAZ 
    /// Carga la escena definida en la variable sceneToLoad del Inspector.
    /// </summary>
    public void LoadTargetScene()
    {
        LoadSceneByName(sceneToLoad);
    }

    public void LoadSceneByName(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena no puede ser nulo o vacío.");
            return;
        }

        // Inicia la carga asíncrona usando una Coroutine
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    private IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        Debug.Log($"Iniciando carga asíncrona de la escena: {sceneName}");

        // Activa la pantalla de carga si está asignada.
        if (loadingScreen != null) loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Evita que la escena se active automáticamente al terminar de cargar
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // El progreso va de 0.0 a 0.9. El 1.0 se alcanza cuando se activa.
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log($"Cargando... {progress * 100}%");
            
            // Actualiza la barra de carga si está asignada.
            if (loadingBar != null) loadingBar.value = progress;

            // Cuando la carga llega al 90%, está lista para activarse.
            if (operation.progress >= 0.9f)
            {
                Debug.Log("Escena lista para activar. Presiona una tecla o espera para continuar...");
                // Aquí podrías esperar a que el usuario presione un botón para continuar
                // o simplemente activarla de inmediato.
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
