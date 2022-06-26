using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationtest : MonoBehaviour
{
    public Rigidbody2D thisrigid;
    public float speed;
    public float rottime;
    public int amountofframesrot;
    public float rotangle;
    public bool turning;
    Vector3 lastpos;
    // Start is called before the first frame update
    void Start()
    {
        amountofframesrot = (int)(rottime / Time.fixedDeltaTime);
        lastpos = this.transform.position;
    }
    public void buttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopAllCoroutines();
            turning = false;
            this.transform.position = Vector3.zero;
            thisrigid.velocity = Vector3.zero;
            this.transform.eulerAngles = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("THIS AMOUNT ROT EACH FRAME: " + (rotangle / rottime) * Time.fixedDeltaTime);
            thisrigid.velocity = thisrigid.transform.up * -1 * speed;
            StartCoroutine(waittime());
        }
    }
    IEnumerator waittime()
    {
        print("START OF TURN ANGLE: " + this.transform.eulerAngles);
        turning = true;
        for(int i = 0; i<amountofframesrot; i++)
        {
            Debug.DrawLine(lastpos, this.transform.position, Color.magenta, 10000000);
            lastpos = this.transform.position;
            thisrigid.transform.eulerAngles = thisrigid.transform.eulerAngles + (new Vector3(0, 0, 1) * (rotangle / rottime) * Time.fixedDeltaTime);
            thisrigid.velocity = thisrigid.transform.up * -1 * thisrigid.velocity.magnitude;
            yield return new WaitForEndOfFrame();
        }
        thisrigid.velocity = Vector3.zero;
        turning = false;
        print("END OF TURN ANGLE:" + this.transform.eulerAngles);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        buttons();
        //if (turning)
        //{
        //    Debug.DrawLine(lastpos, this.transform.position, Color.magenta, 10000000);
        //    lastpos = this.transform.position;
        //    thisrigid.transform.eulerAngles = thisrigid.transform.eulerAngles + (new Vector3(0, 0, 1) * (rotangle / rottime) * Time.fixedDeltaTime);
        //    thisrigid.velocity = thisrigid.transform.up * -1 * thisrigid.velocity.magnitude;
        //}
        if(!turning)
        {
            thisrigid.velocity = Vector3.zero;
        }
    }
}
