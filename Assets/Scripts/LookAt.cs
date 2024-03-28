
using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{

    Transform target;
    public float speed = 10.0f;

    void Update()
    {

        if (GameObject.FindWithTag("tosser"))
        {
            target = GameObject.FindWithTag("tosser").transform;
            if (target)
            {
                Vector3 vectorToTarget = target.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
            }

        }


    }
}