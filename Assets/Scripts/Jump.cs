using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody rigidbodyObj;
    private bool isCollided = false;
    private Vector3 trampolineOriginalPos;
    private Vector3 previousPos;

    private void Awake()
    {
        rigidbodyObj = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        previousPos = StickItGameManager.instance.Trampoline.transform.position;
        trampolineOriginalPos = StickItGameManager.instance.Trampoline.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.transform.parent.gameObject.tag == "Trampoline")
        { 
            trampolineOriginalPos = collision.gameObject.transform.position;
            isCollided = true;
        }
    }

    void Update()
    {

        if (Input.GetKey("space"))
        {
            StickItGameManager.instance.JumpTime += Time.deltaTime;

            StickItGameManager.instance.Trampoline.transform.position = new Vector3(StickItGameManager.instance.Trampoline.transform.position.x, StickItGameManager.instance.Trampoline.transform.position.y - 0.0005f, StickItGameManager.instance.Trampoline.transform.position.z);

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.0005f, gameObject.transform.position.z);
        }

        if (Input.GetKeyUp("space"))
        {
            //StartCoroutine(ReturnTrampolineToOriginalPos());
            makeBallJump();
        }

        updateAndCheckStickTime();

    }

    private void updateAndCheckStickTime()
    {
        if (isCollided)
        {
            StickItGameManager.instance.StickTime += Time.deltaTime;

            if (StickItGameManager.instance.StickTime >= 5)
            {
                StickItGameManager.instance.Lives -= 1;
                StickItGameManager.instance.deactivateLifeObj();
                StickItGameManager.instance.FallOutsideTrampoline = true;
                StickItGameManager.instance.StickTime = 0;

                Debug.Log("Lost a life - More than 5 sec");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        previousPos = trampolineOriginalPos;
    }

    IEnumerator ReturnTrampolineToOriginalPos()
    {
        yield return new WaitForSeconds(0.05f);
        StickItGameManager.instance.Trampoline.transform.position = previousPos;
    }

    private void makeBallJump()
    {
        rigidbodyObj.AddForce(((Vector3.up * 5f) + (Vector3.forward * 2.3f)) * StickItGameManager.instance.JumpTime, ForceMode.VelocityChange);

        StickItGameManager.instance.StickTime = 0;
        StickItGameManager.instance.JumpTime = 0;
        
        isCollided = false;
    }

}
