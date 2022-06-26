using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallmover : MonoBehaviour
{
    public marine2ai marineai;
    public Rigidbody thisrigid;
    public float movespeed;
    public float delaybeforemove;
    public float delaybeforedestroy;
    public float mindist;
    public Transform target;
    public Transform target2;
    public bool moving;
    public Transform movetrans;
    public Transform[] wallmovepositions;
    public int currentindex;
    public bool destroynext;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startmove(int a, Transform[] positions)
    {
        wallmovepositions = positions;
        currentindex = a;
        target = wallmovepositions[a].transform;
        StartCoroutine(move());
        StartCoroutine(changedestroybool());
    }
    public IEnumerator move()
    {
        yield return new WaitForSeconds(delaybeforemove);
        moving = true;
    }
    public IEnumerator changedestroybool()
    {
        print("DESTROY NEXT START");
        yield return new WaitForSeconds(delaybeforedestroy);
        print("DESTROYING NEXT");
        Destroy(gameObject);
        //marineai.walls.Remove(gameObject);
        destroynext = true;
    }
    public IEnumerator destroythis()
    {
        yield return new WaitForSeconds(delaybeforemove);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            thisrigid.velocity = movespeed * movetrans.transform.right;
            print("DISTANCE: " + Vector3.Distance(this.transform.position, target.position));
            if (Vector3.Distance(this.transform.position, target.position)<= mindist)
            {
                this.transform.position = target.position;
                thisrigid.velocity = Vector3.zero;
                moving = false;
                movetrans.eulerAngles -= new Vector3(0, 90, 0);
                int a = currentindex + 1;
                if (a == wallmovepositions.Length)
                {
                    a = 0;
                }
                currentindex = a;
                target = wallmovepositions[a].transform;
                StartCoroutine(move());
            }
        }
        if(moving == false)
        {
            thisrigid.velocity = Vector3.zero;
            if(destroynext == true)
            {
                destroynext = false;
                StartCoroutine(destroythis());
            }
        }
    }
}
