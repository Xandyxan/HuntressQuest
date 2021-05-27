using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShot : MonoBehaviour
{
    public SphereCollider sphereCollider;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Untagged"))
        {
            sphereCollider.enabled = false;
        }
    }
}
