using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss1 : MonoBehaviour
{
    // Start is called before the first frame update
     public float speed;
    private Transform player;
    private Vector3 target;
    public float danoEnemyRange=15;
    private float skillLife=40;
    void Start(){
        player=GameObject.FindGameObjectWithTag("Player").transform;
        target= new Vector3(player.position.x, player.position.y, player.position.z);
    }
    void Update(){
        if(skillLife<=0){
            DestroyObjectoDeTiroEnemy();
        }
        transform.position=Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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
        if(collider.CompareTag("Skill2")){
            skillLife-=20;
        }
    }
    void DestroyObjectoDeTiroEnemy(){
        Destroy(gameObject);
    }
}
