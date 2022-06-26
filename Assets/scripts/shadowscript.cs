using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowscript : MonoBehaviour
{
    public float timetoshadow;
    public bool randtime;
    public float timetoshadowup;
    public float timetoshadowdown;
    public GameObject shadowobject;
    public GameObject dropobject;
    // Start is called before the first frame update
    void Start()
    {
        shadowobject.SetActive(true);
        dropobject.SetActive(false);
        StartCoroutine(shadow());
    }
    public IEnumerator shadow()
    {
        if (randtime)
        {
            yield return new WaitForSeconds(Random.Range(timetoshadowdown, timetoshadowup));
        }
        else
        {
            yield return new WaitForSeconds(timetoshadow);
        }
        dropobject.SetActive(true);
        dropobject.transform.parent = null;
        print("DROP OBJECT: " + dropobject.name + " PARENT: " + dropobject.transform.parent);
        shadowobject.SetActive(false);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
