using Amazon.IoT.Model;
using Amazon.Util.Internal;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Vuforia;

public class UserTouchController : MonoBehaviour
{
    [SerializeField] private GameObject _room;
    [SerializeField] private GameObject _pointer;
    private bool _roomPlaced=false;
    //private Transform _roomPlacementData;
    //private PlaneFinderBehaviour _planeFinder;
    [SerializeField] private AnchorBehaviour _anchorBehaviour;
    //private ARAnchorManager _anchorManager;
    private World vuWorld;
    private IEnumerable a;
    private Vector3 _roomPosition;

    public void LockAnchorForInitialClick(HitTestResult result)
    {
        //Debug.Log(result);

        //Debug.Log(_anchorBehaviour.GetComponent("abc"));
        //_anchorBehaviour.ConfigureAnchor("abc", result);
        //Debug.Log("_anchorBehaviour");

        var listenerBehaviour = GetComponent<AnchorInputListenerBehaviour>();
        if (listenerBehaviour != null && _roomPlaced) {
            Vector3 pointerPosition = new Vector3 (result.Position.x, _roomPosition.y+ 0.01f, result.Position.z);
            GameObject pointer = Instantiate(_pointer, pointerPosition, result.Rotation, GameObject.FindGameObjectWithTag("Room").transform);
            pointer.tag = "Pointer";

        }
        else if ( _anchorBehaviour != null && !_roomPlaced) {
            GameObject room = Instantiate(_room, result.Position, result.Rotation);
            _roomPosition = result.Position;
            room.tag = "Room";
            _roomPlaced = true;
        }
            
        //if (listenerBehaviour != null)
        //{
        //    listenerBehaviour.enabled = false;
        //}
    }

    public void RemoveFloorFromParent(HitTestResult result)
    {
        var listenerBehaviour = GetComponent<AnchorInputListenerBehaviour>();

        //GameObject groundPlaneStage = GameObject.Find("Ground Plane Stage");
        //Debug.Log("room" + _room);
        //Debug.Log("_pointer" + _pointer);
        
        if (listenerBehaviour != null && _roomPlaced)
        {
            //_pointer.SetActive(true);
            //_room.transform.SetParent(null, true);
            //SetParentWithPosition(_room.transform, transform.root, _roomPosition.position, _roomPosition.rotation);
            //_pointer.transform.SetParent(groundPlaneStage.transform,true);
            
            
            
            //if (_room)
            //{
            //    var _myRoom = Instantiate(_room);
            //    _myRoom.transform.parent = _roomPlacementData;
            //    Destroy(_room);
            //}
            
            //SetObjectInsideParent(_room, "Ground Plane Stage");

            //Debug.Log("Staticroom place" + _room.transform);
        }
        else if(listenerBehaviour != null && !_roomPlaced)
        {
            //Debug.Log(result);
            //_anchorBehaviour.ConfigureAnchor("roomPosition",result.Position,result.Rotation);
            //a = vuWorld.GetObserverBehaviours();
            //Debug.Log(a + "ancchors");
            //AnchorBehaviour[] a = FindObjectsOfType<AnchorBehaviour>();
            //Debug.Log("a "+a);
            //foreach (var anchor in a)
            //{
            //    Debug.Log(anchor);
            //}
            //Debug.Log("a "+a);
            //Debug.Log("listner "+listenerBehaviour.transform);
            //Debug.Log("listner " + listenerBehaviour.transform.position);
            //Debug.Log("pointer "+_pointer.transform.position);
            //Instantiate(_pointer,_room.transform.position,_room.transform.rotation, GameObject.Find("Ground Plane Stage").transform);

            //Debug.Log("Placeddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
            //_pointer.SetActive(false);
            //_roomPlacementData = _room.transform;
            //var newObj = Instantiate(_pointer);

            //newObj.transform.parent = _roomPlacementData;
            //var newObj = Instantiate(_pointer);
            //newObj.transform.parent = listenerBehaviour.transform;
            //SetObjectInsideParent(_pointer, "Ground Plane Stage");
            //Destroy(_pointer);
            //_room.transform.SetParent(groundPlaneStage.transform,true);

            //SetParentWithPosition(_room.transform, groundPlaneStage.transform, _room.transform.position, _room.transform.rotation);
            //_roomPlaced =true;
            //_roomPlacementData = _room.transform;

        }


    }

    public void SetObjectInsideParent(GameObject obj, string destination)
    {
        var newObj = Instantiate(obj);
        newObj.transform.parent = GameObject.Find(destination).transform;
    }

    public void SetParentWithPosition(Transform obj,Transform parent , Vector3 position, Quaternion rotation)
    {
        obj.parent = parent;
        obj.position = position;
        obj.rotation = rotation;
        //obj.Translate(position);
    }


}
