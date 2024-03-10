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
    [SerializeField] private AnchorBehaviour _anchorBehaviour;
    private World vuWorld;
    private IEnumerable a;
    private Vector3 _roomPosition;
    public static Transform nextPointerPlacement;

    public void LockAnchorForInitialClick(HitTestResult result)
    {

        var listenerBehaviour = GetComponent<AnchorInputListenerBehaviour>();
        if (listenerBehaviour != null && _roomPlaced) {
            Vector3 pointerPosition = new Vector3 (result.Position.x, _roomPosition.y+ 0.01f, result.Position.z);
            GameObject pointer = Instantiate(_pointer, pointerPosition, result.Rotation, GameObject.FindGameObjectWithTag("Room").transform);
            nextPointerPlacement = pointer.transform;
            pointer.tag = "Pointer";

        }
        else if ( _anchorBehaviour != null && !_roomPlaced) {
            Vector3 roomPosition = new Vector3(result.Position.x+0.1f, result.Position.y, result.Position.z-0.11f);
            GameObject room = Instantiate(_room, roomPosition, result.Rotation);
            _roomPosition = result.Position;
            room.tag = "Room";
            _roomPlaced = true;
        }
            
    }
}
