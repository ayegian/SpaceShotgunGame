using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardscript : MonoBehaviour { 

    public List<int> uselist = new List<int>();
    public List<int> preflist = new List<int>();
    public int numtimestest;
    public int numtimestrue;
    // Start is called before the first frame update
    void Start()
    {
        //Making prefab list
        for(int i = 0; i < 52; i++)
        {
            preflist.Add(i);
        }
        for(int i = 0; i < numtimestest; i++)
        {
            //reset list
            uselist  = new List<int>();
            uselist.AddRange(preflist);
            //2 draws
            for(int j = 0; j < 2; j++)
            {
                int randnum = Random.Range(0, uselist.Count);
                //Numbers are 0-51, so I made it mod 4, where 0 = club, 1 = diamond, and rest dont matter
                //This checks to see if the first one mod 4 is 0, and if
                if (uselist[randnum]%4 == j)
                {
                    numtimestrue++;
                    break;
                }
                uselist.RemoveAt(randnum);
            }
        }
        float top = numtimestrue;
        float bot = numtimestest;
        print("PROBABILITY: " + (top / bot) * 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
