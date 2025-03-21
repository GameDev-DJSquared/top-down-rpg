using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Tweakables")]
    
    [SerializeField] Transform target;
    [SerializeField] float smoothing = 0.1f;
    [SerializeField] float yAxis = 0;
    [SerializeField] float zAxis = 0;

    [Header("Behavior Type")]
    //Purely for Editor to decide if scene offset should be included
    [SerializeField] bool includeOffset = false;
    [SerializeField] bool startFollowing = false;

    

    Vector3 offset;
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        if(includeOffset)
            offset = transform.position - target.position;
        else
            offset = Vector3.zero;

        //Z Offset should be const to make sure camera renders everything
        offset.z = -10;
    }


    void FixedUpdate()
    {
        //Cam doesn't move till you do
        //mayb delet this part
        if(!startFollowing)
        {
            if(InputManager.instance.GetMoveDir() == Vector2.zero)
            {
                return;
            } else
            {
                startFollowing = true;
            }
        }

        
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        //Good ol' Clamp
        //Vector3 clamp = transform.position;
        //clamp.y = Mathf.Clamp(clamp.y, yAxis, 10000);
        //transform.position = clamp;
        //transform.position.y = Mathf.Clamp(trab);

        /*
        cam.transparencySortMode = TransparencySortMode.CustomAxis;
        cam.transparencySortAxis = new Vector3(0, yAxis, zAxis);*/

    }
}
