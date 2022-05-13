using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigos : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;
    private float nextSpawn = 0f;
    Ciclo ciclo;
    // Start is called before the first frame update
    void Start()
    {
        ciclo=GameObject.FindObjectOfType<Ciclo>();
    }

    // Update is called once per frame
    void Update()
    {
        ciclo.dayTime+=Time.deltaTime;
        if(ciclo.dayTime>=24){
            spawnRate-=0.05f;
            
            
            
        }
        if(ciclo.isDay==false){
            Spawner();
        }
        if(spawnRate<=0){
            spawnRate+=0.2f;

        }
        
    }
    public void Spawner(){
        if(Time.time>nextSpawn){
            nextSpawn=Time.time+spawnRate;
            Instantiate(enemy, transform.position, enemy.transform.rotation);
        }
    }
}
