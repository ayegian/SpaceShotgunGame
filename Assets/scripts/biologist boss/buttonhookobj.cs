using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonhookobj : MonoBehaviour
{
    public Rigidbody thisrigid;
    public float speed;
    public float timeforward;
    public float timestill;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(buttonhookfor());
    }
    //MAKE BUTTONHOOK LIKE A NINJA STAR OR SOMETHING COOL
    public IEnumerator buttonhookfor()
    {
        thisrigid.velocity = thisrigid.transform.up* -1 * speed;
        yield return new WaitForSeconds(timeforward);
        thisrigid.velocity = Vector3.zero;
        StartCoroutine(buttonhookback());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            StartCoroutine(buttonhookback());
        }
    }
    public IEnumerator buttonhookback()
    {
        yield return new WaitForSeconds(timestill);
        thisrigid.velocity = thisrigid.transform.up * 1 * speed;
        yield return new WaitForSeconds(timeforward);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
