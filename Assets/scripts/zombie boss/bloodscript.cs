using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodscript : MonoBehaviour
{
    public Vector3 herebeforedown;
    public float bloodspeed;
    public Rigidbody2D bloodrigid;
    public float timetodown;
    // Start is called before the first frame update

    void Start()
    {
        print("HERE BEFORE DOWN: " + herebeforedown+ " DEGREES: "+ Mathf.Atan2((herebeforedown.y - this.transform.position.y), (herebeforedown.x - this.transform.position.x)) * Mathf.Rad2Deg);
        this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((herebeforedown.y - this.transform.position.y), (herebeforedown.x - this.transform.position.x)) * Mathf.Rad2Deg);
        bloodrigid.velocity = this.transform.right * bloodspeed;
        timetodown = Vector2.Distance(herebeforedown, this.transform.position) / bloodspeed;
        print("TIME TO DOWN: " + timetodown);
        StartCoroutine(beforedown());
    }
    IEnumerator beforedown()
    {
        yield return new WaitForSeconds(timetodown);
        print("FINISHED DOWN");
        this.transform.eulerAngles = Vector3.zero;
        bloodrigid.velocity = this.transform.up * -1 * bloodspeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
