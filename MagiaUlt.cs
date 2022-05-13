using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaUlt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifeTime;
    
    
    [SerializeField] private float spellSpeed;
    public int dano4;
    public float nextDamage=0.5f;
    public float timeDamage=0.5f;
    public static MagiaUlt instance;
    InimigoBase inimigoB;
    Player player;
    private Transform playerPos;
    private Vector3 target;
    public float vida;
    public float coolDown;

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        instance = this;
        
        player=GameObject.FindObjectOfType<Player>();
        playerPos=GameObject.FindGameObjectWithTag("Player").transform;
        target= new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;
        transform.position=Vector3.MoveTowards(transform.position, playerPos.position, spellSpeed * Time.deltaTime);
        nextDamage+=Time.deltaTime;
        
    }

    public void DanoUlt(){
        
    }
    /*private void OnTriggerStay(Collider collider){
       if(collider.gameObject.CompareTag("Enemy")){
           if(coolDown>=nextDamage){
            InimigoBase.instance.vida-=dano4;
            nextDamage=coolDown+timeDamage;
            Player.instance.playerLife+=dano4;
            DamagePopup.Create(this.transform.position, dano4, InimigoBase.instance.isCriticalHit);
            coolDown+=0.5f;
            
        }
            
            if(InimigoBase.instance.isCriticalHit) dano4*=2;
                
            
                InimigoBase.instance.anim.SetTrigger("Damage");

       }
    }*/
   
}
