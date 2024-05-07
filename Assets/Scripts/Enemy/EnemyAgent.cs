using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class EnemyAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    private int i = 0;
    public List<Transform> targets;
    public GameObject player;
    public GameObject borEnemy;
    public float distance;
    public EnemyScript setHP;
    public HP_Playser _HP_Playser;

    private KillBoarQuest killBoarQuest;
    
    private int damagEnemy = 10;
    private float distanceBorPint = 1f;
    public float knockbackForce = 5f;
    public float distanceThreshold = 4f;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        setHP = GetComponent<EnemyScript>();
        _HP_Playser = FindObjectOfType<HP_Playser>();

        SetKillBoarQuestReference(FindObjectOfType<KillBoarQuest>());
    }
    
    public void SetKillBoarQuestReference(KillBoarQuest quest)
    {
        killBoarQuest = quest;
    }
    IEnumerator DelayedMethod(float delay)
    {
        yield return new WaitForSeconds(delay);
        TargetUpdate();
        agent.SetDestination(targets[i].position);
        setHP.animator.SetBool("isPatrolling",true);
    }

    void TargetUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            float currentDistanceToTarget = Vector3.Distance(transform.position, targets[i].position);
            if (currentDistanceToTarget < distanceThreshold)
            {
                // Призначаємо наступну точку
                i = Random.Range(0, targets.Count);
                //agent.SetDestination(targets[i].position);
            }
        }
        
    }
   
    
    void Update()
    {
        if (setHP.HP > 0f)
        {
            distance = Vector3.Distance(borEnemy.transform.position, player.transform.position);

            if (distance <= distanceThreshold)
            {
                setHP.animator.SetBool("isPatrolling",false);
                setHP.animator.SetBool("isChasebor",true);
                agent.SetDestination(player.transform.position);
                if (distance <= 1.5f)
                {
                    setHP.animator.SetBool("isAttack",true);
                    if (setHP.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !setHP.animator.IsInTransition(0))
                    {
                        Vector3 knockbackDirection = (player.transform.position - transform.position ).normalized * Time.deltaTime;
                        player.GetComponent<Rigidbody>()
                            .AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                        //добавити сюди визов функції нанесення урону гравцю
                        _HP_Playser.healthBar.value = _HP_Playser.HPP;
                        _HP_Playser.HPP -= damagEnemy;
                        if (_HP_Playser.HPP <= 0)
                        {
                            ReloadScene();
                        }
                        
                    }
                }
                else
                {
                    setHP.animator.SetBool("isAttack",false);
                }
                
            }
            else 
            {
                setHP.animator.SetBool("isChasebor",false);
                
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    
                    //TargetUpdate();
                    StartCoroutine(DelayedMethod(5f));
                    //agent.SetDestination(targets[i].position);
                }
            }
        }
        else
        {
            if (setHP.HP <= 0f)
            {  
                setHP.animator.SetBool("isPatrolling",false);
                setHP.animator.SetBool("isAttack",false);
                setHP.animator.SetBool("isChasebor",false);
                setHP.animator.SetTrigger("isDeid");
                if (setHP.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
                    !setHP.animator.IsInTransition(0))
                {
                    Die();
                }

            } 
        }
    }
    void Die()
    {
        // Викликаємо метод BoarKilled() у класі квесту
        killBoarQuest?.BoarKilled();
        Destroy(gameObject);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
