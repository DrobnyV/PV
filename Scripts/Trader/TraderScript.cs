using UnityEngine;
using UnityEngine.UI;

public class TraderScript : MonoBehaviour
{
    public GameObject textToOpen; // Reference na textový objekt pro otevření obchodu
    private float raycastLength = 2f; // Délka paprsku pro raycast
    private bool canBeOpened = false; // Zda může být obchod otevřen
    public GameObject shop; // Reference na herní objekt obchodu
    private bool shopOpened = false; // Zda je obchod otevřen
    public GameObject player; // Reference na herní objekt hráče
    public GameObject mec; // Reference na herní objekt meče
    public GameObject textToClose; // Reference na textový objekt pro zavření obchodu
    public Button exitButton; // Tlačítko pro zavření obchodu
    public GameObject betterS; // Reference na herní objekt lepšího setu
    public WandScript w1; // Reference na skript první hůlky
    public WandScript w2; // Reference na skript druhé hůlky

    /// <summary>
    /// Metoda pro otevření obchodu.
    /// </summary>
    void Opentrader()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            // Zkontroluje, zda objekt zasažený paprskem má tag "Player"
            if (hit.collider.CompareTag("Player") && shopOpened == false)
            {
                // Pokud je objekt označen jako "Player", zobraz detekovaný objekt
                if (textToOpen != null)
                {
                    textToOpen.SetActive(true);
                    canBeOpened = true;
                }
            }
        }
        else
        {
            // Pokud není detekován žádný hráč, skryj detekovaný objekt
            if (textToOpen != null)
            {
                textToOpen.SetActive(false);
                canBeOpened = false;
            }
        }

        if (canBeOpened == true && Input.GetKeyDown(KeyCode.E))
        {
            shop.SetActive(true);
            shopOpened = true;
            player.GetComponent<PlayerController>().enabled = false;
            mec.GetComponent<AnimationController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            textToOpen.SetActive(false);
            textToClose.SetActive(true);
            betterS.SetActive(false);
        }
    }

    /// <summary>
    /// Metoda pro zavření obchodu.
    /// </summary>
    void CloseShop()
    {
        if (shopOpened == true && Input.GetKeyDown(KeyCode.Escape))
        {
            shop.SetActive(false);
            shopOpened = false;
            player.GetComponent<PlayerController>().enabled = true;
            mec.GetComponent<AnimationController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            textToOpen.SetActive(true);
            textToClose.SetActive(false);
            w1.enabled = true;
            w2.enabled = true;
        }
    }

    /// <summary>
    /// Metoda pro zavření obchodu pomocí tlačítka.
    /// </summary>
    public void CloseShopButton()
    {
        if (shopOpened == true)
        {
            shop.SetActive(false);
            shopOpened = false;
            player.GetComponent<PlayerController>().enabled = true;
            mec.GetComponent<AnimationController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            textToOpen.SetActive(true);
            textToClose.SetActive(false);
            w1.enabled = true;
            w2.enabled = true;
        }
    }

    /// <summary>
    /// Metoda pro inicializaci.
    /// </summary>
    void Start()
    {
        textToOpen.SetActive(false);
        shop.SetActive(false);
    }

    /// <summary>
    /// Metoda Update se volá jednou za snímek.
    /// </summary>
    void Update()
    {
        Opentrader();
        CloseShop();
    }
}
