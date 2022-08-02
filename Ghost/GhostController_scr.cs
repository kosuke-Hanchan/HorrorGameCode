using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GhostController_scr : MonoBehaviour
{

    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject[] wayPoints;
    private int waipointIndex;

    private NavMeshAgent navMeshAgent;
    public float m_fSightAngle = 60.0f;
    private bool playerFind = true;
    private Vector3 playerPos;
    [SerializeField] LayerMask mask;
    [SerializeField] float timeLimit;
    private float beginTime;

    private GameObject flashLight;

    void Start()
    {
        flashLight = GameObject.FindGameObjectWithTag("FlashLight.fg");
        PlayerObject = GameObject.FindWithTag("Player"); 
        wayPoints = GameObject.FindGameObjectsWithTag("GhostWayPoiints"); 
        navMeshAgent = GetComponent<NavMeshAgent>();
        // 最初の目的地
        waipointIndex = Random.Range(1, wayPoints.Length);
        navMeshAgent.SetDestination(wayPoints[waipointIndex].transform.position);


        beginTime = timeLimit;
    }

    void Update()
    {
        if(flashLight.GetComponent<FlashLight_scr>().fg && playerFind){
            playerPos = PlayerObject.transform.position;
            navMeshAgent.SetDestination(playerPos);
        }

        else if(playerFind){
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                waipointIndex = Random.Range(1, wayPoints.Length);
                navMeshAgent.SetDestination(wayPoints[waipointIndex].transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
	{
        if(other.GetComponent<Collider>().tag == "Player"){
            Vector3 posDelta = PlayerObject.transform.position - transform.position;
            float targetAngle = Vector3.Angle(transform.forward, posDelta);

            if( targetAngle < m_fSightAngle)
            {
                if( Physics.Raycast(transform.position,new Vector3(posDelta.x,0f,posDelta.z),out RaycastHit hit,Mathf.Infinity,mask))
                {
                    if( hit.collider.tag == "Player")
                    {
                        playerFind = false;
                        playerPos = PlayerObject.transform.position;
                        navMeshAgent.SetDestination(playerPos);
                        beginTime = timeLimit;
                    }
                }
            }
            else{
                if(!playerFind){
                    beginTime -= Time.deltaTime;
                    playerPos = PlayerObject.transform.position;
                    navMeshAgent.SetDestination(playerPos);
                    if(beginTime >= 0){
                        playerFind = true;
                    }
                }
            }
        }
	}

    private void OnTriggerExit(Collider Player)
	{
        playerFind = true;
    }


    

}