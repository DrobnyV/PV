using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim; // Reference na komponentu Animator pro ovládání animací.
    private bool canClick = true; // Příznak indikující, zda je možné provádět kliknutí.

    void Start()
    {
        anim = GetComponent<Animator>(); // Inicializace proměnné anim odkazem na komponentu Animator tohoto objektu.
    }
    
    // Coroutine pro čekání na možnost dalšího kliknutí.
    private IEnumerator WaitS()
    {
        canClick = false; // Nastaví příznak, že kliknutí není povoleno.
        yield return new WaitForSeconds(2f); // Počkej 2 sekundy.
        canClick = true; // Nastaví příznak, že kliknutí je opět povoleno.
    }

    void Update()
    {
        // Pokud je stisknuto levé tlačítko myši a je povoleno kliknutí.
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            anim.SetTrigger("Active"); // Spustí animaci s názvem "Active".
            StartCoroutine(WaitS()); // Spustí coroutine pro čekání.
        }
    }
}
