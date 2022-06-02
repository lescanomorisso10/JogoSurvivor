using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovTruck : MonoBehaviour
{
    [Header("Geral")]
    public Transform rayFont;
    public enum CheckType{
        TenPerSecond, TwPerSecond, AllTime
    };
    public CheckType checkType= CheckType.AllTime;
    [Range(1, 50)]
    public float visionDistance = 5;
    //
    [Header("RayCast")]
    public string obstaclesTag= "Obstacles";
    [Range(2,360)]
    public float extraRay=20;
    [Range(5,360)]
    public float visionAngle=120;
    [Range(1,10)]
    public int layerNumber=3;
    [Range(0.02f, 0.15f)]
    public float layerDistance=0.1f;
    //
    [Space (10)]
    public List<Transform> visibleObstacle = new List<Transform>();
    public List<Transform> temporaryCollision = new List<Transform>();
    LayerMask obstaclesLayer;
    float checkTime=0;
    Truck truck;
    public GameObject gObject;

    void Start()
    {
        checkTime=0;
        if(!rayFont)
            rayFont=transform;
        //
        truck=GameObject.FindObjectOfType<Truck>();
    }
    void Update()
    {
        if(checkType==CheckType.TenPerSecond){
            checkTime+=Time.deltaTime;
            if(checkTime>=0.1f){
                checkTime=0;
                CheckObstacles();
            }
        }
        if(checkType==CheckType.TwPerSecond){
            checkTime+=Time.deltaTime;
            if(checkTime>=0.05f){
                checkTime=0;
                CheckObstacles();
            }
        }
        if(checkType==CheckType.AllTime){
            CheckObstacles();
        }
    }
    private void CheckObstacles(){
        float layerLimit =  layerNumber * 0.5f;
        for(int x=0; x<=extraRay; x++){
            for(float y = -layerLimit + 0.5f; y<=layerLimit; y++){
                float angleRay= x*(visionAngle / layerNumber) + ((180f-visionAngle)*0.5f);
                Vector3 multipleDirection=(-transform.right) + (transform.up * y * layerDistance);
                Vector3 rayDirection=Quaternion.AngleAxis(angleRay, rayFont.up) * multipleDirection;
                //
                RaycastHit hit;
                if(Physics.Raycast(rayFont.position, rayDirection, out hit, visionDistance)){
                    if(!hit.transform.IsChildOf(transform.root) && !hit.collider.isTrigger){
                        if(hit.collider.gameObject.CompareTag(obstaclesTag)){
                            truck.distanceBack++;
                            
                            if(truck.distanceBack>=50){
                                visionDistance++;
                                if(visionDistance>=500){
                                    visionDistance=0;
                                }
                                truck.distanceBack=50;
                                
                                truck.GetBack();
                                if(!temporaryCollision.Contains(hit.transform)){
                                    temporaryCollision.Add(hit.transform);
                                }
                            }
                        }
                    }
                }else{ 
                    if(visionDistance==0){
                        Destroy(gObject);
                        truck.navMeshAgent.ResetPath( );
                        visionDistance=1;
                        //truck.GetFront();
                    }else{
                        
                        truck.GetFront();
                    }
                    
                }
                
            }
        }
    }  
}
