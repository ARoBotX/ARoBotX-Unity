using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UserTouchController : MonoBehaviour
{
   public void OnInteractiveHitTest(HitTestResult result)
    {
        Debug.Log("OnInteractiveHitTest Worked");
        var listenerBehaviour = GetComponent<AnchorInputListenerBehaviour>();
        Debug.Log("result"+result);
        if (listenerBehaviour != null)
        {
            listenerBehaviour.enabled = false;
        }
    }
}
