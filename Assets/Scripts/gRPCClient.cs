using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using User;

public class gRPCClient : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Test();
        }
    }

    private void Test() {
        Debug.Log("test start");

        var channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

        var client = new UserService.UserServiceClient(channel);

        var reply = client.GetUser(new UserRequest { UserId = "siguma" });

        Debug.Log(reply.Name + " " + reply.Email);

        channel.ShutdownAsync().Wait();
        Debug.Log("Network End");
    }
}
