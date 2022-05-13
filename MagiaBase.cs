using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaBase : MonoBehaviour
{   
    [SerializeField] private float lifeTime;
    [SerializeField] private float spellSpeed;
    public  int dano2;
    private float curaMagia=10f;
    public static MagiaBase instance;
    public float vida;
    InimigoBase inimigo;

    // Start is called before the first frame update
    void Start()
    {
        inimigo=GameObject.FindObjectOfType<InimigoBase>();
        instance=this;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right*spellSpeed*Time.deltaTime);
    }
     public void DanoMagiaBase(){
        
            

            
     }
     private void OnTriggerEnter(Collider collider){
       if(collider.gameObject.CompareTag("Enemy")){
            inimigo.Damage(dano2);
            if(Player.instance.playerLife>=Player.instance.healthMax){
                        Player.instance.overHeal+=curaMagia;
                    }
            
            Player.instance.playerLife+=curaMagia;
            
           

       }
    }
    
}
