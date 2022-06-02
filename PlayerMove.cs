using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMove : MonoBehaviour
{
    public Transform groundCheck;
    public float speed=1;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;

    public LayerMask groundLayer;
    public float gravity = -9.81f;
    private float fallingSpeed;
    private float additionalHeight=0.5f;
    private XRRig rig;
    private bool grounded=false;
    Truck truck;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<XRRig>();
        character=GetComponent<CharacterController>();
        truck=GameObject.FindObjectOfType<Truck>();
    }
    void Update()
    {
        InputDevice device= InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
    private void FixedUpdate(){
        FallowHeadset();
        Quaternion headYaw =Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction* Time.fixedDeltaTime*speed);
        //queda
        bool isGrounded = GroundCheck();
        if(isGrounded)
            fallingSpeed=0;
        else 
            fallingSpeed += gravity * Time.deltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Truck")){
            if(truck.navMeshAgent.speed>=1){
                Debug.Log("Foste Atropelado");
            }
        }
    }
    void FallowHeadset(){
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2  + character.skinWidth, capsuleCenter.z);
    }
    bool GroundCheck(){
        grounded=Physics.Linecast(transform.position, groundCheck.position, 1<<LayerMask.NameToLayer("Ground"));
        //Vector3 rayStart = transform.TransformPoint(character.center);
        //float rayLength = character.center.y + 1f;
        //bool hasHit=Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        //return hasHit;
        return grounded;
    }
}
