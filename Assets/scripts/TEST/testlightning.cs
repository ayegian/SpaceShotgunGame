using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testlightning : MonoBehaviour
{
    public Transform endpoint;
    public GameObject lightning;
    public int recursivenum;
    public int maxrecnum;
    public int anglerange;
    public int numbranches;
    public int recnumstopbranches;
    public float scalemax;
    public float scalemin;
    public SpriteRenderer warningsprite;
    public SpriteRenderer lightningsprite;
    public float warningwaittime;
    public float warningspriteactivetime;
    public float beforedestroy;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void makelightning(int recnum, int up, int numbranchesnow)
    {
        recursivenum = recnum;
        if (recnum < maxrecnum)
        {
            if(recnum >= recnumstopbranches)
            {
                numbranches = 1;
                print("NUM BRANCHES: " + numbranches);
            }
            int inum = Random.Range(0, 2);
            for(int i = 0; i < numbranches; i++)
            {
                print("BRANCH NUM: " + i);
                float randangle = 0;
                if (up == 0)
                {
                    randangle = Random.Range(-anglerange, 0);
                    //randangle = anglerange * -1;
                }
                if (up == 1)
                {
                    randangle = Random.Range(0, anglerange);
                    //randangle = anglerange;
                }
                if (up == 2)
                {
                    if(i == 0)
                    {
                        randangle = anglerange;
                        print("RANDANGLE up: " + randangle);
                    }
                    else
                    {
                        randangle = -anglerange;
                        print("RANDANGLE DONW: " + randangle);
                    }
                }
                //randangle = Random.Range(-anglerange, 0);
                GameObject lightninginstan = Instantiate(lightning, endpoint.position/* + new Vector3(0, recnum*15, 0)*/, Quaternion.Euler(0, this.transform.eulerAngles.y,0));
                print("RAND ANGLE: " + randangle);
                lightninginstan.transform.eulerAngles += new Vector3(0, /*Random.Range(-anglerange, anglerange)*/ randangle, 0);
                lightninginstan.transform.localScale = new Vector3(Random.Range(scalemin, scalemax), 1, 1);
                lightninginstan.GetComponent<testlightning>().makelightning(recnum + 1, (i+inum)%2, 1 + numbranches);
                //lightninginstan.GetComponent<testlightning>().makelightning(recnum + 1, (i+inum)%2, 1 + numbranches);
            }
        }
        print("WARNING WAIT");
        StartCoroutine(warningwait());
    }
    public IEnumerator warningwait()
    {
        warningsprite.enabled = true;
        yield return new WaitForSeconds(warningspriteactivetime);
        warningsprite.enabled = true;
        yield return new WaitForSeconds(warningwaittime-warningspriteactivetime);
        warningsprite.enabled = false;
        lightningsprite.enabled = true;
        yield return new WaitForSeconds(beforedestroy);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //For testing
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    makelightning(0, 2, 0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    if(recursivenum != 0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
