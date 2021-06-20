using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollControl : MonoBehaviour
{
    [SerializeField]
    Camera scrollCamera;
    public float scrollSpeed;
    public float offsetSpeed;
    public Vector2 offset;

    [SerializeField]
    Material scrollMaterial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            scrollCamera.transform.Translate(scrollSpeed * Vector3.right);
            scrollMaterial.SetTextureOffset("_NormalMap", offset);
            offset += Vector2.right * offsetSpeed * Time.deltaTime;
            if (offset.x > 10)
                offset -= Vector2.right * 10;
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            scrollCamera.transform.Translate(scrollSpeed * Vector3.left);
            scrollMaterial.SetTextureOffset("_NormalMap", offset);
            offset -= Vector2.right * offsetSpeed * Time.deltaTime;
            if (offset.x < -10)
                offset += Vector2.right * 10;
        }
    }
}
