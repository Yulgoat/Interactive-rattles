using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MovingCraftingObjects : MonoBehaviour
{
    public XRRayInteractor rHand;
    public XRRayInteractor lHand;

    public GameObject childMoldWithClay;

    public bool goToGoulag { get; set; } = false; // we need to expose a set method even if the variable is public for OpenXR

    public void switchingMolds(GameObject mold_A, GameObject mold_B)
    {
        Vector3 tempPosition = mold_A.transform.position;
        Quaternion tempRotation = mold_A.transform.rotation;
        mold_A.transform.SetPositionAndRotation(mold_B.transform.position, mold_B.transform.rotation);
        mold_B.transform.SetPositionAndRotation(tempPosition, tempRotation);
    }

    public void rotateMoldFromAnotherMold(GameObject exampleMold, GameObject moldToBeRotated)
    {
        moldToBeRotated.transform.localRotation = exampleMold.transform.localRotation;
        //moldToBeRotated.transform.Rotate(exampleMold.transform.rotation.eulerAngles);
    }

    //TODO: Enable the right BoxCollider to be able to grap the gattaiMold
    // May be useless now
    public void moldGattai(XRGrabInteractable mold_A_with_clay, XRGrabInteractable mold_B_with_clay)
    {
        // We want to move the Prefab and not the mold we are actually grabbing
        // And the prefab is the direct parent
        if (rHand.IsSelecting(mold_A_with_clay) || lHand.IsSelecting(mold_A_with_clay)) {
            mold_A_with_clay.GetComponent<BoxCollider>().enabled = false;
            mold_A_with_clay.transform.parent.SetParent(mold_B_with_clay.transform);
            mold_A_with_clay.transform.parent.localPosition = new Vector3(0, 0, 0);
            mold_A_with_clay.transform.parent.Rotate(0, 180, 180);

        } else if (rHand.IsSelecting(mold_B_with_clay) || lHand.IsSelecting(mold_B_with_clay)) {
            mold_B_with_clay.GetComponent<BoxCollider>().enabled = false;
            mold_B_with_clay.transform.parent.SetParent(mold_A_with_clay.transform);
            mold_B_with_clay.transform.parent.localPosition = new Vector3(0, 0, 0);
            mold_B_with_clay.transform.parent.Rotate(0, 180, 180);

        } else { // Temporary thing to be able to proceed when using the XR Device Simulator, since we move things directly in the Scene
            // The mold_B will be the reference
            mold_A_with_clay.GetComponent<BoxCollider>().enabled = false;
            mold_A_with_clay.transform.SetParent(mold_B_with_clay.transform);
            mold_A_with_clay.transform.localPosition = new Vector3(0, 0, 0);
            mold_A_with_clay.transform.Rotate(0, 180, 180);
            mold_A_with_clay.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            mold_B_with_clay.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void moldGattaiMK2(GameObject otherMold)
    {
        otherMold.transform.SetParent(this.childMoldWithClay.transform);
        otherMold.transform.localPosition = new Vector3();
        otherMold.transform.localEulerAngles = new Vector3(0, 180, 180);
    }
}
