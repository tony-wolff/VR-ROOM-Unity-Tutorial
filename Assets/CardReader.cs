using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CardReader : XRSocketInteractor
{
    XRGrabInteractable card_interactable;
    bool isIn;
    bool vectorValid;
    Vector3 enteredPosition;
    Vector3 exitedPosition;
    public float distanceToValidate;
    GameObject ui_vr;
    ChangeMaterial greenlight_sc;
    ChangeMaterial redlight_sc;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        vectorValid = true;
        isIn = false;
        ui_vr = GameObject.FindGameObjectWithTag("ui");
        greenlight_sc = GameObject.FindGameObjectsWithTag("light")[1].GetComponent<ChangeMaterial>();
        redlight_sc = GameObject.FindGameObjectsWithTag("light")[0].GetComponent<ChangeMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return false;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        isIn = true;
        vectorValid = true;
        ui_vr.GetComponentInChildren<TMP_Text>().text = "In";
        card_interactable = args.interactableObject as XRGrabInteractable;
        enteredPosition = card_interactable.transform.position;

    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        isIn = false;
        exitedPosition = card_interactable.transform.position;

        ui_vr.GetComponentInChildren<TMP_Text>().text += ", distance: " + (enteredPosition.y - exitedPosition.y);
        if (vectorValid && (distanceToValidate <= Mathf.Abs(enteredPosition.y - exitedPosition.y)) )
        {
            GameObject padlock = GameObject.FindGameObjectWithTag("padlock");
            padlock.SetActive(false);
            socketActive = false;
            greenlight_sc.SetOtherMaterial();
            redlight_sc.SetOriginalMaterial();
        }
        else
        {
            redlight_sc.SetOtherMaterial();
            greenlight_sc.SetOriginalMaterial();
        }

    }

    public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractor(updatePhase);
        if (isIn)
        {

            float res = Mathf.Abs(Vector3.Dot(card_interactable.transform.up, Vector3.up)) ;
            ui_vr.GetComponentInChildren<TMP_Text>().text = "\ndot product: " + res;
            float velo = Vector3.Magnitude(card_interactable.GetComponent<Rigidbody>().velocity);
            ui_vr.GetComponentInChildren<TMP_Text>().text += "\nvelocity: " + velo;
            if (res > 0.1 || velo < 0.5)
            {
                vectorValid = false;
            }
        }
    }
}
