using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaEnemyRange : MonoBehaviour
{
    public Transform barrel;
    [SerializeField] private float fireRate;
    private float fireTimer;
    [SerializeField] private GameObject spell;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Shoot(){
        if(CanShoot()){
            fireTimer = Time.time+fireRate;
            Instantiate(spell, barrel.position, barrel.rotation);
        }
        

    }
    private bool CanShoot(){
        return Time.time > fireTimer;
    }
    private void Spell(){
        
        print("Tiro");
        Shoot();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
