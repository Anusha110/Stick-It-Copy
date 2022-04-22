using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVisibility : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        print("I became Invisible: " + gameObject.name);
    }

    private void OnBecameVisible()
    {
        print("I became Visible: " + gameObject.name);
    }
}
