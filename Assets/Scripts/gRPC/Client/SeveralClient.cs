using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Grpc.Core;
using Several;

namespace grpc.client {
    [RequireComponent(typeof(Button))]
    public class SeveralClient : MonoBehaviour {

        private const string requestFilePath_ = "/Users/s01308/result/req.txt";
        private const string responseFilePath_ = "/Users/s01308/result/res.txt";
        private const string kPortNum = ":6565";

        private Button severalButton_;
        private System.IO.StreamWriter reqWriter_;
        private System.IO.StreamWriter resWriter_;

        private void Awake() {
            severalButton_ = GetComponent<Button>();
            severalButton_.onClick.AddListener(Tap);
            reqWriter_ = new System.IO.StreamWriter(requestFilePath_, false);
            resWriter_ = new System.IO.StreamWriter(responseFilePath_, false);
        }

        private void Tap() {
            var channel = new Channel("127.0.0.1" + kPortNum, ChannelCredentials.Insecure);

            var client = new SeveralService.SeveralServiceClient(channel);

            //中身をひたすら作る
            var req = new SeveralRequest();
            req.FloatData = 3.14f;

            reqWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            reqWriter_.Flush();
            var reply = client.GetSeveralData(req);
            resWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            resWriter_.Flush();

            channel.ShutdownAsync().Wait();
        }
    }
}