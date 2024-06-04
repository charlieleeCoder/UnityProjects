using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // May need to tweak in editor
    public Vector3 cameraOffset = new Vector3(0f, 1.2f, -2.6f);
    
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Late update is called afterwards
    void LateUpdate()
    {
        if (_target != null)
        {
            this.transform.position = _target.TransformPoint(cameraOffset);

            this.transform.LookAt(_target);
        }
    }
}
