using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer_script : MonoBehaviour
{
    public int wait_time;
    public int wait_timer;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void WaitVoid()
    {
        print("WAIT VOID");
        done = false;
        wait_timer = wait_time;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        print("WAIT IENUMERATOR"+Time.timeScale);
        for(int i = 0; i<wait_time; i++)
        {
            print("IN WAIT 1");
            yield return new WaitForSeconds(1);
            print("IN WAIT 2");
            wait_timer--;
        }
        print("WAIT IENUMERATOR DONE");
        done = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
