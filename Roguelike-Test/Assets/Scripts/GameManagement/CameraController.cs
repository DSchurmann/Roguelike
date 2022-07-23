using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool targetPlayer = true;
    //coords to show whole map for testing
    private Vector3 pos = new Vector3(30, 56, 30);
    private Vector3 targetRot = new Vector3(50, 0, 0);
    private GameObject target;

    private void LateUpdate()
    {
        if(targetPlayer)
        {
            Vector3 newPos = target.transform.position;
            newPos.y = 7f;
            newPos.z -= 5f;
            transform.position = newPos;
            transform.eulerAngles = targetRot;
        }
        else
        {
            transform.position = pos;
            transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }

    public void SetTarget(GameObject t) => target = t;
}
