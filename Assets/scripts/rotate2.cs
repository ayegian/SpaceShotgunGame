using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate2 : MonoBehaviour
{
    private Vector3 diff;
    private player Player;
    // Use this for initialization
    void Awake()
    {
        Player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        diff.Normalize();

        float rot_y = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(90f, rot_y-90, 0);

    }
}

