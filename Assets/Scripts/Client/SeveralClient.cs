using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Grpc.Core;
using Several;

[RequireComponent(typeof(Button))]
public class SeveralClient : MonoBehaviour {

    private const string kPortNum = ":6565";

    private Button severalButton_;

    private void Awake() {
        severalButton_ = GetComponent<Button>();
        severalButton_.onClick.AddListener(Tap);
    }

	private void Tap() {
         Debug.Log("echo start");

        var channel = new Channel("127.0.0.1" + kPortNum, ChannelCredentials.Insecure);

        var client = new SeveralService.SeveralServiceClient(channel);

        var req = new SeveralRequest();
        req.Dic.Add(1, "hoge");

        DateTime date = DateTime.Now;
        date.AddSeconds(10);
        var reply = client.GetSeveralData(req);

        Debug.Log("<color=red>" + reply.IsSuccess + "</color>");
        Debug.Log("<color=red>" + reply.Olds + "</color>");
        Debug.Log("<color=red>" + reply.Names + "</color>");

        channel.ShutdownAsync().Wait();
        Debug.Log("echo End");
	}

}
