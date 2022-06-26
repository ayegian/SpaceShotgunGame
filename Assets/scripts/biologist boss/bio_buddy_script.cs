using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bio_buddy_script : MonoBehaviour
{
    public bool canshoot = true;
    public bool shooting;
    public GameObject shootobj;
    public float shoot_rps;
    public Transform shootpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void buttons()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            startshooting(10);
        }
    }
    public void startshooting(float shoottime0)
    {
        StartCoroutine(enable_shooting(shoottime0));
    }
    public IEnumerator enable_shooting(float shoottime)
    {
        shooting = true;
        yield return new WaitForSeconds(shoottime);
        shooting = false;
    }
    public IEnumerator shoot()
    {
        canshoot = false;
        yield return new WaitForSeconds(1 / shoot_rps);
        Instantiate(shootobj, shootpoint.transform.position, shootpoint.transform.rotation);
        canshoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        buttons();
        if (shooting && canshoot)
        {
            StartCoroutine(shoot());
        }
    }
}
