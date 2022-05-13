using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaMédia : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    
    
    [SerializeField] private float spellSpeed;
    public int dano3;
    private float contadorExplosao=4;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
            transform.Translate(Vector2.right*spellSpeed*Time.deltaTime);
        
        
    }
    private void OnTriggerEnter(Collider collider){
       if(collider.CompareTag("Enemy")){
           dano3=20;
       }
    }
}
