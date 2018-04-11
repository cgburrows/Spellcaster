using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
    MagicBeam2 is an outdated script that was used to test the FireBeam() function that now exists in all other schools.
    Every school now includes three slots for beam prefabs: A start effect, a line renderer, and an impact effect. This
    script combines the three into one particle effect that is called by instantiating them at the same time. The beam
    itself is directioned by a raycast that fires from a "spell spawner" at the tip of the wand that ends at the nearest
    object with a collider. The texture is then drawn onto the raycast and is animated to scroll in a loop along the
    line render to make it look less like a laser pointer and more like a powerful magic spell.    
*/

public class MagicBeam2 : MonoBehaviour
{

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    public Transform firePosition;

    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_TrackedObject trackedObj;

    [Header("Prefabs/Effects")]
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture

    [Header("Put Sliders here (Optional)")]
    public Slider endOffSetSlider; //Use UpdateEndOffset function on slider
    public Slider scrollSpeedSlider; //Use UpdateScrollSpeed function on slider

    [Header("Put UI Text object here to show beam name")]
    public Text textBeamName;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        if (endOffSetSlider)
            endOffSetSlider.value = beamEndOffset;
        if (scrollSpeedSlider)
            scrollSpeedSlider.value = textureScrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);

        // Instantiate all parts of the beam at the same time and set them up to be rotated
        if (triggerButtonDown)
        {
            beamStart = Instantiate(beamStartPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beamEnd = Instantiate(beamEndPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            beam = Instantiate(beamLineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            line = beam.GetComponent<LineRenderer>();
        }

        // Destroy the beam on trigger up
        if (triggerButtonUp)
        {
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
        }

        // While the trigger is held down, transform the beam towards where the user is pointing
        if (triggerButtonPressed)
        {
            RaycastHit _hit;

            if (Physics.Raycast(firePosition.position, transform.forward, out _hit))
            {
                Vector3 tdir = _hit.point - firePosition.position;
                ShootBeamInDir(firePosition.position, tdir);
            }
        }
    }

    public void UpdateEndOffset()
    {
        beamEndOffset = endOffSetSlider.value;
    }

    public void UpdateScrollSpeed()
    {
        textureScrollSpeed = scrollSpeedSlider.value;
    }

    // This function controls the two vectors (beam start and beam end) that track the user's aiming
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
}
