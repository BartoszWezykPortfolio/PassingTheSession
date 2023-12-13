using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float damageSpeed = 1f;
    public Image healthBar;
    public Image damageBar;
    [Header("Damage Overlay")]
    public Image overlayD;
    public float durationD;
    public float fadeSpeedD;
    private float durationTimerD;
    [Header("Healing Overlay")]
    public Image overlayH;
    public float durationH;
    public float fadeSpeedH;
    private float durationTimerH;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlayD.color = new Color(overlayD.color.r, overlayD.color.g, overlayD.color.b, 0);
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreHealth(Random.Range(5, 10));
        }

        if (overlayD.color.a > 0)
        {
            durationTimerD += Time.deltaTime;
            if (durationTimerD > durationD)
            {
                //fade the image
                float tempAlpha = overlayD.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeedD;
                overlayD.color = new Color(overlayD.color.r, overlayD.color.g, overlayD.color.b, tempAlpha);
            }    
        }

        else if (overlayH.color.a > 0)
        {
            durationTimerH += Time.deltaTime;
            if (durationTimerH > durationH)
            {
                //fade the image
                float tempAlpha = overlayH.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeedH;
                overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillHealth = healthBar.fillAmount;
        float fillDamage = damageBar.fillAmount;
        float healthFraction = health / maxHealth;
        if (fillDamage > healthFraction)
        {
            healthBar.fillAmount = healthFraction;
            damageBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            lerpTimer /= 2;
            float percentComplete = lerpTimer / damageSpeed;
            damageBar.fillAmount = Mathf.Lerp(fillDamage, healthBar.fillAmount, percentComplete);
        }

        if (fillHealth < healthFraction)
        {
            damageBar.fillAmount = healthFraction;
            damageBar.color = Color.yellow;
            lerpTimer = Time.deltaTime;
            float percentComplete = lerpTimer / damageSpeed;
            healthBar.fillAmount = Mathf.Lerp(fillHealth, damageBar.fillAmount, percentComplete);
        }
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0f;
        durationTimerH = 0f;
        overlayH.color = new Color(overlayH.color.r, overlayH.color.g, overlayH.color.b, 1);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimerD = 0f;
        overlayD.color = new Color(overlayD.color.r, overlayD.color.g, overlayD.color.b, 1);
    }
}
