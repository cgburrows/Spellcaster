using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public float timeLeft;

    public bool timeUp;

    public delegate void TimerDelegate();
    public TimerDelegate timerDelegate;

    public Timer(float t)
    {
        this.timeLeft = t;
    }

	// Use this for initialization
	void Start () {
        timeUp = false;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeUp = true;
            if (this.timerDelegate != null)
            {
                timerDelegate();
            }
        }
	}
}
