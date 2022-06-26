using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananascript : MonoBehaviour
{
    public Transform castpoint;
    public Rigidbody2D thisrigid;
    public float timebeforeturn;
    public float timeturning;
    public float timebetweenturns;
    public float turnangle;
    public bool beforeturn = true;
    public float bananaspeed;
    public Vector3 lastpos;
    public float startangle;
    public float radiusguess;
    // Start is called before the first frame update
    public void Init(float speed, float turntime, float beforetime)
    {
        beforeturn = true;
        bananaspeed = speed;
        timeturning = turntime;
        timebeforeturn = beforetime;
        startangle = this.transform.eulerAngles.z;
        lastpos = this.transform.position;
        thisrigid.velocity = this.transform.up * -1 * bananaspeed;
        radiusguess = (bananaspeed * timeturning) / Mathf.PI;
        StartCoroutine(waitbeforeturn(timebeforeturn, false));
    }
    public IEnumerator waitbeforeturn(float timemove, bool destroy)
    {
        yield return new WaitForSeconds(timemove);
        if(destroy == true)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(waitturn(timeturning, false));
        }
    }
    public IEnumerator waitturn(float timeforturn, bool turnval)
    {
        beforeturn = false;
        int direction = Random.Range(1, 3);
        if(direction == 2)
        {
            direction = -1;
            print("DIRECTION -1");
        }
        int amountofframesturn= (int)(timeforturn / Time.fixedDeltaTime);
        //print("P8");
        //yield return new WaitForEndOfFrame();
        //print("P9");
        //yield return new WaitForFixedUpdate();
        Vector2 startpos = this.transform.position;
        print("START OF TURN ANGLE: " + this.transform.eulerAngles + " AMOUNT OF FRAMES: "+amountofframesturn);
        //float radius = (timeforturn * bananaspeed*2) / (2 * Mathf.PI);
        //float oneside = bananaspeed * timebeforeturn;
        //float oneangle = Mathf.Rad2Deg*Mathf.Acos((radius * radius) / (2 * oneside * radius));/* arccos((c ^ 2) / (2ac)) = B*/
        //print("RADIUS: " + radius + " ANGLE NEEDED: "+oneangle);
        for (int i = 0; i < amountofframesturn; i++)
        {
            //print("TURN: "+amountofframesturn+" "+i);
            //print("P2");
            thisrigid.transform.eulerAngles = thisrigid.transform.eulerAngles + direction * (new Vector3(0, 0, 1) * (turnangle / timeturning) * Time.fixedDeltaTime);
            //print("P3");
            thisrigid.velocity = thisrigid.transform.up * -1 * bananaspeed;
            //print("P4");
            yield return new WaitForEndOfFrame();
            //print("P5");
        }
        float diameter = Vector2.Distance(startpos, this.transform.position);
        float sideb = Mathf.Sqrt((Mathf.Pow(diameter,2)) + (Mathf.Pow(bananaspeed * timebeforeturn,2)));
        float anglec = (Mathf.Asin(bananaspeed * timebeforeturn / sideb)*Mathf.Rad2Deg);
        float useangle = direction * (90 - anglec) + startangle + 180;
        print("DIAMETER: " + diameter);
        print("ANGLE C: " + anglec);
        print("USE ANGLE: " + useangle);
        //print("P6");
        //thisrigid.velocity = Vector3.zero;
        //print("P7");
        print("END OF TURN ANGLE:" + this.transform.eulerAngles);
        this.transform.eulerAngles = new Vector3(0, 0, useangle);
        thisrigid.velocity = thisrigid.transform.up * -1 * bananaspeed;
        //thisrigid.velocity = Vector3.zero;
        StartCoroutine(waitbeforeturn(sideb/bananaspeed, true));
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        Debug.DrawLine(lastpos, this.transform.position, Color.magenta, 10000000);
        //print("P1");
        lastpos = this.transform.position;
        //if (turning)
        //{
        //    thisrigid.transform.eulerAngles = thisrigid.transform.eulerAngles + (new Vector3(0, 0, 1)*(turnangle/timeturning)*Time.fixedDeltaTime);
        //    thisrigid.velocity = thisrigid.transform.up*-1 * thisrigid.velocity.magnitude;
        //}
        if (beforeturn == true && radiusguess != 0)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(castpoint.transform.position, castpoint.transform.up * -1, radiusguess);
            if (rayhit.collider != null)
            {
                if (rayhit.collider.CompareTag("wall"))
                {
                    StopAllCoroutines();
                    StartCoroutine(waitturn(timeturning, false));
                }
            }
        }

    }
}
