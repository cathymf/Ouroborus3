using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    // Messy but functional attack hitbox points for each direciton
    public GameObject slashObjFront;
    public GameObject slashObjBack;
    public GameObject slashObjLeft;
    public GameObject slashObjRight;

    // health, damage, and important bools
    public float health = 100;
    public float damage = 50;
    bool slashing = false, hurting = false;

    // Animator for setting parameters for animation changes
    // Movement for Facing() direction
    public Animator animator;
    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slash();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Hurt();
        }
    }
    /// <summary>
    /// Method to handle slashing animation
    /// </summary>
    void Slash()
    {
        if(slashing == false)
        {
            animator.SetBool("Attacking", true);
            StartCoroutine(Slashing());
            slashing = true;
        }
    }

    void Hurt()
    {
        if(hurting == false)
        {
            animator.SetBool("Hit", true);
            StartCoroutine(Hurting());
            hurting = false;
        }
    }

    /// <summary>
    /// Coroutine to handle waiting before ending slashing animation
    /// Also handles spawning and checking of hitboxes
    /// </summary>
    /// <returns>Seconds to wait befor returning to coroutine</returns>
    private IEnumerator Slashing()
    {
        int temp = movement.Facing();
        GameObject objTemp = null;

        if (temp == 0)
            objTemp = slashObjFront;
        if (temp == 1)
            objTemp = slashObjLeft;
        if (temp == 2)
            objTemp = slashObjRight;
        if (temp == 3)
            objTemp = slashObjBack;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(objTemp.transform.position, 1);

        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("enemy"))
            {
                enemy.GetComponent<Wolf>().Hurt(damage);
            }
        }

        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Attacking", false);
        slashing = false;
    }

    /// <summary>
    /// Method to handle ending hurting animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator Hurting()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hit", false);
        hurting = false;
    }

    /// <summary>
    /// Debug method to draw hitbox sizes and locations
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(slashObjFront.transform.position, 1);
        Gizmos.DrawWireSphere(slashObjLeft.transform.position, 1);
        Gizmos.DrawWireSphere(slashObjRight.transform.position, 1);
        Gizmos.DrawWireSphere(slashObjBack.transform.position, 1);
    }
}
