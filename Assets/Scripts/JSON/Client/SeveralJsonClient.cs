using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Several;

namespace json.client {
    [RequireComponent(typeof(Button))]
    public class SeveralJsonClient : MonoBehaviour {

        private const string requestFilePath_ = "/Users/s01308/result/req.txt";
        private const string responseFilePath_ = "/Users/s01308/result/res.txt";

        private const string url = "http://localhost:7000/several/data";

        private Button severalButton_;

        private System.IO.StreamWriter reqWriter_;
        private System.IO.StreamWriter resWriter_;

        private void Awake() {
            severalButton_ = GetComponent<Button>();
            severalButton_.onClick.AddListener(() => StartCoroutine(GetSeveralData()));
            reqWriter_ = new System.IO.StreamWriter(requestFilePath_, false);
            resWriter_ = new System.IO.StreamWriter(responseFilePath_, false);
        }

        private IEnumerator GetSeveralData() {

            SeveralModel model = new SeveralModel();
            model.id = 1234;
            model.name = "test";
            model.email = "applibot@test";

            reqWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            reqWriter_.Flush();
            string jsonData = JsonUtility.ToJson(model);

            var request = new UnityWebRequest();
            request.url = url;
            byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
            request.method = UnityWebRequest.kHttpVerbPOST;
            yield return request.SendWebRequest();

            if (request.isNetworkError) {
                Debug.Log(request.error);
            } else {
                Debug.Log(request.downloadHandler.text);
                if (request.responseCode == 200) {
                    string json = request.downloadHandler.text;
                    SeveralModel res = JsonUtility.FromJson<SeveralModel>(json);
                    resWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                    reqWriter_.Flush();
                }
            }
        }


    }
}