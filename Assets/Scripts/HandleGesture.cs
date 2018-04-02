using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AirSig;

public class HandleGesture : MonoBehaviour
{

    // Callback for receiving signature/gesture progression or identification results
    AirSigManager.OnDeveloperDefinedMatch developerDefined;

    // Reference to AirSigManager for setting operation mode and registering listener
    public AirSigManager airsigManager;

    public SteamVR_TrackedObject rightHandControl;

    // particles to be drawn with during gesture recognition
    public ParticleSystem track;

    // Handling developer defined gesture match callback - This is invoked when the Mode is set to Mode.DeveloperDefined and a gesture is recorded.
    // gestureId - a serial number
    // gesture - gesture matched or null if no match. Only guesture in SetDeveloperDefinedTarget range will be verified against
    // score - the confidence level of this identification. Above 1 is generally considered a match
    void HandleOnDeveloperDefinedMatch(long gestureId, string gesture, float score)
    {
        if (gesture != null && score >= 1)
        {
            rightHandControl.GetComponent<ControllerManager>().Fire();

        }
    }

    private void Awake()
    {
        developerDefined = new AirSigManager.OnDeveloperDefinedMatch(HandleOnDeveloperDefinedMatch);
        airsigManager.onDeveloperDefinedMatch += developerDefined;
        airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
        airsigManager.SetDeveloperDefinedTarget(new List<string> { "HEART", "C", "DOWN", "HOURGLASS" });
        airsigManager.SetClassifier("TestGestureProfile", "");

        airsigManager.SetTriggerStartKeys(
            AirSigManager.Controller.RIGHT_HAND,
            SteamVR_Controller.ButtonMask.Trigger,
            AirSigManager.PressOrTouch.PRESS);

    }

    void OnDestroy()
    {
        // Unregistering callback
        airsigManager.onDeveloperDefinedMatch -= developerDefined;
    }

    // Update is called once per frame
    void Update()
    {
        if (-1 != (int)rightHandControl.index)
        {
            var device = SteamVR_Controller.Input((int)rightHandControl.index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                track.Play();
            }
            else if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                track.Stop();
                track.Clear();
            }
        }
    }
}
