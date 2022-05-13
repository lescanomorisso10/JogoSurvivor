using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    MagiaEnemyRange enemyR;
     public float speed;
    private Transform alvo;
    public Animator anim;

    
    public int vida;
    
    
    
    
    MagiaBase magia;
    AtaqueBasico ataque;
    MagiaMédia magiaM;
    Player player;

    
    
    public float distTiro;
    public GameObject tiro;
    public Transform shootPoint;
    bool isShoting;

    public GameObject money;
    public float coolDownTotal;
   
    //Ponto para o qual o personagem irá se mover
    
    
    //Variável NavMeshAgent Para configurar A movimentação do personagem
    private UnityEngine.AI.NavMeshAgent agent;
    public float distanciaAtaque = 2;
   
    private bool isCriticalHit=false;
    public float recuo;
    public float coolDown;
    void Start()
    {   
        player=GameObject.FindObjectOfType<Player>();
        anim=GetComponent<Animator>();
        magia=GameObject.FindObjectOfType<MagiaBase>();
        ataque=GameObject.FindObjectOfType<AtaqueBasico>();
        magiaM=GameObject.FindObjectOfType<MagiaMédia>();
        enemyR=GameObject.FindObjectOfType<MagiaEnemyRange>();
        
        alvo=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed ;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance=distanciaAtaque;
        
        
       
    }
    private void OnTriggerEnter(Collider collider){
       if(collider.gameObject.CompareTag("Skill1")){
            vida-=ataque.dano1;
            if(isCriticalHit) ataque.dano1*=2;
            DamagePopup.Create(this.transform.position, ataque.dano1, isCriticalHit);
            speed=0;
           anim.SetTrigger("Damage");
       }
       if(collider.gameObject.CompareTag("Skill2")){
           vida-=20;
           agent.speed-=recuo;
           DamagePopup.Create(transform.position, 20, isCriticalHit);
           player.playerLife+=0.1f;
           anim.SetTrigger("Damage");
       }
       if(collider.gameObject.CompareTag("Skill3")){
           vida-=100;
           
           DamagePopup.Create(transform.position, 100, isCriticalHit);
           anim.SetTrigger("Damage");
       }
       if(collider.gameObject.CompareTag("Skill4")){
           vida-=500;
           player.playerLife+=1;
           DamagePopup.Create(transform.position, 500, isCriticalHit);
           anim.SetTrigger("Damage");
       }
       else{
           

            
        
       }
    }
    
    void Update()
    {   
    
    coolDown -= Time.deltaTime;
    if(vida<=0){
        Instantiate(money,this.transform.position,this.transform.rotation);
        Destroy(gameObject);
    } 
    anim.SetFloat("Speed", 1 );
    
    
    float distance = Vector3.Distance(this.transform.position, alvo.transform.position);
    if(distance<=distanciaAtaque && !isShoting){
        agent.speed=0;
        InvokeRepeating("Shooti", 0f, 0.5f);
        
        anim.SetFloat("Speed", 0 );
        isShoting= true;
    }else{
        agent.speed=speed;
    }
    if (agent.speed<1){
            anim.SetInteger("Speed", 0);
    }else{
            anim.SetInteger("Speed", 1);

        }

        
        
    
        //anim.SetFloat("Horizontal", alvo.position.x );
        //anim.SetFloat("Vertical", alvo.position.y);
    
        agent.SetDestination(alvo.position);  
        
        
    }
    void Shooti(){
        if(coolDown<=0){
            Instantiate(tiro, shootPoint.position, shootPoint.rotation);
            coolDown+=coolDownTotal;
        }
        
    }
      
        //Faz o personagem se locomover pelo cenario até o point
     

    
    
                
        
        
        
}

