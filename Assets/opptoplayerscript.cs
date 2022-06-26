using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opptoplayerscript : MonoBehaviour
{
    public GameObject toplayer;
    public float adjust;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(toplayer.transform.localEulerAngles.x, adjust+ -1 * toplayer.transform.localEulerAngles.y, toplayer.transform.localEulerAngles.z);
    }
}
