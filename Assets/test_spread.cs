using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_spread : MonoBehaviour
{
    public Transform shootpoint;
    public GameObject[] barfs;
    public float barfs_per_second;
    public float barf_warmup_time;
    public float barf_seconds;
    public float barf_angle_range;
    public float barf_speed_high;
    public float barf_speed_low;
    public float min_scale;
    public float max_scale;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(barf());
    }
    IEnumerator barf()
    {
        yield return new WaitForSeconds(barf_warmup_time);
        int barfs_done = 0;
        while(barfs_done < (barfs_per_second * barf_seconds)){
            GameObject barf = Instantiate(barfs[Random.Range(0, barfs.Length)], shootpoint.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, shootpoint.transform.eulerAngles.z - Random.Range(-1*barf_angle_range, barf_angle_range)));
            float scale = Random.Range(min_scale, max_scale);
            barf.transform.localScale = new Vector3(scale, scale, 1);
            barf.GetComponent<Rigidbody2D>().velocity = barf.transform.up*-1*Random.Range(barf_speed_low, barf_speed_high);

            barfs_done += 1;
            yield return new WaitForSeconds(1/barfs_per_second);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
