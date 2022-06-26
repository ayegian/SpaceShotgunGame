using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_mortar : enemyshoot2
{
    public GameObject dropobject;
    public float warmuptime;
    public float cooldowntime;
    public bool onplayer;
    public bool onself;
    public float minrange;
    public int nummortars = 1;
    public float betweentime = 0;
    public float randrange;
    public bool randangle;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
    }
    public IEnumerator Fire()
    {
        shooting = true;
        canrotate = false;
        yield return new WaitForSeconds(warmuptime);
        for(int i = 0; i<nummortars; i++)
        {
            shoot();
            yield return new WaitForSeconds(betweentime);
        }
        yield return new WaitForSeconds(cooldowntime);
        canrotate = true;
        shooting = false;
    }
    void shoot()
    {
        if (onplayer)
        {
            if (randangle)
            {
                Instantiate(dropobject, Player.transform.position + new Vector3(Random.Range(-randrange, randrange), 0, Random.Range(-randrange, randrange)), Quaternion.Euler(90, Random.Range(0, 360), 0));
            }
            else
            {
                Instantiate(dropobject, Player.transform.position + new Vector3(Random.Range(-randrange, randrange), 0, Random.Range(-randrange, randrange)), Quaternion.Euler(90,0,0));
            }
        }
        else if (onself)
        {
            if (randangle)
            {
                Instantiate(dropobject, this.transform.position + new Vector3(Random.Range(-randrange, -minrange) + Random.Range(minrange, randrange), 0, Random.Range(-randrange, -minrange) + Random.Range(minrange, randrange)), Quaternion.Euler(90, Random.Range(0, 360), 0));
            }
            else
            {
                Instantiate(dropobject, this.transform.position + new Vector3(Random.Range(-randrange, -minrange)+Random.Range(minrange, randrange), 0, Random.Range(-randrange, -minrange) + Random.Range(minrange, randrange)), Quaternion.Euler(90, 0, 0));
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shooting == false && AI.inrange == true)
        {
            StartCoroutine(Fire());
        }
    }
}
