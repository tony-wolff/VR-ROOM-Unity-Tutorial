using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TouchButton : XRBaseInteractable
{
    Material originalMat;
    int number;
    XRBaseController active_controller;
    [Header("button properties")]
    public Material hoverMat;
    public GameObject numberpad;
    // Start is called before the first frame update
    void Start()
    {
        active_controller = null;
        originalMat = GetComponent<Renderer>().material;
        string text = GetComponentInChildren<TMP_Text>().text;
        number = int.Parse(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        XRBaseControllerInteractor controller_interactor = args.interactorObject as XRBaseControllerInteractor;
        if (active_controller != null)
            return;
        active_controller = controller_interactor.xrController;
        print(active_controller);
        gameObject.GetComponent<Renderer>().material = hoverMat;
        NumberPad np_script = numberpad.GetComponent<NumberPad>();
        np_script.setCode(number);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        gameObject.GetComponent<Renderer>().material = originalMat;
        active_controller = null;
    }

    public int getNumber()
    {
        return number;
    }
}
