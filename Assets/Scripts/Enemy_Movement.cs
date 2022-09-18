using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Enemy_Movement : MonoBehaviour
{
    public Transform Player;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;   

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);

        while (enabled)
        {

            Agent.SetDestination(Player.transform.position);
            yield return Wait;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Saha kenarlar�na �arp�nca gameobject yok ediliyor.
        Agent.enabled = false;
        
        //Suya d��me efekti g�r�ns�n diye gecikme veriyoruz.
        Invoke("Kill", 2f);
    }

   void Kill()
    {
        
        gameObject.SetActive(false);
        
        
    }
}
