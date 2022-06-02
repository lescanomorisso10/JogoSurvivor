using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeEffect : MonoBehaviour
{
    public ParticleSystem rockParticle;
    private int rockLife=100;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerExit(Collider collision){
        if(collision.gameObject.CompareTag("Pickaxe")){
            Debug.Log("Hittou");
            rockLife-=1;
            if(rockLife<=0){
                Instantiate(rockParticle, this.transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
            

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
