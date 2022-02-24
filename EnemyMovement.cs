using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent = null;
    public Animator animator;
    public float minDistance = 5f;
    public float maxDistance = 25f;
    private bool isMoving;

    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
         animator = GetComponent<Animator>();
        animator.SetBool("isAttacking", false);

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
            animator.SetBool("isAttacking", false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled) return;


        if (!isMoving)
        {
            findPlayer();
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (animator) animator.SetFloat("Speed", 1f);
            isMoving = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
        }
        else
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        if (animator.GetBool("Death") == true)
        {
            agent.enabled = false;
        }

        
    }

    void FixedUpdate()
    {
      // LayerMask layerMask = LayerMask.GetMask("Wall");

        
    }

    void findPlayer()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPos = target.position;
        agent.SetDestination(playerPos);
    }

    private void OnCollisionStay(Collision collision)
    {



        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("...");
            //agent.enabled = false;
            GameObject Wall = collision.gameObject;
            Vector3 WallV = Wall.transform.position;
           //agent.SetDestination(WallV);
            ToggleAttributes(false);
           /*int health = collision.gameObject.GetComponent<Attributes>().Health;
            
            isMoving = false;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);*/

            GetComponent<EnemyController>().DoDamage(Wall);

            if (Wall.GetComponent<Attributes>().Health <= 0)
            {
                Wall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                Wall.GetComponent<Rigidbody>().AddForce(1f, 0, 0, ForceMode.VelocityChange);
                ToggleAttributes(true);
            }
            
            /*if (health == 0)
            {
                Destroy(collision.gameObject);
            }*/
            
        }

    }


    public void ToggleAttributes(bool state)
    {
        agent.enabled = state;
        // Destroy(Wall);
        isMoving = state;
        animator.SetBool("isMoving", state);
        animator.SetBool("isAttacking", !state);
        if (agent.enabled == true)
        {
            findPlayer();
        }
        Debug.Log("U");
    }

}
