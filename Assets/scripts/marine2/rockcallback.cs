using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockcallback : MonoBehaviour
{
    public marine2ai marine;
    public Rigidbody thisrigid;
    public float returnspeed;
    public bool returning;
    public float mindisttoholder;
    public GameObject holder;
    public rockscript2 rockscript;
    // Start is called before the first frame update
    void Start()
    {
        if(marine == null)
        {
            marine = GameObject.FindObjectOfType<marine2ai>();
        }
        marine.rocks.Add(this.gameObject);
    }
    public void callback(GameObject holdera)
    {
        holder = holdera;
        returning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (returning)
        {
            print("RETURNING");
            this.transform.eulerAngles = new Vector3(90, Mathf.Atan2((holder.transform.position.x - this.transform.position.x), (holder.transform.position.z - this.transform.position.z)) * Mathf.Rad2Deg, 0);
            //thisrigid.velocity = thisrigid.transform.right * returnspeed;
            thisrigid.velocity = thisrigid.transform.up * returnspeed;
            if (Vector3.Distance(this.transform.position, holder.transform.position)<mindisttoholder)
            {
                print("MIN DIST ACHEIVED");
                thisrigid.velocity = Vector3.zero;
                this.transform.parent = holder.transform;
                this.transform.localPosition = Vector3.zero;
                holder.GetComponent<rockholder>().occupied = true;
                print("HOLDER OCCUPIED");
                holder.GetComponent<rockholder>().rock = this.gameObject;
                returning = false;
            }
        }
    }
    public void changeholder(bool a)
    {
        holder.GetComponent<rockholder>().occupied = a;
    }
    private void OnDestroy()
    {   
        if(holder != null)
        {
            holder.GetComponent<rockholder>().occupied = false;
            holder.GetComponent<rockholder>().rock = null;
        }
        if (marine.rocks.Contains(this.gameObject))
        {
            marine.rocks.Remove(this.gameObject);
        }
    }
}
