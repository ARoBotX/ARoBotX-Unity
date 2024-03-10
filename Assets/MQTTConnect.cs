using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using UnityEngine.Networking;
public class MQTTConnect : MonoBehaviour
{
    private UserTouchController userController;
    private Transform _nextPointerPlacement;
    [SerializeField] private float _speedScale;
    [SerializeField] private float _movementSpeed;
    private GameObject _room;
    private Transform _robotPlacement;

    private MqttClient client;
    private String debugMsg;
    //[SerializeField] private float _robotSpeed;

    // Start is called before the first frame update
    void Start()
    {


        try
        {
            string iotEndpoint = "a1cm3c34iajtv7-ats.iot.us-east-1.amazonaws.com";
            int brokerPort = 8883;

            string clientCertLoc = Application.streamingAssetsPath + "/" + "pfx_certificate.pfx";
            string caCertLoc = Application.streamingAssetsPath + "/" + "rootCA.pem";


            UnityWebRequest www = UnityWebRequest.Get(caCertLoc);
            UnityWebRequest www2 = UnityWebRequest.Get(clientCertLoc);
            www.SendWebRequest();
            while (!www.isDone) { }

            if (www.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log("Error loading rootCA");
            }


            www2.SendWebRequest();
            while (!www2.isDone) { }

            if (www2.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log("Error loading certificate.pfx");
            }
            else
            {

                //Debug.Log("Started fetching .pem");
                byte[] cacertData = www.downloadHandler.data;
                X509Certificate cacert = new X509Certificate(cacertData);

                //Debug.Log(".pem fetched!");


                //Debug.Log("Started fetching .pfx");

                byte[] clientCertData = www2.downloadHandler.data;

                var clientCert = new X509Certificate2(clientCertData, "1234");

                //Debug.Log(".pfx fetched");

                client = new MqttClient(iotEndpoint, brokerPort, true,
                                                   cacert,
                                                   clientCert,
                                                   MqttSslProtocols.TLSv1_2);




                client.Connect("pamuditha");
                client.MqttMsgPublishReceived += ClientMqttMsgPublishReceived;
                string topic = "wheelChair/position";
                client.Subscribe(new string[] { topic }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                if (client.IsConnected)
                {
                    Debug.Log("Client Connected!");
                }


            }

        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }
    }



    void Update()
    {
        Debug.Log("Update "+debugMsg);
        _nextPointerPlacement = UserTouchController.nextPointerPlacement;
        //Debug.Log(debugMsg);
        if (_nextPointerPlacement != null)
        {
            _room = GameObject.FindGameObjectWithTag("Room");
            _robotPlacement = _room.GetComponentsInChildren<Transform>()[1];
            //Debug.Log("_robot " + _robot);
            //Debug.Log("userController " + userController);
            //Debug.Log("_nextPointerPlacement " + _nextPointerPlacement);
            if (_robotPlacement != null)
            {
                _robotPlacement.position = Vector3.LerpUnclamped(_robotPlacement.position, _nextPointerPlacement.position, _movementSpeed * _speedScale * Time.deltaTime);
                //_robotPlacement.Translate(Vector3.forward * (1/3600) *Time.deltaTime);
                _robotPlacement.LookAt(_nextPointerPlacement.position);
            }
        }
    }

    private void ClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        //Debug.Log("Message received: " + Encoding.UTF8.GetString(e.Message));
        debugMsg = Encoding.UTF8.GetString(e.Message);
    }

    private static void KeepConsoleAppRunning(Action onShutdown)
    {
        //   manualResetEvent = new ManualResetEvent(false);
        //     Console.WriteLine("Press CTRL + C or CTRL + Break to exit...");

        //  Console.CancelKeyPress += (sender, e) =>
        //     {
        // onShutdown();
        // e.Cancel = true;
        // manualResetEvent.Set();
        //  };

        //   manualResetEvent.WaitOne();
        //  }

    }



}