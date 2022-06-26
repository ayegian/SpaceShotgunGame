using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class reset_boss : MonoBehaviour
{
    public int time_before_round;
    public GameObject boss_obj;
    public GameObject boss_obj_prefab;
    public GameObject this_obj;
    public GameObject music_obj;
    public GameObject[] exclude_objs;
    public List<GameObject> children_objs = new List<GameObject>();
    public GameObject canvas_obj;
    public GameObject lose_canvas;
    public TextMeshProUGUI text;
    public MonoBehaviour[] scripts;
    public int bossnum;
    // Start is called before the first frame update
    void Awake()
    {
        scripts = boss_obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        foreach(Transform child in transform)
        {
            children_objs.Add(child.gameObject);
        }
        getchildren(transform);
        difficulty difficultyscript = GameObject.FindWithTag("difficulty").GetComponent<difficulty>();
        if (difficultyscript.max_boss_num < bossnum)
        {
            difficultyscript.max_boss_num = bossnum;
        }
        difficultyscript.cur_boss_num = bossnum;
    }
    void getchildren(Transform trans)
    {
        foreach(Transform child in trans)
        {
            children_objs.Add(child.gameObject);
            getchildren(child);
        }
        return;
    }
    void Start()
    {
        
    }
    public void reset_void()
    {
        print("START RESET");
        StartCoroutine(reset());
    }
    public void destroy_boss_obj()
    {
        if (boss_obj)
        {
            Destroy(boss_obj);
        }
        else
        {
            print("NO BOSS OBJ 3");
        }
    }
    public IEnumerator reset()
    {
        
        print("RESET IENUMERATOR");
        if (!canvas_obj)
        {
            print("NO CANVAS OBJ");
        }
        else
        {
            print("YES CANVAS OBJ");
        }
        canvas_obj.SetActive(false);
        if (!boss_obj)
        {
            print("NO BOSS OBJ 1");
        }
        Vector3 boss_obj_pos = boss_obj.transform.position;
        if (boss_obj)
        {
            print("YES BOSS OBJ");
            Destroy(boss_obj);
        }
        else
        {
            print("NO BOSS OBJ 2");
        }
        GameObject [] objects = Object.FindObjectsOfType<GameObject>();
        GameObject diffobj = GameObject.FindWithTag("difficulty");
        foreach (GameObject obj in objects)
        {
            if (obj)
            {
                print("YES OBJ");
            }
            else
            {
                print("NO OBJ");
            }
            if (obj != this_obj && obj != diffobj && obj != music_obj)
            {
                bool delete = true;
                print("RESET START OBJ");
                if (exclude_objs != null)
                {
                    for (int i = 0; i < exclude_objs.Length; i++)
                    {
                        if (exclude_objs[i])
                        {
                            print("YES EXCLUDE");
                        }
                        else
                        {
                            print("NO EXCLUDE");
                        }
                        if (obj == exclude_objs[i])
                        {
                            delete = false;
                        }
                    }
                    if (children_objs.Contains(obj))
                    {
                        delete = false;
                    }
                }
                if (delete)
                {
                    if (obj)
                    {
                        print("YES OBJ");
                        Destroy(obj);
                    }
                    else
                    {
                        print("NO OBJ");
                    }
                }
                //Destroy(obj);
            }
        }
        canvas_obj.SetActive(true);
        print("RESET INSTANTATE BOSS OBJ");
        boss_obj = Instantiate(boss_obj_prefab, boss_obj_pos, Quaternion.identity);
        scripts = boss_obj.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        for (int i = time_before_round; i>0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        canvas_obj.SetActive(false);
        foreach (MonoBehaviour script in scripts)
        {
            if (script)
            {
                script.enabled = true;
            }
        }
    }    
    // Update is called once per frame
    void Update()
    {
        print("TIMESCALE: " + Time.timeScale);
    }
}
