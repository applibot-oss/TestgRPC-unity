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

        //中身をひたすら作る
        var req = new SeveralRequest();
        req.FloatData = 3.14f;
        req.DoubleData = 1.23;
        req.IntData = 10;
        req.LongData = 2300000000000;
        req.BoolData = true;
        req.StringData = "hoge";
        req.ByteData = Google.Protobuf.ByteString.CopyFromUtf8("foo");
        req.List.Add(1);
        innerTest innerList = new innerTest();
        innerList.FList.Add(1.23f);
        req.InnerList.Add(innerList);
        req.Dic.Add(1, "hoge");
        req.DicRepeat.Add(2, innerList);
        req.Animal = Animal.Dog;

        var reply = client.GetSeveralData(req);
        Debug.Log("<color=red>" + reply.IsSuccess + "</color>");

        channel.ShutdownAsync().Wait();
        Debug.Log("echo End");
    }

}
