using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Třída ProjectileScript řídí chování projektilu v hře, včetně pohybu a poškození při kolizi s objekty.
/// </summary>
public class ProjectileScript : MonoBehaviour
{
    /// <summary>
    /// Rychlost pohybu projektilu.
    /// </summary>
    public float speed = 20f;

    /// <summary>
    /// Poškození způsobené projektilu.
    /// </summary>
    public float damage = 50f;

    /// <summary>
    /// Doba života projektilu.
    /// </summary>
    public float lifetime = 5f;

    /// <summary>
    /// Reference na objekt zobrazující pomocné poškození.
    /// </summary>
    public GameObject helpDMG;

    /// <summary>
    /// Inicializační metoda. Nastaví dobu života projektilu a získá referenci na objekt zobrazující pomocné poškození.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, lifetime);
        helpDMG = GameObject.Find("MainH");
    }

    /// <summary>
    /// Metoda pro aktualizaci, která se volá jednou za frame. Provádí pohyb projektilu.
    /// </summary>
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    /// <summary>
    /// Metoda volaná při kolizi projektilu s jiným objektem. Provádí poškození cílovému objektu a zničení projektilu.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (int.TryParse(helpDMG.GetComponent<TMP_Text>().text, out int helpDamageMultiplier))
            {
                float calculatedDamage = damage * helpDamageMultiplier;

                if (collision.collider.GetComponent<EnemyHP>() != null)
                {
                    collision.collider.GetComponent<EnemyHP>().takeDamage(calculatedDamage);
                    Debug.Log("Hit Enemy, damage: " + calculatedDamage);
                }
                else if (collision.collider.GetComponent<EnemyHPAIR>() != null)
                {
                    collision.collider.GetComponent<EnemyHPAIR>().takeDamage(calculatedDamage);
                    Debug.Log("Hit Enemy, damage: " + calculatedDamage);
                }
                else if (collision.collider.GetComponent<EnemyBossHP>() != null)
                {
                    collision.collider.GetComponent<EnemyBossHP>().takeDamage(calculatedDamage);
                    Debug.Log("Hit Enemy, damage: " + calculatedDamage);
                }
                else if (collision.collider.GetComponent<RockScript>() != null)
                {
                    collision.collider.GetComponent<RockScript>().takeDamage(5);
                    Debug.Log("Hit Rock");
                }
                else if (collision.collider.GetComponent<IronScript>() != null)
                {
                    collision.collider.GetComponent<IronScript>().takeDamage(5);
                    Debug.Log("Hit Iron");
                }
                else if (collision.collider.GetComponent<DiamondScript>() != null)
                {
                    collision.collider.GetComponent<DiamondScript>().takeDamage(5);
                    Debug.Log("Hit Diamond");
                }
                else if (collision.collider.GetComponent<TreeScript>() != null)
                {
                    collision.collider.GetComponent<TreeScript>().takeDamage(5);
                    Debug.Log("Hit Tree");
                }
                else
                {
                    Debug.Log("Hit something else");
                }
            }
            else
            {
                Debug.LogError("Failed to parse helpDMG text to int.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error on collision: " + e);
        }

        Destroy(gameObject);
    }
}
