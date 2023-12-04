using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using Unity.VisualScripting.Antlr3.Runtime;

public class RobotController : MonoBehaviour
{
    public static UnityEvent OnForwardClicked;
    [HideInInspector] public static UnityEvent OnLeftClicked;
    [HideInInspector] public static UnityEvent OnRightClicked;
    [HideInInspector] public static UnityEvent OnBackwardClicked;

    private GameObject _robot;
    private float realLength = 200f;
    private float realBreadth = 300f;
    private Vector3 robotMovingPosition;
    private Vector3 robotMovingDirection;

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
    
    void AWSIotInitialization(){
 
          const string IotEndpoint = "a3apm3sagumwi6-ats.iot.ap-south-1.amazonaws.com";
          const int BrokerPort = 8883;
            var clientCert = new X509Certificate2("D:/Projects/Unity Projects/ARoBotX-Unity/AWS_secrets/pfx_certificate.pfx", "12345678");
   
            var caCert = X509Certificate.CreateFromSignedFile("D:/Projects/Unity Projects/ARoBotX-Unity/AWS_secrets/AmazonRootCA1.pem"); 

            var client = new MqttClient(IotEndpoint, BrokerPort, true, caCert, clientCert, MqttSslProtocols.TLSv1_2 );

          
            client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;


            client.Connect("94a63b36-70e3-11ee-b962-0242ac120002");

            client.Subscribe(new[] { "topic_1" }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE }); 
     }

    public class CoordinatesData
    {
        public float[] coordinates;
    }
    public void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Debug.Log("We received a message...");
            string encodedCoordinates = Encoding.UTF8.GetString(e.Message);
            CoordinatesData coordinate = JsonUtility.FromJson<CoordinatesData>(encodedCoordinates);
            if (coordinate != null && coordinate.coordinates.Length == 2)
            {
            Vector3 realTimeCoordinates = new Vector3(coordinate.coordinates[0],0, coordinate.coordinates[1]);
            if (e.Message == null)
            {
                robotMovingDirection = new Vector3(0, 0, 0);
            }
            if (_robot != null)
            {
                MoveToPosition(realTimeCoordinates);
            }
            
            // Now you can use the 'x' and 'y' variables as needed.
        }
        
        }

    void GoForward()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        Vector3 robotButtonMoveDirection = new Vector3(0, 0, -1);
        robotAttributes.Translate(Time.deltaTime * robotButtonMoveDirection);
        Debug.Log("Forward");
    }

    Vector3 ActualPositionMapper(Vector3 rawPositionData)
    {
        float lengthScale = 1 / realLength;
        float breadthScale = 1 / realBreadth;
        return new Vector3(rawPositionData.x * lengthScale, 0, rawPositionData.z * breadthScale);
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        
        targetPosition = ActualPositionMapper(targetPosition);
        robotMovingDirection = FindMovingDirection(targetPosition);

    }

    Vector3 FindMovingDirection(Vector3 targetPosition)
    {
        Vector3 direction = robotMovingPosition - targetPosition;
        return direction;
    }

    void GoLeft()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        Vector3 robotButtonMoveDirection = new Vector3(1, 0, 0);
        robotAttributes.Translate(Time.deltaTime * robotButtonMoveDirection);
        Debug.Log("Left");
    }

    void GoRight()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        Vector3 robotButtonMoveDirection = new Vector3(-1, 0, 0);
        robotAttributes.Translate(Time.deltaTime * robotButtonMoveDirection);

    }

    void GoBackWard()
    {
        FindRobot();
        Transform robotAttributes = _robot.transform;
        Vector3 robotButtonMoveDirection = new Vector3(0, 0, 1);
        robotAttributes.Translate(Time.deltaTime * robotButtonMoveDirection);
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
        AWSIotInitialization();
    }

    // Update is called once per frame
    void Update()
    {
        FindRobot();
        if (_robot != null)
        {
            Transform robotAttributes = _robot.transform;
            robotAttributes.Translate(Time.deltaTime * robotMovingDirection);
        }
        
    }
}
