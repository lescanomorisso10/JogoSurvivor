using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform barrel;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject spell;
    [SerializeField] private GameObject aa;
    [SerializeField] private GameObject MagiaMedia;
    [SerializeField] private GameObject MagiaUlt;
    public Vector3 mousePos;
    public float coolDown=0.5f;


    public float coolDown2=20f;
    private int coolDown2ToInt;
    public Text coolDown2Text;

    public float coolDown3=50f;
    private int coolDown3ToInt;
    public Text coolDown3Text;
    

    public Transform pivo;



    private float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        CoolDowns();
      
        
        
        Spell();
        AtaqueBasico();
        AtaqueMedio();
        Ult();
    }
    private void CoolDowns(){
        coolDown -= Time.deltaTime;
          
        coolDown3 -= Time.deltaTime;

        coolDown2 -= Time.deltaTime;
        coolDown2ToInt= (int)coolDown2;
        coolDown2Text.text=coolDown2ToInt.ToString(); 
        if(coolDown2<=0){
            coolDown2=0;
        }

        coolDown3 -= Time.deltaTime;
        coolDown3ToInt= (int)coolDown3;
        coolDown3Text.text=coolDown3ToInt.ToString(); 
        if(coolDown3<=0){
            coolDown3=0;
        }
    }
    private void Spell(){
        if(Input.GetMouseButtonDown(1) && CanShoot()){
            print("Tiro");
            Shoot();
        }
    }
    private void Shoot(){
        fireTimer = Time.time+fireRate;
        Instantiate(spell, barrel.position, barrel.rotation);

    }
    private bool CanShoot(){
        return Time.time > fireTimer;
    }
    private void AtaqueBasico(){
            
        if(coolDown<=0){
            GameObject clone;
            
        
        
            clone=Instantiate(aa, new Vector3(barrel.transform.position.x, barrel.transform.position.y +1, barrel.transform.position.z), barrel.rotation)as GameObject;
            coolDown=0.5f;
        }    
        
            
            
    }
    private void AtaqueMedio(){
        
        if(coolDown2<=0){
            if(Input.GetKeyDown(KeyCode.E)){
                Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo)){
                    mousePos=hitInfo.point;
                    Instantiate(MagiaMedia, mousePos, Quaternion.LookRotation(pivo.transform.position, Vector3.down));
                    coolDown2=20f;
                }
                

            }
        }
    }
    private void Ult(){
        if(coolDown3<=0){
            if(Input.GetKeyDown(KeyCode.Q)){
             Instantiate(MagiaUlt, barrel.position, barrel.rotation);
             coolDown3=50f;
            }
        }
    }
}
