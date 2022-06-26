using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogue_system : MonoBehaviour
{
    public GameObject[] dialogues;
    public GameObject dialogue_canvas;
    int currdialogue = 0;
    public bool dialoguedone = false;
    public bool end = false;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject a in dialogues)
        {
            a.SetActive(false);
        }
        dialogues[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && currdialogue < dialogues.Length)
        {
            currdialogue += 1;

            foreach(GameObject a in dialogues)
            {
                a.SetActive(false);
            }
            if (currdialogue == dialogues.Length)
            {
                print("DIALOGUE DONE");
                dialoguedone = true;
                dialogue_canvas.SetActive(false);
            }
            else
            {
                dialogues[currdialogue].SetActive(true);
            }
        }
    }
}
