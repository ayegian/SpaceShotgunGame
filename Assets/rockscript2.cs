using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockscript2 : MonoBehaviour
{
    public GameObject smallrock;
    public float smallrockspeed;
    public int degreesbetweenrocks;
    public int damage = 1;
    public bool canexplode;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<player>().health -= damage;
            //collision.gameObject.GetComponent<player>().invincible = true;
        }
        if (!collision.gameObject.CompareTag("boss") && !collision.gameObject.CompareTag("rock")&&collision.isTrigger == false)
        {
            print("ROCK DESTROYER: " + collision.gameObject.tag+" NAME: "+collision.name);
            explode();
        }
    }
    public void timetoexplodevoid(float b)
    {
        StartCoroutine(timetoexplode(b));
    }
    public IEnumerator timetoexplode(float a)
    {
        yield return new WaitForSeconds(a);
        explode();
    }
    public void explode()
    {
        print("EXPLODE");
        for (int i = 0; i < (360 / degreesbetweenrocks); i++)
        {
            GameObject rockinstan = Instantiate(smallrock, this.transform.position, Quaternion.Euler(90, degreesbetweenrocks * i, 0));
            rockinstan.GetComponent<Rigidbody>().velocity = rockinstan.transform.right * smallrockspeed;
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
