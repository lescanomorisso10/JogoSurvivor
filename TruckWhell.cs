using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckWhell : MonoBehaviour
{
    public ParticleSystem smoke;
    Truck truck;
    // Start is called before the first frame update
    void Start()
    {
        truck=GameObject.FindObjectOfType<Truck>();
    }
    public void OnTriggerStay(Collider collision){
        if(collision.gameObject.CompareTag("Ground")){
            if(truck.navMeshAgent.speed>1){
                Instantiate(smoke, this.transform.position, Quaternion.identity);
            }
        }
    }
}
