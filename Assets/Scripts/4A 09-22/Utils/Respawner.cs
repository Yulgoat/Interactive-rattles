using UnityEngine;
using System.Collections;

/*
 * To be the more generic possible, you need to specify which GameObject you're taking the coordinates from
 * and the Rigidbody you're working on. Be careful that when you're grabbing something with OpenXR, if there is 
 * a parent for the Object containing the XRGrabInteractable script, it will be riped off its parent and the locality
 * of the coordinates change ! EXCEPT if you set to true the Retain Transform Parent boolean on the XRGrabInteractable script
 * Don't forget to link the methods to the StartEntered and StartExited events of XRGrabInteractable
 * (refer to the spoon for an example)
 */
public class Respawner : MonoBehaviour
{
    Vector3 initialPosition;
    Quaternion initialRotation;

    public GameObject referenceObject;
    public Rigidbody rb;

    public bool isGrabbed { get; set; } = false;
    public float timeBeforeRespawn = 2f;  // In seconds

    private void Start()
    {
        initialPosition = this.transform.localPosition;
        initialRotation = this.transform.localRotation;
    }

        
    // Because we can't call a function with coroutines from OpenXR events
    public void StartRespawnLogic()
    {
        StartCoroutine(Respawn());
    }


    private IEnumerator Respawn()
    {
        isGrabbed = false;
        yield return new WaitForSecondsRealtime(timeBeforeRespawn); // We give 2s (default) to the person to grab the item back just in case

        if (!isGrabbed)
        {
            // Setting back the object to the right reference, because OpenXR brokes everything
            this.transform.parent = referenceObject.transform;

            // We need to absolutely freeze the object movement, velocity set to zero isn't enough (the object is still moving slowly)
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            this.transform.localPosition = initialPosition;
            this.transform.localRotation = initialRotation;

            rb.isKinematic = false;
        }
    }

    public void setIsGrabbedToFalse()
    {
        isGrabbed = false;
    }
}
