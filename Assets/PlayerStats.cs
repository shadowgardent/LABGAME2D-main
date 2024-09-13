using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : MonoBehaviour
{
    public float maxHealth;
    public float health ;
    private bool canTakeDamage = true;
    public bool playerIsAlive = true;
    public Logic logic; 
    private Animator anim;
    
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        health = maxHealth;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();

        if (logic == null)
        {
            Debug.LogError("Logic component not found!");
        }
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        health -= damage;
        anim.SetBool("Damage", true);

        if (health <= 0)
        {
            
            GetComponentInParent<GatherInput>().DisableControls();
            GetComponent<PolygonCollider2D>().enabled = false;

            // Call gameOver method
            if (logic != null)
            {
                logic.gameOver();
            }
            else
            {
                Debug.LogError("Logic component is not assigned");
            }
        }
        
        StartCoroutine(DamagePrevention());
    }

    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        
        if (health > 0)
        {
            canTakeDamage = true;
            anim.SetBool("Damage", false);
        }
        else
        {
            anim.SetBool("Death", true);
        }
    }
}
