using UnityEngine;

public class ContenedorBotella : MonoBehaviour
{
    public bool aceptaBuenas;
    public TemporizadorPastel temporizador;
    public BotellaManager botellaManager;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Botella")) return;

        Botella botella = other.GetComponent<Botella>();
        if (botella == null) return;

        bool esBuena = botella.esBuena;
        Debug.Log($"Contenedor aceptaBuenas: {aceptaBuenas}, Botella esBuena: {esBuena}");

        if (aceptaBuenas && esBuena)
        {
            Debug.Log("Botella buena en contenedor de buenas: suma tiempo y puntaje");
            if (temporizador != null)
                temporizador.AñadirTiempo(3f);
            if (botellaManager != null)
                botellaManager.puntaje += 10;
        }
        else if (!aceptaBuenas && !esBuena)
        {
            Debug.Log("Botella mala en contenedor de malas: suma tiempo y puntaje");
            if (temporizador != null)
                temporizador.AñadirTiempo(3f);
            if (botellaManager != null)
                botellaManager.puntaje += 10;
        }
        else
        {
            Debug.Log("Botella incorrecta: quita tiempo");
            if (temporizador != null)
                temporizador.QuitarTiempo(5f);
        }

        if (botellaManager != null)
            botellaManager.ActualizarTexto();

        Destroy(other.gameObject);
    }

}
