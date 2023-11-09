using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PineSocketHandler : MonoBehaviour
{
    public GameObject parentMold;
    public XRSocketInteractor socket;
    public XRGrabInteractable pineRef;

    public bool isPineSet = false;


    public void CheckIfEnteredObjectIsPine()
    {
        if(socket.IsSelecting(pineRef))
        {
            pineRef.transform.SetParent(parentMold.transform);
            pineRef.GetComponent<CapsuleCollider>().enabled = false;
            isPineSet = true;

            // Need to wait a little bit before desactivating the socket, otherwise the Pine won't be placed
            // at the right place with the attach transform
            // Update : why did I want to remove the socket ???
            //StartCoroutine(WaitBeforeDesactivateSocket());
        }
    }

    private IEnumerator WaitBeforeDesactivateSocket()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        DesactivateSocket();
    }
    public void DesactivateSocket()
    {
        socket.gameObject.SetActive(false);
    }
}
