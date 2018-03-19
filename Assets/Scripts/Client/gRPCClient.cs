using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Messages;

public class gRPCClient : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Test();
        }
    }

    private void Test() {
        Debug.Log("test start");

        var channel = new Channel("127.0.0.1:6565", ChannelCredentials.Insecure);

        var client = new EchoService.EchoServiceClient(channel);

        var reply = client.EchoService(new EchoMessage { Message = "test" });

        Debug.Log(reply.Message);

        channel.ShutdownAsync().Wait();
        Debug.Log("Network End");
    }
}
