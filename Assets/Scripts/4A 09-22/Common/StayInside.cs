using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInside : MonoBehaviour
{

    public GameObject container; 
    Vector3 initialPosition;
    float width = 0.3f;// = 0.04364f;
    float height = 0.45f;// = 0.0602f;
    float depth = 0.7f;// = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = container.transform.position - this.transform.position;
        //Debug.Log(string.Format("{0}, {1}, {2}", initialPosition, container.transform.position, this.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(string.Format("{0}, {1}, {2}", width, height, depth));
        //Debug.Log(string.Format("{0}, {1}, {2}", initialPosition, container.transform.position, this.transform.position));
            bool needRecenter = false;
            if (this.transform.position.x < container.transform.position.x - width / 2 || this.transform.position.x > container.transform.position.x + width / 2)
            {
                needRecenter = true;
                //Debug.Log(string.Format("recenter X: {0}; {1}", this.transform.position.x, container.transform.position.x - width / 2));
            }

            if (this.transform.position.y < container.transform.position.y - height / 2 || this.transform.position.y > container.transform.position.y + height / 2)
            {
                needRecenter = true;
                //Debug.Log(string.Format("recenter Y: {0}; {1}", this.transform.position.y, container.transform.position.y - height / 2));
            }

            if (this.transform.position.z < container.transform.position.z - depth / 2 || this.transform.position.z > container.transform.position.z + depth / 2)
            {
                needRecenter = true;
                //Debug.Log(string.Format("recenter Z: {0}; {1}", this.transform.position.z, container.transform.position.z - depth / 2));
            }

            if (needRecenter)
            {
                //Physics.IgnoreLayerCollision(6, 7); // Layer 6 = Hochet
                this.transform.position = container.transform.position + initialPosition;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //Debug.Log(string.Format("{0}, {1}, {2}", initialPosition, container.transform.position, this.transform.position));
                //Physics.IgnoreLayerCollision(6, 7, false);

            }

    }

}
