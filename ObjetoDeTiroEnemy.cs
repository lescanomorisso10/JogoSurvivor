using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDeTiroEnemy : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    public float danoEnemyRange=15;
    void Start(){
        player=GameObject.FindGameObjectWithTag("Player").transform;
        target= new Vector3(player.position.x, player.position.y, player.position.z);
    }
    void Update(){
        
        transform.position=Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x==target.x && transform.position.y == target.y && transform.position.z == target.z){
            DestroyObjectoDeTiroEnemy();
        }

    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            DestroyObjectoDeTiroEnemy();
        }
          if(collider.CompareTag("Barreira")){
            DestroyObjectoDeTiroEnemy();
        }
    }
    void DestroyObjectoDeTiroEnemy(){
        Destroy(gameObject);
    }

}
