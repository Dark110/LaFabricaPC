using UnityEngine;

public class InteractuarConObjeto : MonoBehaviour
{
    public Camera camara;
    public Transform holder;
    public float distanciaRaycast = 3f;
    public float velocidadLerp = 5f;
    public KeyCode teclaInteractuar = KeyCode.E;
    public KeyCode teclaSoltar = KeyCode.Q;

    private GameObject objetoActual;
    private bool objetoAgarrado = false;
    private Rigidbody objetoRigidbody;
    private MoverEnX moverEnXScript;

    void Update()
    {
        RaycastHit hit;
        Ray ray = camara.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, distanciaRaycast, LayerMask.GetMask("Interactuable")))
        {
            if (Input.GetKeyDown(teclaInteractuar) && !objetoAgarrado)
            {
                AgarrarObjeto(hit.collider.gameObject);
            }
        }

        if (objetoAgarrado && objetoActual != null)
        {
            MoverObjetoConLerp();
        }

        if (Input.GetKeyDown(teclaSoltar) && objetoAgarrado)
        {
            SoltarObjeto();
        }

        // ✅ Verificar si el objeto fue destruido mientras lo teníamos agarrado
        if (objetoAgarrado && objetoActual == null)
        {
            objetoAgarrado = false;
            objetoRigidbody = null;
        }
    }

    void AgarrarObjeto(GameObject objeto)
    {
        objetoActual = objeto;
        objetoAgarrado = true;

        objeto.transform.SetParent(holder);
        objetoRigidbody = objeto.GetComponent<Rigidbody>();
        moverEnXScript = objeto.GetComponent<MoverEnX>();

        if (moverEnXScript != null)
        {
            moverEnXScript.enabled = false;
        }

        if (objetoRigidbody != null)
        {
            objetoRigidbody.useGravity = false;
            objetoRigidbody.isKinematic = false;
        }
    }

    void MoverObjetoConLerp()
    {
        if (objetoActual == null) return;

        objetoActual.transform.position = Vector3.Lerp(
            objetoActual.transform.position,
            holder.position,
            velocidadLerp * Time.deltaTime
        );
    }

    public void ForzarSoltarSiEs(GameObject objeto)
    {
        if (objetoAgarrado && objetoActual == objeto)
        {
            objetoActual = null;
            objetoAgarrado = false;
            objetoRigidbody = null;
        }
    }

    void SoltarObjeto()
    {
        if (objetoActual != null)
        {
            objetoActual.transform.SetParent(null);

            if (objetoRigidbody != null)
            {
                objetoRigidbody.useGravity = true;
                objetoRigidbody.isKinematic = false;
                objetoRigidbody.velocity = Vector3.zero;
                objetoRigidbody.angularVelocity = Vector3.zero;
            }
        }

        objetoActual = null;
        objetoAgarrado = false;
        objetoRigidbody = null;
    }

    void OnDrawGizmos()
    {
        if (camara == null) return;

        Ray ray = camara.ScreenPointToRay(Input.mousePosition);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * distanciaRaycast);
    }
}
