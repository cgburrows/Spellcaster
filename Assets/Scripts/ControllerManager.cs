using UnityEngine;
using System.Collections;

using AirSig;

public class ControllerManager : MonoBehaviour
{
    public bool triggerButtonDown = false;
    public Rigidbody spellPrefab;
    public Transform firePosition;

    public float timeLeft;

    public bool casting;

    public AirSigManager airsigManager;

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
        casting = false;
    }


    public void Fire()
    {
        Debug.Log("Fire");
        Rigidbody spellInstance;
        spellInstance = Instantiate(spellPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
        spellInstance.AddForce(firePosition.forward * 2000);
    }

    private void Update()
    {
        if (casting == true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                casting = false;
                //airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
            }
        }
    }
}

