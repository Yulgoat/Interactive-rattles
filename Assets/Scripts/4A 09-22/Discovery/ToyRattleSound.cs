using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToyRattleSound : MonoBehaviour
{
    //public XRBaseInteractor interactor;
    AudioSource audioSource;
    private Rigidbody rb;
    private Vector3 prevPos;

    public XRGrabInteractable hochet;
    public XRRayInteractor rHand;
    public XRRayInteractor lHand;
    void Start()
    {
        prevPos = transform.position;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = ((transform.position - prevPos) / Time.deltaTime).magnitude;
        float minSpeedSound = 5.0f;
        if (speed > minSpeedSound)
        {
            Debug.Log(speed);
            float maxVolumeOffset = 2.0f;
            //scale 0.5-1.7 for sound volume
            audioSource.volume = Mathf.Min(minSpeedSound + maxVolumeOffset, speed) / (minSpeedSound + maxVolumeOffset);
            audioSource.Play();

            float minMagnitude = 5.5f;
            if (speed > minMagnitude)
            {
                //scale 0.1-0.6 for vibration intensity
                // https://forum.unity.com/threads/haptic-feedback-in-xr.1011787/
                float maxMagnitude = 8;
                float minIntensity = 0.1f;
                float maxIntensity = 0.6f;
                float intensity = Mathf.Min(maxMagnitude, speed - minMagnitude + minIntensity) / maxMagnitude * maxIntensity;
                //float intensity = 1;
                if (lHand.IsSelecting(hochet))
                {
                    ActionBasedController lInteract = lHand.GetComponent<ActionBasedController>();
                    lInteract.SendHapticImpulse(intensity, 0.1f);
                }
                if (rHand.IsSelecting(hochet))
                {
                    ActionBasedController rInteract = rHand.GetComponent<ActionBasedController>();
                    rInteract.SendHapticImpulse(intensity, 0.1f);
                }
            }
        }
        prevPos = transform.position;
        //if (GetComponent<XRGrabInteractable>().isSelected)
        //{
        //    Debug.Log(speed.magnitude);
            
        //}
    }
}
