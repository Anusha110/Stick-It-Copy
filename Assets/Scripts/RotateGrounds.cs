using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrounds : MonoBehaviour
{

    [SerializeField] GameObject[] grounds;
    [SerializeField] GameObject mainCamera;
    Vector3 lastGroundPosition;

    void Awake()
    {
        lastGroundPosition = grounds[grounds.Length - 1].transform.position;
    }

    void Update()
    {
        foreach (GameObject groundObj in grounds)
        {
            if (!CheckIfGroundIsVisible(groundObj))
            {
                groundObj.transform.position = new Vector3(groundObj.transform.position.x, groundObj.transform.position.y, lastGroundPosition.z - 0.5f);
            }
        }
    }

    bool CheckIfGroundIsVisible(GameObject ground)
    {
        Vector3 screenPoint = mainCamera.GetComponent<Camera>().WorldToViewportPoint(ground.transform.position);
        if (screenPoint.z >= -1.8)
            return true; 
        return false;
    }

}
