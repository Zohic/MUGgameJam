using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollControl : MonoBehaviour
{
    [SerializeField]
    Camera scrollCamera;
    public float scrollSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            scrollCamera.transform.Translate(scrollSpeed * Vector3.right);
        }else if(Input.mouseScrollDelta.y < 0)
        {
            scrollCamera.transform.Translate(scrollSpeed * Vector3.left);
        }
    }
}
