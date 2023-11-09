using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Samples.ControllerSample;

public class SoundTrigger : MonoBehaviour
{
    AudioSource audioSource;

    public XRGrabInteractable hochet;
    public XRRayInteractor rHand;
    public XRRayInteractor lHand;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Component[] components = GetComponents<Component>();
        //for (int i = 0; i < components.Length; i++)
        //{
        //    Debug.Log(components[i].GetType());
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.relativeVelocity.magnitude > 0.5f)
        {
            //scale 0.5-1.7 for sound volume
            audioSource.volume = Mathf.Min(1.7f, collision.relativeVelocity.magnitude) / 1.7f;
            audioSource.Play();

            if (collision.relativeVelocity.magnitude > 1f)
            {
                //scale 0.1-0.6 for vibration intensity
                // https://forum.unity.com/threads/haptic-feedback-in-xr.1011787/
                float maxMagnitude = 2;
                float minIntensity = 0.1f;
                float maxIntensity = 0.6f;
                float intensity = Mathf.Min(maxMagnitude, collision.relativeVelocity.magnitude - 1 + minIntensity) / maxMagnitude * maxIntensity;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
