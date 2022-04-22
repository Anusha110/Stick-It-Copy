using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrampolineBase : MonoBehaviour
{

    [SerializeField] private GameObject groundsWithStickyBall;
    [SerializeField] private GameObject[] trampolines;
    [SerializeField] private GameObject stickyBall;

    private Vector3 destinationVector;
    float initialHeight;


    private void Start()
    {
        initialHeight = stickyBall.transform.position.y;
    }

    private GameObject previousTrampoline;

    private void Update()
    {
        if (StickItGameManager.instance.FallOutsideTrampoline)
        {
            RestartLevel();
        }
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = groundsWithStickyBall.transform.position;
        while (time < duration)
        {
            groundsWithStickyBall.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        groundsWithStickyBall.transform.position = targetPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        if (gameObject.transform.parent.gameObject != StickItGameManager.instance.Trampoline)
        {
            previousTrampoline = StickItGameManager.instance.Trampoline;
            StickItGameManager.instance.Trampoline = gameObject.transform.parent.gameObject;
            destinationVector = new Vector3(groundsWithStickyBall.transform.position.x, groundsWithStickyBall.transform.position.y, groundsWithStickyBall.transform.position.z - 1f);

            StartCoroutine(LerpPosition(destinationVector, 0.2f));

            if (CheckIfTrampolineWasSkipped())
            {
                StickItGameManager.instance.Lives -= 1;
                StickItGameManager.instance.deactivateLifeObj();
                StickItGameManager.instance.FallOutsideTrampoline = true;
                Debug.Log("Lost a life Skipped");
            }
            else
            {
                StickItGameManager.instance.Score += 5;
                StickItGameManager.instance.updateScoreText();
            }
        }
    }

    private bool CheckIfTrampolineWasSkipped()
    {
        int trampolineIndex = 0, prevTrampolineIndex = -1, currentTrampolineIndex = 0;

        foreach (GameObject trampoline in trampolines)
        {
            if(trampoline == previousTrampoline)
            {
                prevTrampolineIndex = trampolineIndex;
            } else if (trampoline == StickItGameManager.instance.Trampoline)
                {
                currentTrampolineIndex = trampolineIndex;
            }
            trampolineIndex += 1;
        }

        return ((currentTrampolineIndex - prevTrampolineIndex) != 1);
    }

    private void RestartLevel()

    {
        Debug.Log("Restart Level");

        stickyBall.GetComponent<Rigidbody>().isKinematic = false;

        StickItGameManager.instance.Level -= 1;
        StickItGameManager.instance.TimeLeft += 2;
        StickItGameManager.instance.restartTimer();

        //StopCoroutine(StickItGameManager.instance.updateTimer);
        //StickItGameManager.instance.updateTimer = StartCoroutine(StickItGameManager.instance.UpdateTimer());

        GameObject desiredStartingTrampoline;
        int trampolineIndex = 0;

        foreach (GameObject trampoline in trampolines)
        {
            
            if (trampoline.gameObject == StickItGameManager.instance.LevelFirstTrampoline)
            {
                break;
            }

            trampolineIndex += 1;

        }

        if(trampolineIndex != 0)
        {
            desiredStartingTrampoline = trampolines[trampolineIndex - 1];
        } else
        {
            desiredStartingTrampoline = trampolines[trampolineIndex];
        }
        
        destinationVector = new Vector3(StickItGameManager.instance.GroundPosition.x, StickItGameManager.instance.GroundPosition.y, StickItGameManager.instance.GroundPosition.z - 1f);

        StartCoroutine(LerpPosition(destinationVector, 1.5f));

        StickItGameManager.instance.FallOutsideTrampoline = false;

        stickyBall.transform.position = new Vector3(StickItGameManager.instance.LevelFirstTrampoline.transform.position.x,initialHeight, StickItGameManager.instance.LevelFirstTrampoline.transform.position.z);

        StickItGameManager.instance.Trampoline = StickItGameManager.instance.LevelFirstTrampoline;
    }

}








//int trampolineIndex = 0;
//GameObject trampolines = gameObject.transform.parent.gameObject.transform.parent.gameObject;

//Debug.Log("Trampolines Obj: " + trampolines.name + "gameObject: " + gameObject);


//foreach(Transform trampoline in trampolines.transform)
//{
//    trampolineIndex += 1;

//    if (trampoline.gameObject == StickItGameManager.instance.Trampoline)
//    {
//        break;
//    }

//}

//destinationVector = new Vector3(groundsWithStickyBall.transform.position.x, groundsWithStickyBall.transform.position.y, groundsWithStickyBall.transform.position.z + (float) trampolineIndex - 1f);

//Debug.Log("trampolineIndex - 1:" + (trampolineIndex - 1f));


//void Update()
//{

//    Debug.Log("StickItGameManager.instance.CollisionCount: " + StickItGameManager.instance.CollisionCount);

//    if (StickItGameManager.instance.CollisionCount > 2)
//    {
//        ground.transform.position = Vector3.Lerp(groundPos, destinationVector, 0.5f);
//        groundPos = ground.transform.position;
//        Debug.Log("Ground Pos: " + groundPos + " ground.transform.position: " + ground.transform.position);

//        Debug.Log("ground.transform.position: " + ground.transform.position + " destinationVector: " + destinationVector);

//        if(ground.transform.position == destinationVector)
//        {
//            print("REACHED DESTINATION VECTOR");
//            groundPos = ground.transform.position;
//        }
//    }  

//}

//Debug.Log("StickItGameManager.instance.Trampoline.name: " + StickItGameManager.instance.Trampoline);

//foreach (Transform child in trampolineTransform)
//{
//    if (child.name == "nearCameraBasePoint")
//    {
//        Debug.Log("Came into here");
//        awayFromCameraBasePoint = child.transform.position.z;

//    }

//}