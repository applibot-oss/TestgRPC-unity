using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Helloworld;

public class Test : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Say();
        }
    }

    private void Say() {
        Debug.Log("hello world");

        Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

        var client = new Greeter.GreeterClient(channel);

        string user = "you";

        var reply = client.SayHello(new HelloRequest { Name = user });
        Debug.Log("Greeting: " + reply.Message);

        channel.ShutdownAsync().Wait();
    }

}
