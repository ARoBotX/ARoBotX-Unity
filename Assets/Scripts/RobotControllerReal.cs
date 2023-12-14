using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RobotControllerReal : MonoBehaviour
{
    Vector3 mapPosition;
    Quaternion mapRotation;

    [SerializeField] private PlaneFinderBehaviour _planeFinder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject roomPrefab = GameObject.Find("Ground Plane Stage/Room/floor");

        
    }
}
