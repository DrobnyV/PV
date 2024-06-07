using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float basicMoveSpeed = 6f; // Základní rychlost pohybu hráče
    public float moveSpeed; // Aktuální rychlost pohybu hráče
    public float jumpSpeed = 8.0f; // Rychlost skoku hráče
    public float gravity = 20.0f; // Gravitace působící na hráče
    public float fastMoveSpeed = 10f; // Rychlost rychlého pohybu hráče
    private CharacterController controller; // Odkaz na komponentu CharacterController hráče
    private Vector3 moveDirection = Vector3.zero; // Směr pohybu hráče
    public float mouseSensitivity = 100.0f; // Citlivost myši pro pohyb kamery
    private float verticalRotation = 0.0f; // Vertikální rotace kamery
    public Slider hpBar; // Odkaz na komponentu Slider zobrazující zdraví hráče
    public Slider staminaBar; // Odkaz na komponentu Slider zobrazující vytrvalost hráče
    private bool running; // Indikátor běhu
    private Coroutine recharge; // Coroutine pro dobíjení vytrvalosti
    private bool canAttack = true; // Indikátor možnosti útoku
    public float maxStamina = 100; // Maximální hodnota vytrvalosti
    public float stamina; // Aktuální hodnota vytrvalosti
    public float health = 1; // Zdraví hráče
    public float maxHealth = 100f; // Maximální zdraví hráče
    public float defense = 0; // Obrana hráče
    public float regenRate = 1f; // Rychlost regenerace zdraví hráče
    public float regenCooldown = 5f; // Doba do obnovení regenerace zdraví
    public float timer; // Časovač pro regeneraci zdraví
    public float resourseAmount = 1; // Množství surovin
    public float lowHPBonus; // Bonus při nízkém zdraví
    public float lifeSteal = 0; // Lifesteal hráče
    public float HPOnKill = 0; // Zdraví získané po zabití
    public TMP_Text help; // Odkaz na textovou komponentu pro nápovědu
    public WandScript WandScript; // Odkaz na skript pro hůl
    public MecDamager MecDamager; // Odkaz na skript pro meč
    public WandScript bowScipt; // Odkaz na skript pro luk
    public AnimationController animace1; // Odkaz na skript pro animaci 1
    public AnimationController animace2; // Odkaz na skript pro animaci 2
    public Axe axe; // Odkaz na skript pro sekyru
    public Pickaxe pickaxe; // Odkaz na skript pro krumpáč
    public AnimationController animace3; // Odkaz na skript pro animaci 3
    public AnimationController animace4; // Odkaz na skript pro animaci 4

    /// <summary>
    /// Inicializuje skript.
    /// </summary>
    void Start()
    {
        resourseAmount = 1;
        stamina = maxStamina;
        moveSpeed = basicMoveSpeed;
        health = maxHealth;
        stamina = maxStamina;
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("Hráč musí mít komponentu CharacterController");
        }
    }

    /// <summary>
    /// Coroutine pro útok.
    /// </summary>
    private IEnumerator AttackCoroutine()
    {
        canAttack = false; // Zabraňuje dalším útokům, dokud coroutine neskončí
        stamina -= 10; // Sníží vytrvalost za útok
        yield return new WaitForSeconds(2f); // Čeká 2 sekundy
        canAttack = true; // Povolí další útok
    }

    /// <summary>
    /// Aktualizuje zdraví na liště.
    /// </summary>
    void updateHealth()
    {
        hpBar.value = health;
    }

    /// <summary>
    /// Přijímá poškození od útoku.
    /// </summary>
    /// <param name="damage">Množství poškození.</param>
    public void takeDamage(float damage)
    {
        health = health - damage;
    }

    /// <summary>
    /// Metoda pro změnu statistik.
    /// </summary>
    public void ZmenitStat()
    {
        if (int.Parse(help.text) == 0)
        {
            maxHealth = 150;
            health = maxHealth;
            maxStamina = 80;
            stamina = maxStamina;
            basicMoveSpeed = 5;
            fastMoveSpeed = 8;
        }

        if (int.Parse(help.text) == 1)
        {
            maxHealth = 80;
            health = maxHealth;
            maxStamina = 200;
            stamina = maxStamina;
            basicMoveSpeed = 4;
            fastMoveSpeed = 9;
        }
        if (int.Parse(help.text) == 2)
        {
            maxHealth = 100;
            health = maxHealth;
            maxStamina = 130;
            stamina = maxStamina;
            basicMoveSpeed = 7;
            fastMoveSpeed = 13;
        }
    }

    /// <summary>
    /// Aktualizuje stav hráče a hry.
    /// </summary>
    void Update()
    {
        staminaBar.maxValue = maxStamina;
        hpBar.maxValue = maxHealth;
        if (stamina < 10)
        {
            WandScript.enabled = false;
            MecDamager.enabled = false;
            bowScipt.enabled = false;
            axe.enabled = false;
            pickaxe.enabled = false;
            animace1.enabled = false;
            animace2.enabled = false;
            animace3.enabled = false;
            animace4.enabled = false;
        }
        else
        {
            WandScript.enabled = true;
            MecDamager.enabled = true;
            bowScipt.enabled = true;
            axe.enabled = true;
            pickaxe.enabled = true;
            animace1.enabled = true;
            animace2.enabled = true;
            animace3.enabled = true;
            animace4.enabled = true;
        }
        if (health <= 0.30 * maxHealth)
        {
            stamina *= lowHPBonus;
            regenRate *= lowHPBonus;
            basicMoveSpeed *= lowHPBonus;
            fastMoveSpeed *= lowHPBonus;
            lifeSteal *= lowHPBonus;
        }
        hpBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackCoroutine());
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            if (Input.GetButton("Jump") && stamina > 10)
            {
                stamina -= 10;
                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
                moveDirection.y = jumpSpeed;
            }
            updateHealth();
            die();
            if (health < maxHealth)
            {
                timer += Time.deltaTime;

                if (timer >= regenCooldown)
                {
                    RegenerateHP();
                    timer = 0f;
                }
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90.0f, 90.0f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        staminaBar.value = stamina;
        runin();
    }

    /// <summary>
    /// Coroutine pro dobíjení vytrvalosti hráče.
    /// </summary>
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (stamina < maxStamina)
        {
            stamina += 5f;
            if (stamina > maxStamina) stamina = maxStamina;
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// Regeneruje zdraví hráče.
    /// </summary>
    void RegenerateHP()
    {
        health += regenRate;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// Metoda pro běh hráče.
    /// </summary>
    void runin()
    {
        if (stamina < 0) stamina = 0;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
        }

        if (running == true && (moveDirection.z != 0 || moveDirection.x != 0) && stamina > 0)
        {
            moveSpeed = fastMoveSpeed;
            stamina -= 10 * Time.deltaTime;
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            moveSpeed = basicMoveSpeed;
        }
    }

    /// <summary>
    /// Metoda pro smrt hráče.
    /// </summary>
    public void die()
    {
        if (this.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
