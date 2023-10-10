using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using Vuforia;

public class RobotController : MonoBehaviour
{
    [SerializeField] private GameObject _robotPrefab;
    [SerializeField] private PlaneFinderBehaviour _planeFinder;
    [SerializeField] private Transform _arCamera;
    [SerializeField] private GameObject _groundPrefab;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _zOffset;

    [SerializeField] private bool _testingModeOn;
    private GameObject _tempCreatedRobot;

    private bool _isCreated = false;
    private GameObject _ground;
    private float realLength = 200f;
    private float realBreadth = 300f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (!_isCreated) 
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Working1");
                _isCreated = true;
                CreateMapRobotSetup();
                
            }
            
        }

        if (_testingModeOn && _isCreated)
        {
            Vector3 robotVirtualPosition = ActualPositionsMapper(0, 0);
            Debug.Log(robotVirtualPosition);
            Transform robotSpawnPoint = _ground.transform.Find("RobotSpwanPoint");
            _tempCreatedRobot.transform.position = robotVirtualPosition;
            _tempCreatedRobot.transform.rotation = robotSpawnPoint.rotation;
            Debug.Log("Testing mode on");
        }
#endif
    }

    private void CreateMapRobotSetup() 
    {
        StartCoroutine(InitialRobotPlacement(0,0));
    }


    private void InstantiateGround()
    {
        Transform planeFinderAttributes = _planeFinder.PlaneIndicator.transform;
        Transform arCameraAttributes = _arCamera.transform;
        Vector3 cameraRobotLookingDirection = planeFinderAttributes.position - arCameraAttributes.position;
        cameraRobotLookingDirection.y = 0;
        Quaternion cameraRobotLookingDirectionQ = Quaternion.LookRotation(-cameraRobotLookingDirection, Vector3.up);
        _ground = Instantiate(_groundPrefab, planeFinderAttributes.position, cameraRobotLookingDirectionQ);
    }

    private void InstantiateRobot(Vector3 robotVirtualPosition)
    {
        Transform robotSpawnPoint = _ground.transform.Find("RobotSpwanPoint");
        if (_testingModeOn)
        {
            _tempCreatedRobot = Instantiate(_robotPrefab, robotVirtualPosition, robotSpawnPoint.rotation);
        }
        else
        {
            GameObject robot = Instantiate(_robotPrefab, robotVirtualPosition, robotSpawnPoint.rotation);
        }
    }
    private IEnumerator InitialRobotPlacement(float robotActualPositionX, float robotActualPositionY) 
    {
        
        InstantiateGround();
        Vector3 robotVirtualPosition = ActualPositionsMapper(robotActualPositionX, robotActualPositionY);
        InstantiateRobot(robotVirtualPosition);
        //GameObject robotModelClone = Instantiate(_robotPrefab, planeAttributes.position, cameraRobotLookingDirectionQ); 
        yield return new WaitForSeconds(1);
    }

    // Maps X,Y coodinates like this
    // X => X to Virtual X , there is no real height to virtual constant Y , Y to Virtual Z
    private Vector3 ActualPositionsMapper(float robotActualPositionX, float robotActualPositionY)
    {
        MeshRenderer robotPrefabMesh = _robotPrefab.GetComponent<MeshRenderer>();
        float lengthScale = 10 / realLength;
        float breadthScale = 10 / realBreadth;
        Transform robotSpawnPoint = _ground.transform.Find("RobotSpwanPoint");
        if (!_testingModeOn)
        {
            _xOffset = 2.5f;
            _zOffset = 2.5f;
        }
        Vector3 robotVirtualPosition = new Vector3(robotActualPositionX * lengthScale - _xOffset, robotSpawnPoint.position.y + robotPrefabMesh.bounds.size.y / 2, robotActualPositionY * breadthScale - _zOffset);
        return robotVirtualPosition;
    }
}
