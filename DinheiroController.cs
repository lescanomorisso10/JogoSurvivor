using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinheiroController : MonoBehaviour
{
    // Start is called before the first frame update
    GameController gController;
    // Start is called before the first frame update
    void Start()
    {
        gController=GameObject.FindObjectOfType<GameController>();
        
    }
    void Update(){
        Destroy(gameObject, 5f);
    }
    
    
        

    

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag=="Player"){
            
            
            gController.SetMoney();



            Destroy(gameObject);
            

        }
        
    }
}
