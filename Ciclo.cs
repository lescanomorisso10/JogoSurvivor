using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ciclo : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;
    public float spawnRateBoss;
    private float nextSpawn = 0f;
    Ciclo ciclo;
    public float dayTime;
    public int dayCount;
    public bool isDay;
    public GameObject Dia;
    public GameObject Noite;
    public GameObject boss1;
    public Text dayCountText;
    // Start is called before the first frame update
    

    // Update is called once per frame
    
    public void Spawner(){
        if(Time.time>nextSpawn){
            nextSpawn=Time.time+spawnRate;
            Instantiate(enemy, transform.position, enemy.transform.rotation);
        }
    }
    public void SpawnerBoss1(){
        if(Time.time>nextSpawn){
            nextSpawn=Time.time+spawnRateBoss;
            Instantiate(boss1, transform.position, boss1.transform.rotation);
        }
        
    }
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {

        
        isDay=true;
        dayCount=0;
        dayTime=0;
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(dayCount==10){
            SpawnerBoss1();
        }
        if(isDay==false){
            Spawner();
        }
        if(spawnRate<=0){
            spawnRate+=0.2f;

        }
        dayTime+=Time.deltaTime;
        if(dayTime>=24){
            dayTime=0;
            dayCount++;
            dayCountText.text=dayCount.ToString();
            spawnRate-=00.05f;
            
        }
        if(dayTime>=18 || dayTime <6){
            if(isDay){
                
                isDay=false;
                Noite.gameObject.SetActive(true);
                Dia.gameObject.SetActive(false);
                
                
            }
        }else{
            if(!isDay){
                
                isDay=true;
                Noite.gameObject.SetActive(false);
                Dia.gameObject.SetActive(true);
            }
        }

        
    }
}
