using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    private int i;
    public List<Transform> targets;
    public GameObject player;
    public GameObject borEnemy;
    public float distance;
    public EnemyScript setHP;
    
    private KillBoarQuest killBoarQuest;

    public float distanceThreshold = 4f;
    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        setHP = GetComponent<EnemyScript>();
        
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
    }

    void TargetUpdate()
    {
        i = Random.Range(0, targets.Count);
    }
    void Update()
    {
        if (setHP.HP > 0f)
        {
            distance = Vector3.Distance(borEnemy.transform.position, player.transform.position);

            if (distance <= distanceThreshold)
            {
                agent.SetDestination(player.transform.position);
            }
            else 
            {
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
                Die();
                
            } 
        }
    }
    void Die()
    {
        // Викликаємо метод BoarKilled() у класі квесту
        killBoarQuest?.BoarKilled();
        Destroy(gameObject);
    }
}
