using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparence : MonoBehaviour{
    private float transparency = 0.0f; // alpha channel of transparency with 0 = total transparency and 1 non transparent
    private float timer = 0.0f; // Reset every 3 seconds after being shaken
    private  Vector3 oldEulerAngles; // Used to detect change in rotation
    private float baseTransparency; // Normal alpha of the rattle's texture
    private bool isTransparencySet = false;
    public float shakeThreshold = 0.1f; // In angles
    

    // Start is called before the first frame update
    void Start(){
        // Get the initial transparency of the texture
        baseTransparency = GetComponent<Renderer>().material.color.a;
    }

    // Update is called once per frame
    void Update(){
        //Increment timer if rattle is transparent
        if (isTransparencySet){
            timer += Time.deltaTime;
        }
        // Check if the object is shaken
        if (Mathf.Abs(oldEulerAngles.x - gameObject.GetComponent<Rigidbody>().rotation.eulerAngles.x) > shakeThreshold){
            // Does useless assignments a lot but don't have time to make better
            // Make the rattle transparent
            SetTransparency(transparency, true);
            isTransparencySet = true;
        }
        // Check if the object has stopped shaking and 3 seconds has passed
        else if (Mathf.Abs(oldEulerAngles.x - gameObject.GetComponent<Rigidbody>().rotation.eulerAngles.x)  < shakeThreshold
                && timer >= 3.0f){
            // Make the rattle non transparent and reset the timer
            SetTransparency(baseTransparency, false);
            timer = 0.0f;
            isTransparencySet = false;
        }
        oldEulerAngles = gameObject.GetComponent<Rigidbody>().rotation.eulerAngles;
    }

    // Set alpha component of the rattle and change it's rendering mode
    void SetTransparency(float alpha, bool transparent){
        Color color = GetComponent<Renderer>().material.color; // get the current color of the material
        color.a = alpha; // replace the alpha component of the color by new alpha
        GetComponent<Renderer>().material.color = color; // set the color of the texture with new color
        if (transparent){
            // if the object is set to be transparent, change the material rendering mode to be transparent
            StandardShaderUtils.ChangeRenderMode(GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Transparent);
        }
        else{
             // if the object is set to be non transparent, change the material rendering mode to be opaque
            StandardShaderUtils.ChangeRenderMode(GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Opaque);
        }
    }
}
