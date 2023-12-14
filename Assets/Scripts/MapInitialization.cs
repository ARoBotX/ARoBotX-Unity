using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MapInitialization : MonoBehaviour
{

    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private Transform _arCamera;
    [SerializeField] private PlaneFinderBehaviour _planeFinder;
    private GameObject _floor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Working");
        Debug.Log(Input.touchCount);
        if (Input.touchCount == 1) {
            StartCoroutine(PlaceMap());
            
        }
    }


    IEnumerator PlaceMap()
    {
        Debug.Log("Map Placement Working");
        Transform planeFinderAttributes = _planeFinder.PlaneIndicator.transform;
        Debug.Log(planeFinderAttributes.position);
        Transform arCameraAttributes = _arCamera.transform;
        Vector3 cameraToPlaneDirection = planeFinderAttributes.position - arCameraAttributes.position;
        cameraToPlaneDirection.y = 0;
        Quaternion cameraToPlaneQ = Quaternion.LookRotation(-cameraToPlaneDirection, Vector3.up);

        _floor = Instantiate(_floorPrefab, planeFinderAttributes.position, cameraToPlaneQ);
        yield return new WaitForSeconds(1);
    }
}
