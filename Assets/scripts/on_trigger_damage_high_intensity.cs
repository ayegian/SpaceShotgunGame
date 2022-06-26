using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class on_trigger_damage_high_intensity : MonoBehaviour
{
    public int damage;
    public bool destroyaftertime;
    public float destroytime;
    public bool destroyoncollide;
    public bool dontdestroyontag;
    public string dontdestroytag;
    public bool onlydestroyonwall;
    public float detect_rad;
    public Transform cast_point;
    public LayerMask mask;
    public ContactFilter2D contact_filter = new ContactFilter2D();
    // Start is called before the first frame update
    void Start()
    {
        if (destroyaftertime)
        {
            StartCoroutine(destroyobj());
        }
        contact_filter.useTriggers = !destroyoncollide;
        contact_filter.layerMask = mask;
        contact_filter.useLayerMask = true;
    }
    IEnumerator destroyobj()
    {
        yield return new WaitForSeconds(destroytime);
        Destroy(gameObject);
    }
    void Damage(Collider2D other)
    {
        print("DAMAGING: " + other.gameObject.name);
        if (other.gameObject.CompareTag("player"))
        {
            other.GetComponentInChildren<player>().health -= damage;
        }
        if (onlydestroyonwall)
        {
            if (other.CompareTag("wall"))
            {
                Destroy(gameObject);
            }
        }
        else if (other.isTrigger == false && destroyoncollide == true)
        {
            print("DESTROYER NAME: " + other.name);
            if (dontdestroyontag == false)
            {
                Destroy(gameObject);
            }
            else
            {
                if (other.gameObject.CompareTag(dontdestroytag) == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] hit_colls = Physics2D.OverlapCircleAll(cast_point.position, detect_rad, mask);
        foreach(Collider2D coll in hit_colls)
        {
            Damage(coll);
        }
        if(hit_colls.Length == 0)
        {
            print("HIGH INTENSITY NOTHING IN HITCOLLS");
        }
    }
}
