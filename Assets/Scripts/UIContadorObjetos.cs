using UnityEngine;
using TMPro; // Usa TextMeshPro

public class UIContadorObjetos : MonoBehaviour
{
    [Header("Texto de la UI")]
    public TextMeshProUGUI textoPuntaje;

    private void Update()
    {
        // Obtiene el puntaje global del script DragItem
        textoPuntaje.text = "Objetos encontrados: " + DragItem.puntajeGlobal;
    }
}