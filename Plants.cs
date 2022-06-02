using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{   
    private float lifeTime=6f;
    public Rigidbody rig;
    public Transform spawner;
    private Vector3 spawnerFallow;
    SpawnPlants spawnS;
    private bool graped;
    PlantingArmor planting;
    void Start()
    {
        rig=GetComponent <Rigidbody>();
        spawnS=GameObject.FindObjectOfType<SpawnPlants>();
        planting=GameObject.FindObjectOfType<PlantingArmor>();
    }
    void Update()
    {   
        Graped(); 
        if(transform.position.x==spawnerFallow.x && transform.position.y == spawnerFallow.y && transform.position.z == spawnerFallow.z){
            spawnS.canSpawn=false;
        }
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("ReloadLocal")){
            IfGrapedDestroy();
            planting.plantShoot+=1;  
        }else{
            Graped();
        }
    }
    private void OnTriggerStay ( Collider col){
        if(col.gameObject.CompareTag("Wall")){
            rig.AddForce(Vector3.left * 2);
        }
        if(col.gameObject.CompareTag("Ground")){
            Destroy(gameObject, lifeTime);  
            IfGrapedDestroy() ;
        }
    }
    public void GrapResultTrue(){
        graped=true;
    }
    public void GrapResultFalse(){
        graped=false;
    }
    public void IfGrapedDestroy(){
        Destroy(GetComponent<MeshRenderer>()); 
        Destroy(GetComponent<MeshFilter>());
        lifeTime=1000;
        Destroy(gameObject, lifeTime);   
    }
    public void Graped(){
        if(graped){
            spawnS.canSpawn=false;
            IfGrapedDestroy();
        }else{
            lifeTime=50;
            spawnerFallow= new Vector3(spawner.position.x, spawner.position.y, spawner.position.z);
            transform.Translate(Vector2.down*0); 
            Destroy(gameObject, lifeTime); 
        }
    }
    

    
}
