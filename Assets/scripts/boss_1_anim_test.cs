using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_1_anim_test : MonoBehaviour
{
    public Animator boss_anim;
    public Animator left_arm_anim;
    public Animator right_arm_anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void buttons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            boss_anim.SetBool("idle", true);
            left_arm_anim.SetBool("idle", true);
            right_arm_anim.SetBool("idle", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            boss_anim.SetBool("flamethrower", true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            boss_anim.SetBool("idle", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(hurtanim());
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            boss_anim.SetBool("flamethrower", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { }
    }
    IEnumerator hurtanim()
    {
        boss_anim.Play("Hurt", -1, 0);
        AnimatorStateInfo info = boss_anim.GetNextAnimatorStateInfo(0);
        while (true)
        {
            bool b = info.normalizedTime >= 1f;
            print("IS PLAYING: \n"+ b+" NORM TIME: "+info.normalizedTime + " NAME: "+info.IsName("Hurt"));
            yield return new WaitForEndOfFrame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        buttons();
    }
}
