using System;
using UnityEngine;

public class zdepth : MonoBehaviour
{
    Transform _transform;
    [SerializeField]  bool stationary = true;
    private void Start()
    {
       _transform = transform; 
    }

    private void LateUpdate()
    {
       Vector3  position = transform.position;
       position.z = position.y * 0.0001f;
       transform.position = position;
       if (stationary)
       {
           Destroy(this);
       }
    }
}
