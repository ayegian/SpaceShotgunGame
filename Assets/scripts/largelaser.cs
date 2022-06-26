using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class largelaser : MonoBehaviour {
    public BoxCollider2D box;
    GameObject beam;
    SpriteRenderer sprite;
    public float delaytime;
    public float timealive;
    Rigidbody2D thisrigid;
	// Use this for initialization
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        box = this.gameObject.GetComponent<BoxCollider2D>();
        box.enabled = false;
        StartCoroutine(attack());
    }
    IEnumerator attack()
    {
        print("attack");
        yield return new WaitForSeconds(delaytime);
        box.enabled = true;
        sprite.color = Color.green;
        yield return new WaitForSeconds(timealive);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
