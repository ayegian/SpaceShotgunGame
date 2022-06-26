using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class reset_script : MonoBehaviour
{
    public List<GameObject> exclude_objs = new List<GameObject>();
    public GameObject boss_prefab;
    public GameObject boss_obj;
    public GameObject boss_obj_2;
    public MonoBehaviour[] boss_scripts;
    public timer_script timer;
    public bool started = true;
    public Transform[] all_objs;
    public int boss_num;
    public player Player;
    public GameObject dead_menu;
    public Vector3 obj_pos;
    public float death_wait_time;
    public bool player_dead = false;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<difficulty_script>().cur_boss < boss_num)
        {
            FindObjectOfType<difficulty_script>().cur_boss = boss_num;
        }
        GetChildren();
        ResetVoid();
        print("RESET START");
    }
    void GetChildren()
    {
        print("GET CHILDREN");
        exclude_objs.Add(FindObjectOfType<difficulty_script>().gameObject);
        exclude_objs.Add(this.gameObject);
        GetChildrenRecursive(this.transform);
    }
    void GetChildrenRecursive(Transform trans)
    {
        foreach (Transform child in trans)
        {
            print("ADD CHILD");
            exclude_objs.Add(child.gameObject);
            GetChildrenRecursive(child);
        }
    }
    void DisableScripts()
    {
        boss_scripts = boss_obj.GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour script in boss_scripts){
            script.enabled = false;
        }
   }    
    void EnableScripts()
    {
        boss_scripts = boss_obj.GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour script in boss_scripts){
            script.enabled = true;
        }
    }
    public void DestroyAll()
    {
        if (boss_obj)
        {
            obj_pos = boss_obj.transform.position;
            Destroy(boss_obj);
            all_objs = FindObjectsOfType<Transform>();
            foreach (Transform tranny in all_objs)
            {
                if (!(exclude_objs.Contains(tranny.gameObject)))
                {
                    Destroy(tranny.gameObject);
                }
            }
        }
    }
    public void ResetVoid()
    {
        print("RESettiNG");
        if (boss_obj)
        {
            print("THERE IS BOSS OBJ");
            obj_pos = boss_obj.transform.position;
            Destroy(boss_obj);
            all_objs = FindObjectsOfType<Transform>();
            foreach (Transform tranny in all_objs)
            {
                if (!(exclude_objs.Contains(tranny.gameObject)))
                {
                    Destroy(tranny.gameObject);
                }
            }
        }
        else
        {
            print("THERE IS NO BOSS OBJ");
        }
        boss_obj = Instantiate(boss_prefab, obj_pos, Quaternion.identity);
        player_dead = false;
        if (boss_obj == null)
        {
            print("BOSS OBJ STILL NULL");
        }
        else
        {
            print("BOSS OBJ NAME: " + boss_obj.name);
        }
        //print("TIME SCALE ZERO");
        //Time.timeScale = 0;
        print("TIMER VOID RESET OBJ");
        timer.WaitVoid();
        DisableScripts();
        started = false;
        Player = FindObjectOfType<player>();
    }
    public void StartGame()
    {
        started = true;
        EnableScripts();
    }
    public IEnumerator OnPlayerDeath()
    {
        yield return new WaitForSeconds(death_wait_time);
        DestroyAll();
        dead_menu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(started == false)
        {
            if (timer.done)
            {
                StartGame();
            }
        }
        if(Player.lastknownhealth == 0&&player_dead==false)
        {
            //TRY TO MAKE IT SO DELAY

            StartCoroutine(OnPlayerDeath());
            player_dead = true;
            //DestroyAll();
            //dead_menu.SetActive(true);
        }
    }
}
