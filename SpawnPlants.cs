using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    public bool canSpawn;
    public float coolDown=5f;
    public GameObject plants;
    public Transform spawnLocal;
    private Vector3 plantsSpawnLocal;
    Plants plant;
    void Start()
    {
        plant=GameObject.FindObjectOfType<Plants>();
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Grass")){
            canSpawn=false;
            coolDown=7;
        }else{
            canSpawn=true;
        }
    }
    void Update()
    {   
        plantsSpawnLocal= new Vector3(spawnLocal.transform.position.x,spawnLocal.transform.position.y, spawnLocal.transform.position.z );
        
        if(spawnLocal.transform.position==plants.transform.position){
            canSpawn=false;  
        }
        if(coolDown<=0){
            coolDown=0;
            coolDown+=8;
            canSpawn=true;
        }else
           canSpawn=false;
        if(canSpawn){
            Instantiate(plants, plantsSpawnLocal, spawnLocal.rotation);       
        }else   
            coolDown-=Time.deltaTime;
    }
}
