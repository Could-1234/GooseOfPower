using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class MyEnemyMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private MyEnemyHealth myEnemyHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        myEnemyHealth = GetComponent<MyEnemyHealth>();

    }   
    // Update is called once per frame
    void Update()
    {
        if (!myEnemyHealth.IsDeath)
         nav.SetDestination(player.position);
    }
}
