using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar : MonoBehaviour
{
    public TMP_Text healThText;
    public Image healthBar;

    public float health = 100, maxHealth = 100;
    float lerpSpeed;

    //private void Start()
    //{
    //    ColorChanger();
    //}

    private void Update()
    {
        if (health > maxHealth) 
            health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    //void ColorChanger()
    //{
    //    Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
    //    healthBar.color = healthColor;
    //}

    public void setHealth(float amount)
    {
        health = amount;
        if (health < 0)
            health = 0;

        if (health > 100)
            health = 100;
        healThText.text = health + "%";
        //ColorChanger();
    }
}
