using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    [Header("General")]
    [Tooltip("In m/s")] [SerializeField] float Speed = 15f;
    [Tooltip("In m")] [SerializeField] float xMoveBoundary = 9f;
    [Tooltip("In m")] [SerializeField] float yMoveBoundary = 6f;
    [SerializeField] float lerpTValue = 0.2f;

    [Header("Pitch")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -20f;

    [Header("Yaw")]
    [SerializeField] float positionYawFactor = 3f;

    [Header("Roll")]
    [SerializeField] float controlRollFactor = -50f;

    float xThrow, yThrow;
    bool movementEnabled;

    // Use this for initialization
    void Start () {
        movementEnabled = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (movementEnabled)
        {
            HandleTranslation();
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        //Calculate target angles.
        float pitchfromPosition = transform.localPosition.y * positionPitchFactor;
        float pitchfromControl = yThrow * controlPitchFactor;
        float pitchTarget = pitchfromPosition + pitchfromControl;
        
        float targetYaw = transform.localPosition.x * positionYawFactor;

        float rollTarget = xThrow * controlRollFactor;
        
        //Use target angles to create a target Quaternion
        Quaternion rotationTarget = Quaternion.Euler(pitchTarget, targetYaw, rollTarget);

        //Linear interpolation to smooth out the rotation
        Quaternion smoothedTarget = Quaternion.Lerp(transform.localRotation, rotationTarget, lerpTValue);

        transform.localRotation = smoothedTarget;
    }

    private void HandleTranslation()
    {
        float xOffset = HandleHorizontalMovement();
        float yOffset = HandleVerticalMovement();
        transform.localPosition += new Vector3(xOffset, yOffset, 0);
    }

    private float HandleVerticalMovement()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yRawOffset = yThrow * Speed * Time.deltaTime;
        float yOffset;

        //Binds ship to moveBoundary
        if (Mathf.Abs(yRawOffset + transform.localPosition.y) >= yMoveBoundary) yOffset = 0;
        else yOffset = yRawOffset;
        return yOffset;
    }

    private float HandleHorizontalMovement()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xRawOffset = xThrow * Speed * Time.deltaTime;
        float xOffset;

        //Binds ship to screen.
        if (Mathf.Abs(xRawOffset + transform.localPosition.x) >= xMoveBoundary) xOffset = 0;
        else xOffset = xRawOffset;
        return xOffset;
    }

    private void OnPlayerDeath()            //Called by string reference
    {
        movementEnabled = false;
    }
}
