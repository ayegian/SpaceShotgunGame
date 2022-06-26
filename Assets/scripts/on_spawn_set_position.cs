using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class on_spawn_set_position : MonoBehaviour
{
    public bool allow_collider_overlap;
    public Vector3 bot_left_bounds;
    public Vector3 top_right_bounds;
    public CircleCollider2D test_col;
    // Start is called before the first frame update
    void Start()
    {
        setpos();
    }
    void setpos()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(bot_left_bounds.x + 1.5f, top_right_bounds.x - 1.5f), UnityEngine.Random.Range(bot_left_bounds.y + 1.5f, top_right_bounds.y - 1.5f), 0);
        Collider[] hitColliders = Physics.OverlapSphere(pos, test_col.radius);
        while (hitColliders.Length > 0)
        {
            pos = new Vector3(UnityEngine.Random.Range(bot_left_bounds.x + 1.5f, top_right_bounds.x - 1.5f), UnityEngine.Random.Range(bot_left_bounds.y + 1.5f, top_right_bounds.y - 1.5f), 0);
            hitColliders = Physics.OverlapSphere(pos, test_col.radius);
        }
        this.transform.position = pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
