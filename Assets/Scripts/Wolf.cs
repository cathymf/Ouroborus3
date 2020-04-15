using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{

    // default health, currentHealth, damage amount
    // and other important variables
    public float health = 100;
    private float currentHealth;
    public float damage = 50;

    public Animator animator;

    bool hurting = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            this.enabled = false;
        }
    }
    /// <summary>
    /// Method to handle collisions with player slash attack
    /// </summary>
    /// <param name="collision">collision2D data containing colliding gameObject information</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("WolfHit");
        if (collision.gameObject.CompareTag("slashHitBox"))
        {
            Hurt(collision.gameObject.GetComponent<Combat>().damage);
        }
    }

    /// <summary>
    /// Method to damage the wolf when hit
    /// </summary>
    /// <param name="damage">damage to lower health by</param>
    public void Hurt(float damage)
    {
        currentHealth -= damage;
        if (hurting == false)
        {
            animator.SetBool("Hurting", true);
            StartCoroutine(Hurting());
            hurting = false;
        }
    }
    /// <summary>
    /// Coroutine to handle waiting before disabling hurt animation
    /// </summary>
    /// <returns>seconds to wait before returning to coroutine</returns>
    private IEnumerator Hurting()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hurting", false);
    }
}
