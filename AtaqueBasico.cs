using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBasico : MonoBehaviour
{
    bool go;
    public GameObject player;
    Transform itemToRotate;
    [SerializeField] private float time;
    Skills skill;
    public int dano1;
    

    Vector3 locationInFrontOfPlayer;
    

    // Start is called before the first frame update
    void Start()
    {
        
        skill=GameObject.FindObjectOfType<Skills>();
        go=false;
        player=GameObject.Find("Sprite");

        itemToRotate=gameObject.transform;

        locationInFrontOfPlayer=new Vector3(player.transform.position.x, player.transform.position.y , player.transform.position.z) + skill.barrel.transform.right * 10f;

        StartCoroutine(Boom());
    }
    IEnumerator Boom(){
        go=true;
        yield return new WaitForSeconds(0.3f);
        go=false;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        itemToRotate.transform.Rotate(0,Time.deltaTime * 500, 0);
        if(go){
            transform.position= Vector3.MoveTowards(transform.position, locationInFrontOfPlayer,Time.deltaTime*40);
        }
        if(!go){
            transform.position= Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y +1, player.transform.position.z), Time.deltaTime *40);

        }
        if(!go && Vector3.Distance(player.transform.position, transform.position) <1.5){
            Destroy(this.gameObject);
        }
    }
    
}

