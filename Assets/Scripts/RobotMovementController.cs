using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementController : MonoBehaviour
{
    private UserTouchController userController;
    private Transform _nextPointerPlacement;
    [SerializeField] private float _speedScale;
    [SerializeField] private float _movementSpeed;
    private GameObject _room;
    private Transform _robotPlacement;
    //[SerializeField] private float _robotSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        _nextPointerPlacement = UserTouchController.nextPointerPlacement;
        if (_nextPointerPlacement != null) 
        {
            _room = GameObject.FindGameObjectWithTag("Room");
            Debug.Log("_room " + _room);
            _robotPlacement = _room.GetComponentsInChildren<Transform>()[1];
            //Debug.Log("_robot " + _robot);
            //Debug.Log("userController " + userController);
            //Debug.Log("_nextPointerPlacement " + _nextPointerPlacement);
            if (_robotPlacement != null)
            {
                _robotPlacement.position = Vector3.LerpUnclamped(_robotPlacement.position, _nextPointerPlacement.position , _movementSpeed * _speedScale * Time.deltaTime);
                //_robotPlacement.Translate(Vector3.forward * (1/3600) *Time.deltaTime);
                _robotPlacement.LookAt(_nextPointerPlacement.position);
            }

        }


    }
}
