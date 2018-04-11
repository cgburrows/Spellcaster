using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is the script for the Selection Wheel that is attached to the controller. It splits the controller touchpad
    into six equal "slices" that, when clicked on, will swap the user's current spell school to the desired element.

    We also use a special function to find out where the user's thumb is on the touchpad, and show/hide the wheel
    depending on if the user has their thumb on the touchpad.

    The game objects are the actual game objects that house the spells and gestures.
*/

public class SelectionWheel : MonoBehaviour
{
    private SteamVR_TrackedController rightController;
    private SteamVR_TrackedObject trackedObject;

    // keep track of the current school so it can be disabled when another is enabled
    private GameObject CurrentSchool;

    public GameObject ArcaneSchool;
    public GameObject FrostSchool;
    public GameObject FireSchool;
    public GameObject LightSchool;
    public GameObject NatureSchool;
    public GameObject StormSchool;

    public GameObject SelectionWheel1;

    // Use this for initialization
    void OnEnable()
    {
        CurrentSchool = ArcaneSchool;
        CurrentSchool.SetActive(true);

        rightController = GetComponent<SteamVR_TrackedController>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();

        // subscribe event handlers
        rightController.PadClicked += HandlePadClicked;
        rightController.PadTouched += HandlePadTouched;
        rightController.PadUntouched += HandlePadUntouched;
    }

    // These two functions make the wheel visible or invisible depending on if the user has their
    // thumb on the touchpad
    private void HandlePadUntouched(object sender, ClickedEventArgs e)
    {
        SelectionWheel1.SetActive(false);
    }
    private void HandlePadTouched(object sender, ClickedEventArgs e)
    {
        SelectionWheel1.SetActive(true);
    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        // Here is the math that assigns each school to a wheel slice:
        if (e.padX != 0 || e.padY != 0)
        {
            float degree = FindDegree(e.padX, e.padY);
            Debug.Log("Degree: " + degree);

            if (30f < degree && degree <= 90f)
            {
                CurrentSchool.SetActive(false);
                LightSchool.SetActive(true);
                CurrentSchool = LightSchool;
            }
            else if (330f < degree || degree <= 30f)
            {
                CurrentSchool.SetActive(false);
                FireSchool.SetActive(true);
                CurrentSchool = FireSchool;
            }
            else if (270f < degree && degree <= 330f)
            {
                CurrentSchool.SetActive(false);
                StormSchool.SetActive(true);
                CurrentSchool = StormSchool;
            }
            else if (210f < degree && degree <= 270f)
            {
                CurrentSchool.SetActive(false);
                NatureSchool.SetActive(true);
                CurrentSchool = NatureSchool;
            }
            else if (150f < degree && degree <= 210f)
            {
                CurrentSchool.SetActive(false);
                ArcaneSchool.SetActive(true);
                CurrentSchool = ArcaneSchool;
            }
            else if (90f < degree && degree <= 150f)
            {
                CurrentSchool.SetActive(false);
                FrostSchool.SetActive(true);
                CurrentSchool = FrostSchool;
            }

        }
    }

    void OnDisable()
    {
        // unsubscribe event handlers
        rightController.PadClicked -= HandlePadClicked;
        rightController.PadTouched -= HandlePadTouched;
        rightController.PadUntouched -= HandlePadUntouched;
    }

    // Update is called once per frame
    void Update()
    {
        //device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    private float FindDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) / Math.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }
}
