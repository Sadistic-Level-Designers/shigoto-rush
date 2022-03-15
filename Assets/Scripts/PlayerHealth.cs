using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float regeneration;
    public Volume screenVolume;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // this is a test
        if (Input.GetKeyDown(KeyCode.Space)) 
            PlayerTakeDamage();

        // autohealing
        if (currentHealth < maxHealth)
            currentHealth += regeneration * Time.deltaTime;

        // clamp health between 0 and 1
        currentHealth = Mathf.Clamp01(currentHealth);

        // set screen effect
        if(screenVolume)
            screenVolume.weight = Mathf.Pow(1f - currentHealth, 2);

        if(currentHealth <= 0)
        {  
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PlayerTakeDamage()
    {
        currentHealth = currentHealth - damage;
    }

}

