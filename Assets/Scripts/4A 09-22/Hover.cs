using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Vector3 upDelta;
    public Vector3 downDelta;
    public float moveDuration;
    public float breathingTime;
    public GameObject target;

    private float lastLoop;
    private int where_last = 0; // 0: original, 1: up, 2: up-breathing, 3: down, 4: down-breathing
    private float angular = 0;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastLoop = Time.time;
        originalPosition = target.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.angular += (Time.deltaTime * 2 * Mathf.PI / moveDuration);
        var sinconj = Mathf.Sin(this.angular);
        this.target.transform.localPosition = this.originalPosition + Mathf.Clamp(sinconj, -1, 0) * this.downDelta + Mathf.Clamp(sinconj, 0, 1) * this.upDelta;
    }
}
