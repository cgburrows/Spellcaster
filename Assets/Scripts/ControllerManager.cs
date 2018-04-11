using UnityEngine;
using System.Collections;

using AirSig;

public class ControllerManager : MonoBehaviour
{
    //[Header("Trigger Buttons")]
    //public bool triggerButtonDown = false;
    //public bool triggerButtonUp = false;
    //public bool triggerButtonPressed = false;

    public float timeLeft;

    public bool casting;
    public bool slow;

    public ParticleSystem timeBubble;

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
        airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        casting = false;
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

        if (slow == true)
        {
            if (Time.timeScale == 1.0F)
            {
                Time.timeScale = 0.5F;
            }
            else
            {
                Time.timeScale = 1.0F;
            }
            Time.fixedDeltaTime = 0.2F * Time.timeScale;
            slow = false;
        }
        if (Time.timeScale == 0.5f)
        {
            timeBubble.Play();
        }
        else
        {
            timeBubble.Stop();
            timeBubble.Clear();
        }
    }
}

