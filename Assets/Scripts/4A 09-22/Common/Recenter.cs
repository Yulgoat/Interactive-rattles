using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{

    public GameObject pellet;
    public GameObject hand;

    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        rot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void recenter() {
        // this.transform.position = new Vector3(hand.transform.position.x+0.12f,this.transform.position.y,hand.transform.position.z+0.02f);
        // this.transform.rotation = rot;

        this.transform.position = new Vector3(0.00779999979f,0.0816000029f,-0.0502999984f);
        this.transform.rotation = rot;
    }
}
