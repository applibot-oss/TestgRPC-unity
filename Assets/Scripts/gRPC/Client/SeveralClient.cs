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
        private DataCreator dataCreator_;
        private System.IO.StreamWriter reqWriter_;
        private System.IO.StreamWriter resWriter_;

        private void Awake() {
            severalButton_ = GetComponent<Button>();
            severalButton_.onClick.AddListener(Tap);
            dataCreator_ = GameObject.Find("DataCreator").GetComponent<DataCreator>();
            reqWriter_ = new System.IO.StreamWriter(requestFilePath_, false);
            resWriter_ = new System.IO.StreamWriter(responseFilePath_, false);
        }

        private void Tap() {
            var channel = new Channel("127.0.0.1" + kPortNum, ChannelCredentials.Insecure);

            //中身をひたすら作る
            var req = new SeveralData();
            req.FloatData = 1.23f;
            req.DoubleData = 3.14;
            req.IntData = 2;
            req.LongData = 2222222222222;
            req.BoolData = true;
            req.StringData = "applibot@test";
            req.List.AddRange(dataCreator_.data);

            for (int i = 0; i < 100; ++i) {
                var client = new SeveralService.SeveralServiceClient(channel);
                reqWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                reqWriter_.Flush();
                var reply = client.GetSeveralData(req);
                resWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                resWriter_.Flush();
            }
            channel.ShutdownAsync().Wait();

        }
    }
}