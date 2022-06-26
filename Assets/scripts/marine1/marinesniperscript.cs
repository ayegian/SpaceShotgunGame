using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marinesniperscript : MonoBehaviour
{
    public player Player;
    public Rigidbody thisrigid;
    public float movespeed;
    public float shootrange;
    public float beforeshoottime;
    public float aftershoottime;
    public float shootobjectactivetime;
    public GameObject shootobject;
    public bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<player>();
    }
    IEnumerator shootrifle()
    {
        shooting = true;
        thisrigid.velocity = Vector3.zero;
        float movetemp = movespeed;
        movespeed = 0;
        yield return new WaitForSeconds(beforeshoottime);
        shootobject.SetActive(true);
        yield return new WaitForSeconds(shootobjectactivetime);
        shootobject.SetActive(false);
        yield return new WaitForSeconds(aftershoottime-shootobjectactivetime);
        movespeed = movetemp;
        shooting = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(shooting == false && Vector3.Distance(this.transform.position, Player.transform.position)<= shootrange)
        {
            StartCoroutine(shootrifle());
        }
        thisrigid.velocity = Vector3.Normalize(Player.transform.position - this.transform.position) * movespeed;
    }
}
