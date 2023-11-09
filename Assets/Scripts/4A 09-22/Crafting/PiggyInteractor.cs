using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class PiggyInteractor : MonoBehaviour
{
    public XRGrabInteractable piggyAsXR;
    public XRRayInteractor rHand;
    public XRRayInteractor lHand;

    public bool hasBeenShakenWithPine { get; set; } = false;

    /*
     * We want to avoid a force grab when the user will click on the pine to take it off
     * Piggy. But we still want him to be able to force grab the Piggy because it's more easy.
     * And since we can't have (or it's difficult to set up) two different Interactors for
     * the Hands, we need to turn off the ForceGrab boolean for the hand which doesn't hold
     * the Piggy.
     * We also set the boolean hasBeenShakenWithPine to true here, so the interaction layers and
     * force grab switch occur just once. The "First Select Entered" event is NOT the only
     * first time you grab an Object. It triggers everytime you grab an object after completely
     * ungrabbed it (so a function won't be called again if you just pass an object in the other hand)
     */
    public void unsetSecondHandForceGrab()
    {
        if(!hasBeenShakenWithPine)
        {
            if (rHand.IsSelecting(piggyAsXR))
            {
                lHand.GetComponent<XRRayInteractor>().useForceGrab = false;
                lHand.GetComponent<XRRayInteractor>().interactionLayers = InteractionLayerMask.GetMask("MoldPine");
            }
            else
            {
                rHand.GetComponent<XRRayInteractor>().useForceGrab = false;
                rHand.GetComponent<XRRayInteractor>().interactionLayers = InteractionLayerMask.GetMask("MoldPine");
            }
            hasBeenShakenWithPine = true;
        }
    }


    public void reverseAttachForLeftHand()
    {
        if (lHand.IsSelecting(piggyAsXR))
        {
            Vector3 angle = new Vector3(piggyAsXR.gameObject.GetComponent<Transform>().localEulerAngles.x,
                                        -piggyAsXR.gameObject.GetComponent<Transform>().localEulerAngles.y,
                                        piggyAsXR.gameObject.GetComponent<Transform>().localEulerAngles.z);
            piggyAsXR.gameObject.GetComponent<Transform>().localEulerAngles = angle;
        }
    }
}
