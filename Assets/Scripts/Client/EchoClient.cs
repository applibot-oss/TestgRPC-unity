using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Grpc.Core;
using Messages;

public class EchoClient : MonoBehaviour {

    private const string kPortNum = ":6565";

    private InputField echoField_;
    private Button echoButton_;

    private void Awake() {
        echoField_ = transform.Find("EchoField").GetComponent<InputField>();
        echoButton_ = transform.Find("EchoButton").GetComponent<Button>();
        echoButton_.onClick.AddListener(Echo);
    }

    public void Echo() {
        Debug.Log("echo start");

        var channel = new Channel("127.0.0.1" + kPortNum, ChannelCredentials.Insecure);

        var client = new EchoService.EchoServiceClient(channel);

        var reply = client.EchoService(new EchoMessage { Message = echoField_.text });

        Debug.Log("<color=red>" + reply.Message + "</color>");

        channel.ShutdownAsync().Wait();
        Debug.Log("echo End");
    }
}
