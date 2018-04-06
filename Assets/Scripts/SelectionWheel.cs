using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionWheel : MonoBehaviour {
    private SteamVR_TrackedController rightController;
    private bool activity;
    public GameObject SelectionWheel1;
	// Use this for initialization
	void Start () {
        activity = false;
	}

    private void OnEnable()
    {
        rightController = GetComponent<SteamVR_TrackedController>();
        
    }
    // Update is called once per frame
    void Update () {
        SelectionWheel1.SetActive(activity);
        if (rightController.padTouched)
        {
            activity = true;
        }
        else
            activity = false;
	}
    
}
