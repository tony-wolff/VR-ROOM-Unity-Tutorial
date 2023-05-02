using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandle : XRGrabInteractable
{
    GameObject padlock;
    GameObject door;
    XRBaseController controller;
    private InputDevice _device;
    Vector3 controllerVelocity;
    Vector3 startPosition;
    Vector3 stopPosition;
    float distance;
    [Header("Door Data")]
    public float doorHeaviness;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = door.transform.position;
        distance = 0.8f;
        Vector3 m_WorldDragDirection = transform.TransformDirection(new Vector3(-1, 0, 0)).normalized;
        stopPosition = startPosition + m_WorldDragDirection * distance;
    }

    protected override void Awake()
    {
        base.Awake();
        padlock = GameObject.FindGameObjectWithTag("padlock");
        interactionLayers = InteractionLayerMask.GetMask("Nothing");
        door = GameObject.FindGameObjectWithTag("door");

        trackPosition = false;
        trackRotation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        XRBaseControllerInteractor cont = args.interactorObject as XRBaseControllerInteractor;
        controller = cont.xrController;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (!padlock.activeSelf)
        {
            interactionLayers = InteractionLayerMask.GetMask("Default");
        }
        if (isSelected)
        {
            Vector3 pullDirection = transform.position - controller.transform.position;
            Vector3 moveDirection = new Vector3(-1, 0, 0);
            if (Vector3.Dot(pullDirection, door.transform.right) > 0.1 || Vector3.Dot(pullDirection, door.transform.right) < -0.1)
            {
                //Go left
                if (Vector3.Magnitude(pullDirection) < 0 && transform.position.z < startPosition.z)
                    return;
                door.transform.Translate(moveDirection * Vector3.Dot(pullDirection, door.transform.right) * Time.deltaTime);
                transform.Translate(moveDirection * Vector3.Dot(pullDirection, door.transform.right) * Time.deltaTime);
            }
                
        }
    }

    void UpdateVelocity()
    {
        var c = controller as ActionBasedController;
        if (GameObject.FindGameObjectWithTag("lefthand").GetComponent<ActionBasedController>() == c)
        {
            _device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }
        else if (GameObject.FindGameObjectWithTag("righthand").GetComponent<ActionBasedController>() == c)
        {
            _device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        }
        _device.TryGetFeatureValue(CommonUsages.deviceVelocity, out controllerVelocity);
    }

}
