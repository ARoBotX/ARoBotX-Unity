using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public static UnityEvent OnForwardClicked;
    [HideInInspector] public static UnityEvent OnLeftClicked;
    [HideInInspector] public static UnityEvent OnRightClicked;
    [HideInInspector] public static UnityEvent OnBackwardClicked;

    private GameObject _robot;

    private void OnDisable()
    {
        OnForwardClicked.RemoveListener(GoForward);
        OnLeftClicked.RemoveListener(GoLeft);
        OnRightClicked.RemoveListener(GoRight);
        OnBackwardClicked.RemoveListener(GoBackWard);
    }
    void FindRobot()
    {
        if (_robot == null)
        {
            _robot = GameObject.FindWithTag("Robot");
        }
        
    }

    void GoForward()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        robotAttributes.position += Vector3.forward * Time.deltaTime;
    }

    void GoLeft()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        robotAttributes.position += Vector3.left * Time.deltaTime;

    }

    void GoRight()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        robotAttributes.position += Vector3.right * Time.deltaTime;

    }

    void GoBackWard()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        robotAttributes.position += Vector3.back * Time.deltaTime;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (OnForwardClicked == null)
            OnForwardClicked = new UnityEvent();
        if (OnLeftClicked == null)
            OnLeftClicked = new UnityEvent();
        if (OnRightClicked == null)
            OnRightClicked = new UnityEvent();
        if (OnBackwardClicked == null)
            OnBackwardClicked = new UnityEvent();

        OnForwardClicked.AddListener(GoForward);
        OnLeftClicked.AddListener(GoLeft);
        OnRightClicked.AddListener(GoRight);
        OnBackwardClicked.AddListener(GoBackWard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
