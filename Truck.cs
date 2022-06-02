using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    
    public float minDistance = 2;
    public Transform target;
    public Transform targetBack;
    public Transform obstacle;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public UnityEngine.AI.NavMeshPath path;
    private Rigidbody rig;
    FovTruck fov;
    public float distanceBack;
    PlantingArmor planting;
    void Start()
    {   
        path=GetComponent<UnityEngine.AI.NavMeshPath>();
        navMeshAgent=GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.stoppingDistance = minDistance;
        rig=GetComponent<Rigidbody>();
        fov=GameObject.FindObjectOfType<FovTruck>();
        planting=GameObject.FindObjectOfType<PlantingArmor>();
    }
    
    void Update()
    {
        
        planting.canRun=false; 
        if(planting.canRun==true){
            navMeshAgent.speed=3;
        }
    }
    public void GetBack(){  
        float distance = Vector3.Distance(this.transform.position, obstacle.transform.position);
        if(distance<distanceBack){
            Vector3 move= -transform.forward * Time.fixedDeltaTime ;
          //  Vector3 dirToPlayer= this.transform.position - targetBack.transform.position;
          //  Vector3 newPos=transform.position + dirToPlayer;
            navMeshAgent.Move(move);
        }else{
            GetFront();
        }
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("FinalPath")){
            Destroy(gameObject);
        }
        if(collider.gameObject.CompareTag("Obstacles")){
            navMeshAgent.radius+=0.2f;
        }
    }
    public void GetFront(){
        navMeshAgent.SetDestination(target.position);
    }
    public void SetTarget(){
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if( distance>=minDistance){
            GetFront();
        }
        else{
            GetBack();
        }
    }
}