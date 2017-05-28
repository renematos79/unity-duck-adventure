using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour {

	private SliderJoint2D Slider;
	private JointMotor2D Engine;
	private List<JointLimitState2D> JOINT_LIMITS = new List<JointLimitState2D> () {
		JointLimitState2D.LowerLimit,
		JointLimitState2D.UpperLimit
	};

	// Use this for initialization
	void Start () {
		Slider = GetComponent<SliderJoint2D> ();
		Engine = Slider.motor;
	}
	
	// Update is called once per frame
	void Update () {
		if (JOINT_LIMITS.Contains (Slider.limitState)) {
			Engine.motorSpeed = Slider.limitState == JointLimitState2D.LowerLimit ? 1 : -1;
			Slider.motor = Engine;
		}
	}
}
