using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rig;
    public float speed;
    public Animator anim;
    InimigoBase inimigo;
    private Vector2 move;
    public float playerLife;
    public float overHeal;
    public float overHealMax=200;
    public float healthMax=200;
    public Image BarraDeVida;
    public Image BarraOverHeal;
    ObjetoDeTiroEnemy tiroEnemy;
    public static Player instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance=this;
        tiroEnemy=GameObject.FindObjectOfType<ObjetoDeTiroEnemy>();
        inimigo=GameObject.FindObjectOfType<InimigoBase>();
        anim=GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider){
       if(collider.gameObject.CompareTag("Enemy")){
           if(overHeal>0){
               overHeal-=5;
           }else
           playerLife-=5;
           
       }
       if(collider.gameObject.CompareTag("SkillEnemyRange")){
           if(overHeal>0){
               overHeal-=5;
           }else
           playerLife-=10;
           playerLife-=tiroEnemy.danoEnemyRange;
           overHeal-=tiroEnemy.danoEnemyRange;
       }
       if(collider.gameObject.CompareTag("SkillBoss1")){
           if(overHeal>0){
               overHeal-=5;
           }else
           playerLife-=50;
           playerLife-=tiroEnemy.danoEnemyRange;
           overHeal-=tiroEnemy.danoEnemyRange;
       }
    }
    private void BarraVida(){
        BarraDeVida.fillAmount=playerLife/healthMax;
    }
    private void OverHeal(){
        BarraOverHeal.fillAmount=overHeal/overHealMax;
    }
    // Update is called once per frame
    void Update()
    {   
        if(playerLife>=healthMax){
            
            playerLife=healthMax;
            BarraOverHeal.enabled=true;
            BarraDeVida.enabled=false;
        }
        if(overHeal>=overHealMax){
            overHeal=overHealMax;
        }
        BarraVida();
        OverHeal();
        if(overHeal<=0){
           BarraOverHeal.enabled=false;
           BarraDeVida.enabled=true;
        }
        if(playerLife<=0){
            this.gameObject.SetActive(false);
        }
        move.x=Input.GetAxis("Horizontal");
        move.y=Input.GetAxis("Vertical");
        move.Normalize();

        rig.velocity=new Vector3(move.x* speed, rig.velocity.y, move.y*speed);
        anim.SetFloat("Horizontal", move.x );
        anim.SetFloat("Vertical", move.y);
    
     
        anim.SetFloat("Speed", move.magnitude );
    }

}
