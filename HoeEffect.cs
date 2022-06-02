using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoeEffect : MonoBehaviour
{
    
     public ParticleSystem dustParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Ground")){
            Debug.Log("Hittou");
            Instantiate(dustParticle, this.transform.position, Quaternion.identity);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
