using TMPro;
using UnityEngine;

public class BotellaManager : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje;
   

    [Header("Puntaje actual")]
    public int puntaje = 0;

    void Start()
    {
        ActualizarTexto();
    }

    public void ActualizarTexto()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntaje: " + puntaje;
    }

    // Método opcional para reiniciar el puntaje
    public void ReiniciarPuntaje()
    {
        puntaje = 0;
        ActualizarTexto();
    }
}
