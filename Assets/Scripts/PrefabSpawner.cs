using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Asegúrate de incluir esto

public class PrefabSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PrefabConProbabilidad
    {
        public GameObject prefab;
        [Range(0f, 100f)] public float probabilidad;
    }

    [Header("Prefabs y Probabilidades")]
    public List<PrefabConProbabilidad> prefabsConProbabilidades = new List<PrefabConProbabilidad>();

    [Header("Configuración de Spawneo")]
    public int cantidadInicialASpawnear = 10;
    public int incrementoPorNivel = 3;
    public Transform puntoDeSpawneo;
    public float intervaloSpawn = 3f;
    public float intervaloMinimo = 0.5f;
    public float reduccionPorNivel = 0.3f;

    [Header("UI")]
    public TextMeshProUGUI textoRonda; // Arrastra aquí tu TextMeshProUGUI desde el inspector

    private int nivel = 1;

    void Start()
    {
        ActualizarTextoRonda();
        if (ValidarProbabilidades())
        {
            StartCoroutine(RondasDeSpawneo());
        }
        else
        {
            Debug.LogError("Las probabilidades no suman 100%. Corrige eso en el inspector.");
        }
    }

    void ActualizarTextoRonda()
    {
        if (textoRonda != null)
            textoRonda.text = $"Ronda: {nivel}";
    }

    bool ValidarProbabilidades()
    {
        float total = 0f;
        foreach (var item in prefabsConProbabilidades)
        {
            total += item.probabilidad;
        }
        return Mathf.Approximately(total, 100f);
    }

    IEnumerator RondasDeSpawneo()
    {
        while (true)
        {
            int cantidadASpawnear = cantidadInicialASpawnear + (nivel - 1) * incrementoPorNivel;
            int cantidadSpawneada = 0;
            ActualizarTextoRonda();

            while (cantidadSpawneada < cantidadASpawnear)
            {
                SpawnearUnPrefab();
                cantidadSpawneada++;

                if (Random.value < 0.3f)
                {
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
                    SpawnearUnPrefab();
                    cantidadSpawneada++;
                }

                yield return new WaitForSeconds(intervaloSpawn);
            }

            yield return new WaitForSeconds(5f);

            intervaloSpawn = Mathf.Max(intervaloMinimo, intervaloSpawn - reduccionPorNivel);
            nivel++;
        }
    }

    void SpawnearUnPrefab()
    {
        GameObject prefabElegido = ElegirPrefabPorProbabilidad();
        Instantiate(prefabElegido, puntoDeSpawneo.position, Quaternion.identity);
    }

    GameObject ElegirPrefabPorProbabilidad()
    {
        float random = Random.Range(0f, 100f);
        float acumulado = 0f;

        foreach (var item in prefabsConProbabilidades)
        {
            acumulado += item.probabilidad;
            if (random <= acumulado)
            {
                return item.prefab;
            }
        }

        return prefabsConProbabilidades[0].prefab;
    }
}
