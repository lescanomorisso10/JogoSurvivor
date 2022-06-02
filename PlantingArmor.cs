using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingArmor : MonoBehaviour
{
    public GameObject plants;
    public Transform barrel;
    public Transform waterLocal;
    private float coolDown=5f;
    private bool canShoot;
    Plants plant;
    public int plantShoot=0;
    public GameObject plantLocal;
    public GameObject activeBag;
    Truck truck;
    public ParticleSystem water;
    public bool canRun;
    void Start()
    {
        plant=GameObject.FindObjectOfType<Plants>();
        truck=GameObject.FindObjectOfType<Truck>();
        canRun=false;
    }
    public void Shoot(){
        if(canShoot){
            GameObject SpawnPlant= Instantiate(plants, barrel.position, barrel.rotation);
            plantShoot-=1;  
            
            plantLocal.SetActive(false);
        }
    }
    public bool CanShoot(){
        if(coolDown<=0){
            if(plantShoot==1){
                canShoot=true;
            }
        }else{
            canShoot=false;
        }
        
        return canShoot;
    }
    public void Water(){
        if(plantShoot==0){
            if(coolDown<5){
                Instantiate(water, waterLocal.position, waterLocal.rotation);
            }
        }
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Ground")){
            CanShoot();
            activeBag.SetActive(true);
            canRun=true; 
        }else{
            canShoot=false;
        }
    }
    public void PlantingShoots(){
        if(plantShoot>=2){
            plantShoot=1;
            plantLocal.SetActive(true);
        }
        if(plantShoot<=0){
            plantShoot=0;
            canShoot=false;
            plantLocal.SetActive(false);
        }
    }
    void Update()
    {
        PlantingShoots();
        coolDown-=Time.deltaTime;
        if(coolDown<=0)
            coolDown=0;
    }
}
