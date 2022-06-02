using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class TwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    // Start is called before the first frame update
    void Start()
    {   
        foreach ( var item in secondHandGrabPoints){
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectEntered.AddListener(OnSecondHandRelease);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ProcessInteractable ( XRInteractionUpdateOrder.UpdatePhase updatePhase){
        if(secondInteractor && selectingInteractor){
            selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        base.ProcessInteractable(updatePhase);
    }
    public void OnSecondHandGrab(XRBaseInteractor interactor ){
        Debug.Log("Second Grab");
        secondInteractor = interactor;
    }
    public void OnSecondHandRelease(XRBaseInteractor interactor ){
        Debug.Log("Second Release");
        secondInteractor=null;
    }
    protected override void OnSelectEntering(XRBaseInteractor interactor){
        Debug.Log("GrabEnter");
        base.OnSelectEntering(interactor);
        attachInitialRotation = interactor.attachTransform.localRotation;
        
    }
    protected  override void OnSelectExiting(XRBaseInteractor interactor){
        Debug.Log("GrabExit");
        base.OnSelectExiting(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachInitialRotation;
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor){
        bool isalreadygrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabbed;
    }
}
