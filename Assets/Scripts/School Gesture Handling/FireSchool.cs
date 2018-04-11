using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AirSig;

public class FireSchool : MonoBehaviour
{
    [Header("Missile Spell")]
    public Rigidbody missilePrefab;
    public float missileDPS;
    public float missileForce;

    [Header("Beam Spell")]
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    public float beamDPS;

    [Header("Beam Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture

    [Header("Burn Spell")]
    public ParticleSystem flame;
    public AudioSource fireSound;

    // Callback for receiving signature/gesture progression or identification results
    AirSigManager.OnDeveloperDefinedMatch developerDefined;

    // particles to be drawn with during gesture recognition
    [Header("AirSig and Tracking")]
    public ParticleSystem track;

    // Reference to AirSigManager for setting operation mode and registering listener
    public AirSigManager airsigManager;
    public SteamVR_TrackedObject rightHandControl;

    private ControllerManager controllerManager;
    private string lastMatchedGesture;
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    [Header("Grip Buttons")]
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    [Header("Spell Spawner")]
    public Transform firePosition;
    public GameObject targetDummy;
    //public string damageType = "fire";

    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_TrackedObject trackedObj;

    // Handling developer defined gesture match callback - This is invoked when the Mode is set to Mode.DeveloperDefined and a gesture is recorded.
    // gestureId - a serial number
    // gesture - gesture matched or null if no match. Only guesture in SetDeveloperDefinedTarget range will be verified against
    // score - the confidence level of this identification. Above 1 is generally considered a match
    void HandleOnDeveloperDefinedMatch(long gestureId, string gesture, float score)
    {
        if (score >= 1)
        {
            if ((gesture == "C" || gesture == "CLOSEDX") && score >= 0.7f)
            {
                lastMatchedGesture = gesture;
                controllerManager.timeLeft = 6.0f;
                controllerManager.casting = true;
                //airsigManager.SetMode(AirSigManager.Mode.None);
            }
        }
    }

    private void OnEnable()
    {
        controllerManager = rightHandControl.GetComponent<ControllerManager>();

        developerDefined = new AirSigManager.OnDeveloperDefinedMatch(HandleOnDeveloperDefinedMatch);
        airsigManager.onDeveloperDefinedMatch += developerDefined;
        airsigManager.SetDeveloperDefinedTarget(new List<string> { "C", "CLOSEDX" });
        airsigManager.SetClassifier("FireGestureProfile", "");

        airsigManager.SetTriggerStartKeys(
            AirSigManager.Controller.RIGHT_HAND,
            SteamVR_Controller.ButtonMask.Trigger,
            AirSigManager.PressOrTouch.PRESS);

        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void OnDisable()
    {
        // Unregistering callback
        airsigManager.onDeveloperDefinedMatch -= developerDefined;
    }

    // MAGIC BEAM STUFF STARTS HERE
    // ============================
    void FireBeam(bool gripButtonDown, bool gripButtonUp, bool gripButtonPressed)
    {
        var device1 = SteamVR_Controller.Input((int)rightHandControl.index);
        gripButtonDown = device1.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
        gripButtonUp = device1.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
        gripButtonPressed = device1.GetPress(SteamVR_Controller.ButtonMask.Grip);

        if (gripButtonDown)
        {
            beamStart = Instantiate(beamStartPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beamEnd = Instantiate(beamEndPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beam = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line = beam.GetComponent<LineRenderer>();
        }
        if (gripButtonUp)
        {
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
        }

        if (gripButtonPressed)
        {
            RaycastHit _hit;
            device1.TriggerHapticPulse(1500);

            if (Physics.Raycast(firePosition.position, firePosition.transform.forward, out _hit))
            {
                Vector3 tdir = _hit.point - firePosition.position;
                ShootBeamInDir(firePosition.position, tdir);
            }
        }
    }

    public void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            end = hit.point - (dir.normalized * beamEndOffset);
        else
            end = transform.position + (dir * 100);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }
    // ==========================
    // MAGIC BEAM STUFF ENDS HERE

    // MAGIC MISSILE SPELL
    // ===================
    public void FireMissile()
    {
        Rigidbody spellInstance;
        spellInstance = Instantiate(missilePrefab, firePosition.position, firePosition.rotation) as Rigidbody;
        spellInstance.AddForce(firePosition.forward * 2000);
    }
    // =======================
    // END MAGIC MISSILE SPELL

    // BURN SPELL
    // ===================
    public void BurnBaby()
    {
        //DamageOverTime.enable = true;
    }
    // =======================
    // END BURN SPELL

    // Update is called once per frame
    void Update()
    {
        FireBeam(gripButtonDown, gripButtonUp, gripButtonPressed);

        if ((!controllerManager.casting) && (-1 != (int)rightHandControl.index))
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
        else if (controllerManager.casting)
        {
            var device = SteamVR_Controller.Input((int)rightHandControl.index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (lastMatchedGesture == "C")
                {
                    FireMissile();
                }
                if (lastMatchedGesture == "CLOSEDX")
                {
                    ParticleSystem spellInstance;
                    spellInstance = Instantiate(flame, targetDummy.transform.position, targetDummy.transform.rotation) as ParticleSystem;
                    fireSound.Play();
                }
                controllerManager.casting = false;
                //airsigManager.SetMode(AirSigManager.Mode.DeveloperDefined);
            }
        }
    }
}
