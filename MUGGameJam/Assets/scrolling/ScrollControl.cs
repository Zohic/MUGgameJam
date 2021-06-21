using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollControl : MonoBehaviour
{
    [SerializeField]
    Camera scrollCamera;
    public float scrollSpeed;
    public float offsetCoof;
    public Vector2 offset;

    [SerializeField]
    Material scrollMaterial;

    public bool canScroll;
    public bool handWorking = false;
    public PlayerControl player;

    public ChankSpawner spawner;

    public float camWidth;

    void Start()
    {
        
    }

    
    public void Translate(bool left, float speed)
    {
        if(left)
        {
            if (scrollCamera.transform.position.x + camWidth/2 + scrollSpeed * speed * Time.deltaTime >= spawner.rightChunk.rightEnd.position.x)
                return;

            scrollCamera.transform.Translate(scrollSpeed * speed * Vector3.right);
            scrollMaterial.SetTextureOffset("_NormalMap", offset);

            offset += Vector2.right * offsetCoof * speed * Time.deltaTime;
            if (offset.x > 10)
                offset -= Vector2.right * 10;
        }else
        {

            if (scrollCamera.transform.position.x - camWidth/2 - scrollSpeed * speed * Time.deltaTime <= spawner.leftChunk.leftEnd.position.x)
                return;

            scrollCamera.transform.Translate(scrollSpeed * speed * Vector3.left);
            scrollMaterial.SetTextureOffset("_NormalMap", offset);
            offset -= Vector2.right * offsetCoof * speed * Time.deltaTime;
            if (offset.x < -10)
                offset += Vector2.right * 10;
        }
        
    }

    void Update()
    {
        if (canScroll)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                Translate(true, 1);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                Translate(false, 1);
            }
        }else
        {
            if(!handWorking)
            {
                float dist = player.transform.position.x - scrollCamera.transform.position.x;
                if(Mathf.Abs(dist) > 800)
                    Translate((dist > 0), Mathf.Abs(dist) / 3000);
            }
        }

        

        
    }
}
