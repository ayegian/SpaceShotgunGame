using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class cutscene_manager : MonoBehaviour
{
    public PlayableDirector director;
    public Camera camera;
    public GameObject maincamobj;
    public MonoBehaviour[] scripts;
    public GameObject boss;
    public TimelineAsset timeline;
    public Animation animation;
    public reset_boss reset;
    public dialogue_system dialogue;
  
    // Start is called before the first frame update
    void Awake()
    {
        scripts = boss.GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
    IEnumerator cutscenestart()
    {
        //.enabled = false;
        if (timeline)
        {
            camera.gameObject.SetActive(true);
            maincamobj.SetActive(false);
            print("TIMELINE DURATION: " + timeline.duration);
            director.Play();
            yield return new WaitForSeconds((float)timeline.duration);
            //Camera.main.enabled = true;
        }
        //if (animation)
        //{
        //    camera.gameObject.SetActive(true);
        //    maincamobj.SetActive(false);
        //    print("Animation DURATION: " + animation.clip.length);
        //    animation.Play(animation.clip.name, PlayMode.StopSameLayer);
        //    yield return new WaitForSeconds((float)animation.clip.length);
        //    //Camera.main.enabled = true;
        //}
        print("RESET CAMERAS");
        camera.gameObject.SetActive(false);
        maincamobj.SetActive(true);
        //foreach (MonoBehaviour script in scripts)
        //{
        //    script.enabled = true;
        //}
        reset.reset_void();
    }
    IEnumerator cutsceneend()
    {
        if (timeline)
        {
            print("DO END CUTSCENE");
            director.Play();
            yield return new WaitForSeconds((float)timeline.duration);
            Destroy(boss);
            yield return new WaitForSeconds(1);
            Scene curscene = SceneManager.GetActiveScene();
            print("CUTSCENE LOAD NEW SCENE CURRENT SCENE: "+ curscene.buildIndex);
            SceneManager.LoadScene(curscene.buildIndex + 1);

            //NEW SCENE
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (dialogue.dialoguedone)
        {
            print("DIALOGUE DONE");
            if (dialogue.end)
            {
                print("DIALOGUE END");
                StartCoroutine(cutsceneend());
            }
            else
            {
                StartCoroutine(cutscenestart());
            }
        }
    }
}
