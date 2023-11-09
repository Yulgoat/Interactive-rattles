using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;


public class PineInteractor : MonoBehaviour
{

    // Set to true by XR Event when FirstSelected
    public bool isShakedWithPine { get; set; }

    public bool pineIsBroken = false;
    public XRRayInteractor rHand;
    public XRRayInteractor lHand;


    public void OnJointBreak(float breakForce)
    {
        pineIsBroken = true;
        this.GetComponent<AudioSource>().Play();
    }

    public void ResetInteractorToForceGrab()
    {
        lHand.GetComponent<XRRayInteractor>().useForceGrab = true;
        lHand.GetComponent<XRRayInteractor>().interactionLayers = InteractionLayerMask.GetMask("Default");

        rHand.GetComponent<XRRayInteractor>().useForceGrab = true;
        rHand.GetComponent<XRRayInteractor>().interactionLayers = InteractionLayerMask.GetMask("Default");
    }


    public void BreakPine()
    {
        pineIsBroken = true;
        this.GetComponent<AudioSource>().Play();
    }
}
