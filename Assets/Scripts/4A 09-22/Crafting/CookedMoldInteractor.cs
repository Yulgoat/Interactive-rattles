using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMoldInteractor : MonoBehaviour
{
    public bool hasBeenRipedOff { get; set; } = false;

    public void disapearMoldAnimation()
    {
        //TODO: Do something after getting riped off the Piggy ??
    }

    public void OnJointBreak(float breakForce)
    {
        hasBeenRipedOff = true;
        Debug.Log("TODO : Depop the Mold");
    }
}
