using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceprojectile : MonoBehaviour
{
    public Rigidbody2D thisrigid;
    public float lifetime;
    public float speed;
    public float damage = 1;
    public float bounceangle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyobj());
        thisrigid.velocity = this.transform.right * speed;
        print("BOUNCE SET VELO: "+thisrigid.velocity);
    }
    IEnumerator destroyobj()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<player>().health -= damage;
            Destroy(gameObject);
        }
        else
        {
            //this.transform.Rotate(new Vector3(0, 0, 180));
            //var varneeded = this.transform.position - collision.gameObject.transform.position;
            //var lookrot = Quaternion.LookRotation(varneeded, Vector3.forward);
            //print("LOOKROT: " + lookrot+ " EULERS: "+lookrot.eulerAngles);
            //print("VAR NEEDED: " + varneeded);
            //thisrigid.velocity = this.transform.right * speed;
            StartCoroutine(changeangle());
        }
    }
    IEnumerator changeangle()
    {
        yield return new WaitForEndOfFrame();
        float signangle = Vector2.SignedAngle(Vector2.right, thisrigid.velocity.normalized);
        //print("CHANGE DIR " + this.transform.eulerAngles+" RIGID ROT: "+thisrigid.transform.eulerAngles+" VELO: "+ thisrigid.velocity+" DIR MOVING: "+Vector2.SignedAngle(Vector2.right, thisrigid.velocity.normalized));
        print("SIGN ANGLE: " + signangle);
        print("THIS ANGLE BEFORE: " + this.transform.eulerAngles);
        this.transform.eulerAngles = new Vector3(0, 0, signangle);
        print("this angle after: " + this.transform.eulerAngles);
        this.transform.eulerAngles += new Vector3(0, 0, Random.Range(-bounceangle, bounceangle));
        thisrigid.velocity = this.transform.right * speed;
        yield return new WaitForEndOfFrame();
        print("this angle after after: " + this.transform.eulerAngles);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
