using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash_sprite : MonoBehaviour
{
    public float betweenflash;
    public SpriteRenderer sprite;
    public float flashing;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startflash()
    {
        StartCoroutine(flash());
    }
    public void endflash()
    {
        StopAllCoroutines();
        sprite.enabled = true;
    }
    public IEnumerator flash()
    {
        while (true)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(betweenflash);
        }
        yield return new WaitForEndOfFrame();
    }
    public void changecolor(int r, int g, int b)
    {
        sprite.color = new Color(r, g, b);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
