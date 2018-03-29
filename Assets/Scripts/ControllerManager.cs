using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour
{
    public bool triggerButtonDown = false;
    public Rigidbody spellPrefab;
    public Transform firePosition;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private SteamVR_Controller.Device controller
    {

        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {

        if (controller == null)
        {
            Debug.Log("Controller not initialized");

            return;
        }

        triggerButtonDown = controller.GetPressDown(triggerButton);

        if (triggerButtonDown)
        {
            Debug.Log("Fire");
            Rigidbody spellInstance;
            spellInstance = Instantiate(spellPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
            spellInstance.AddForce(firePosition.forward * 2000);
        }
    }
}

