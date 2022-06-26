using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritemasktest : MonoBehaviour {
    SpriteMask mask;
    Vector3 scale;
    float change;
    Vector3 position;
    public GameObject shotgunblast;
	// Use this for initialization
	void Awake () {
        change = .3f;
        mask = gameObject.GetComponent<SpriteMask>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            scale = new Vector3(mask.transform.localScale.x, collision.transform.position.y - mask.transform.position.y, mask.transform.localScale.z);
            position = new Vector3(mask.transform.position.x, (mask.transform.position.y + (scale.y/2)), mask.transform.position.z);
            mask.transform.localScale = scale;
        }
        
    }

    // Update is called once per frame
    void Update () {
        this.transform.position = shotgunblast.transform.position;
	}
}
