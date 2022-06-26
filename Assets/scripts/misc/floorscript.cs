using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorscript : MonoBehaviour
{
    public int numpassengers;
    public int lowestfloor;
    public int highestfloor;
    List<int> list = new List<int>();
    public float numtests;
    public float numtrue;
    // Start is called before the first frame update
    void Start()
    {
        for(int j = 0; j < numtests; j++)
        {
            list = new List<int>();
            for (int i = 0; i < numpassengers; i++)
            {
                list.Add(Random.Range(lowestfloor, highestfloor + 1));
            }
            for(int i = lowestfloor; i<highestfloor+1; i++)
            {
                //print("check Floornum: " + i);
                int count = 0;
                foreach(int f in list)
                {
                    if(f == i)
                    {
                        count++;
                    }
                }
                if(count >= 2)
                {
                    numtrue++;
                    break;
                }
            }
        }
        print("PERCENT TRUE: " + numtrue / numtests);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
