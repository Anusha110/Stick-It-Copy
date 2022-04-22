using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StickItGameManager.instance.FallOutsideTrampoline = true;
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        StickItGameManager.instance.Lives -= 1;
        StickItGameManager.instance.deactivateLifeObj();
        Debug.Log("Lost a life Floor");
    }
}
