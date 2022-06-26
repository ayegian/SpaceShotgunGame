using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_continuous : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject spawn_obj;
    public float spawntime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }
    public IEnumerator spawn()
    {
        while (true)
        {
            if (spawntime != 0)
            {
                yield return new WaitForSeconds(spawntime);
                Instantiate(spawn_obj, spawnpoint.position, spawnpoint.rotation);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
