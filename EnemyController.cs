using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform TargetToSeek;
    public Animator anim;
    bool attacking;
    public float hitpoints;
    
    public float damageW = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (anim == null)
            anim = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if(TargetToSeek == null) {
            return;
        }

        agent.SetDestination(TargetToSeek.position);
    }

    public void Detection() {
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        this.GetComponent<RandomNavMeshWalk>().enabled = false;
        TargetToSeek = target;
    }
    
   /* public void LostDetection() {
        TargetToSeek = null;
        this.GetComponent<RandomNavMeshWalk>().enabled = true;
    }*/
   
    public void DoDamage(GameObject enemy)
    {
        if (attacking == false)
        {
            StartCoroutine(Damage(enemy));
        }

    }

    IEnumerator Damage(GameObject m_enemy)
    {
        attacking = true;
        yield return new WaitForSeconds(0.4f);
        m_enemy.GetComponent<Attributes>().Health -= damageW; 
        attacking = false;

        Debug.Log("F");


    }
    public void Damage(float dmg)  //Used in a SendMessage by weapons to stun the enemies or knock them back
    {
        hitpoints -= dmg;
        if (hitpoints <= 0)
        {
            anim.SetBool("Death", true);
            Death();

            GameManager.Instance.Enemy.Remove(this.gameObject);
        }

    }

    public void Death()
    {
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;


    }

}
