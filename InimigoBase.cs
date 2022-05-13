using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using CodeMonkey.Utils;


public class InimigoBase : MonoBehaviour
{
    
    public float speed;
    private Transform alvo;
    public Animator anim;
    public float distance;
    public float vida;
    
    public int danoInimigoBase=5;
    
    bool moving;
    MagiaBase magia;
    AtaqueBasico ataque;
    MagiaMédia magiaM;
    Player player;
    public GameObject money;
   
    public float tempoDevida;
    private Renderer sr;
    private NavMeshAgent agent;
    
   
    public bool isCriticalHit=false;
    public float recuo;
    private Color ColorToChange=Color.red;
    private AudioSource audioS;
    private float timeMoney=2f;
    public static InimigoBase instance;
    void Start()
    {   
        instance=this;
        audioS = GetComponent<AudioSource>();
        sr=GetComponent<Renderer>();
        player=GameObject.FindObjectOfType<Player>();
        anim=GetComponent<Animator>();
        magia=GameObject.FindObjectOfType<MagiaBase>();
        ataque=GameObject.FindObjectOfType<AtaqueBasico>();
        magiaM=GameObject.FindObjectOfType<MagiaMédia>();
        
        
        alvo=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        //Pega o Componente NavMeshAgent
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed ;
        agent.stoppingDistance=distance;
        //Variaveis setadas como False para Não utilizar os eixos Y Baseado em 3 dimensões
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        distance=2;
        MagiaUlt.instance.vida=vida;
        MagiaBase.instance.vida=vida;
       
    }
    private void OnTriggerEnter(Collider collider){
       if(collider.gameObject.CompareTag("Skill1")){
            vida-=ataque.dano1;
            if(isCriticalHit) ataque.dano1*=2;
            DamagePopup.Create(this.transform.position, ataque.dano1, isCriticalHit);
            
           anim.SetTrigger("Damage");

       }
       if(collider.gameObject.CompareTag("Skill2")){
           
           MagiaBase.instance.DanoMagiaBase();
            vida-=MagiaBase.instance.dano2;
            
           agent.speed=0;
           DamagePopup.Create(transform.position, MagiaBase.instance.dano2, isCriticalHit);
           
           sr.material.color=ColorToChange;
           anim.SetTrigger("Damage");
           agent.Move(-alvo.position*recuo);
           
           audioS.Play();
       }
       if(collider.gameObject.CompareTag("Skill3")){
           vida-=100;
           
           DamagePopup.Create(transform.position, 100, isCriticalHit);
           sr.material.color=ColorToChange;
           anim.SetTrigger("Damage");
       }
       
       if(collider.gameObject.CompareTag("Vortex")){
           agent.Move(Vortex.instance.pivo.position*recuo*5);
       }
       
    }
    private void OnTriggerStay(Collider collision){
           if(collision.gameObject.CompareTag("Skill4")){
                if(MagiaUlt.instance.coolDown>=MagiaUlt.instance.nextDamage){
                    vida-=MagiaUlt.instance.dano4;
                    MagiaUlt.instance.nextDamage=MagiaUlt.instance.coolDown+MagiaUlt.instance.timeDamage;
                    Player.instance.playerLife+=MagiaUlt.instance.dano4;
                     DamagePopup.Create(transform.position, MagiaUlt.instance.dano4, isCriticalHit);
                    sr.material.color=ColorToChange;
                    anim.SetTrigger("Damage");
                    if(Player.instance.playerLife>=Player.instance.healthMax){
                        Player.instance.overHeal+=MagiaUlt.instance.dano4;
                    }
                    
                    
                    MagiaUlt.instance.coolDown+=0.5f;

        }
       

            
        }
           
          
       }
    
    void Update()
    {   
        
        
        if(agent.speed<=0){
            agent.speed+=2;
            
            if(agent.speed>=3){
                agent.speed=3;
                
                
            }
        }
        MagiaUlt.instance.coolDown+=Time.deltaTime;
        
        danoInimigoBase=5;

        if(vida<=0){
            Destroy(gameObject, 0.3f);
            timeMoney-=0.05f;
            if(timeMoney<=0){
                Instantiate(money,this.transform.position,this.transform.rotation);
            }
            
            
        }

    
    
    anim.SetFloat("Horizontal", alvo.position.x );
    anim.SetFloat("Vertical", alvo.position.y);
    
    agent.SetDestination(alvo.position);  
    anim.SetFloat("Speed", 1 );
    if(distance<=0.5){
        
        anim.SetFloat("Speed", 0 );
    }
      
        //Faz o personagem se locomover pelo cenario até o point
     

    
    
                
        
        
        
    }
     public void Damage(float takeDamage){
        
        vida -= takeDamage;

        if(vida<=0){
            Destroy(gameObject, 0.3f);
            timeMoney-=0.05f;
            if(timeMoney<=0){
                Instantiate(money,this.transform.position,this.transform.rotation);
            }
            
            
        }
    }
    
    
    // Start is called before the first frame update
   

}