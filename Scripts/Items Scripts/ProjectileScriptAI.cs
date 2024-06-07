using UnityEngine;

/// <summary>
/// Třída ProjectileScriptAI řídí chování projektilu v hře nepřátelských jednotek, včetně pohybu a poškození při kolizi s hráčem.
/// </summary>
public class ProjectileScriptAI : MonoBehaviour
{
    /// <summary>
    /// Rychlost pohybu projektilu.
    /// </summary>
    public float speed = 20f;

    /// <summary>
    /// Poškození způsobené projektilu.
    /// </summary>
    public float damage = 10f;

    /// <summary>
    /// Doba života projektilu.
    /// </summary>
    public float lifetime = 5f;

    /// <summary>
    /// Inicializační metoda. Nastaví dobu života projektilu.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Metoda pro aktualizaci, která se volá jednou za frame. Provádí pohyb projektilu.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// Metoda volaná při kolizi projektilu s jiným objektem. Provádí poškození hráči a zničení projektilu.
    /// </summary>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            collider.GetComponent<PlayerController>().takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
