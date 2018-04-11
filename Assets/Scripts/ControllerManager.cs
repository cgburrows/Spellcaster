using UnityEngine;
using System.Collections;

using AirSig;

/*
    The controller manager is a key part of our game. Since some functions cannot be called outside of Update() we needed to
    get creative with how we handled spells. For example, the "Slow Time" spell needed to access a particle system outside
    of the ArcaneSchool script. To deal with this, the controller manager actually handles the Slow Time spell.

    The controller manager also records special events like the trigger being pulled and has the SteamVR_TrackedObject script
    on it to get other events. It's most important function is to detect whether or not the user is casting a spell or is
    drawing a gesture in the air. We don't want the user to accidentally record a spell when they fire another, so the
    controller manager tracks what the user is doing with the magic wand and employs a timer to make sure the user doesn't
    get stuck in one mode. After six seconds of not casting a spell, the controller manager discards the last tracked gesture
    and lets the user cast again.
*/

public class ControllerManager : MonoBehaviour
{
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
        casting = false; // the user does not cast upon spawn
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

        // special case of the slow time spell
        // time is slowed by half when the user casts the spell
        // and returned to normal upon the second cast
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

