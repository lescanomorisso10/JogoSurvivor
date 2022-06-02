using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController ;

    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    private GameObject spawnedHandModel;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        TryInitialize();
        
        
    }
    void UpdateHandAnim(){
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)){
            anim.SetFloat("Trigger", triggerValue);
        }else{
            anim.SetFloat("Trigger", 0);
        }
        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)){
            anim.SetFloat("Grip", gripValue);
        }else{
            anim.SetFloat("Grip", 0);
        }
    }
    
    void TryInitialize(){
        List<InputDevice> devices = new List<InputDevice>();
        
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach ( var item in devices){
            Debug.Log(item.name + item.characteristics);
        }
        if(devices.Count > 0){
            targetDevice= devices[0];
            GameObject prefab = controllerPrefabs.Find(controllerPrefabs => controllerPrefabs.name == targetDevice.name);
            if(prefab){
                spawnedController= Instantiate(prefab, transform);
                
                 showController=false;
            }else{
                Debug.LogError("Controller not found");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
                
                showController=true;
            
                spawnedHandModel=Instantiate(handModelPrefab, transform);
                anim= spawnedHandModel.GetComponent<Animator>();

                if(showController){
                    spawnedHandModel.SetActive(false);
                    spawnedController.SetActive(true);
                }else{
           
                    spawnedHandModel.SetActive(true);
                    spawnedController.SetActive(false);
                    UpdateHandAnim();
                }    
            } 
        }
    }
    void Update()
    {
        if(!targetDevice.isValid){
            TryInitialize();
        }else{
            if(showController){
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
            }else{
           
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
            UpdateHandAnim();
        }
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if(primaryButtonValue){
            
            
           showController=false;
        }
        targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButton);
        if(secondaryButton){
           

            showController= true;
        }

            

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if(triggerValue > 0.1f){
            
        }
            

        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if(primary2DAxisValue != Vector2.zero){

             }
                  
        }
    }
        
}
