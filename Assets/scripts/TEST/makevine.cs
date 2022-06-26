using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makevine : MonoBehaviour
{
    public GameObject prefab;
    public GameObject vine;
    public float numvines;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= numvines; i++) {
            Instantiate(vine, new Vector3(1.2f * i, 0, 0), Quaternion.Euler(0,0,0), prefab.transform);
        }            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
