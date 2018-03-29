using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Several;

namespace json.client {
    [RequireComponent(typeof(Button))]
    public class SeveralJsonClient : MonoBehaviour {

        private const string dataDirPath = "./jsonResult";
        private const string url = "http://localhost:7000/several/data";

        private DataCreator dataCreator_;
        private Button severalButton_;

        private System.IO.StreamWriter reqWriter_;
        private System.IO.StreamWriter resWriter_;

        private void Awake() {
            severalButton_ = GetComponent<Button>();
            severalButton_.onClick.AddListener(() => StartCoroutine(GetSeveralData()));
            dataCreator_ = GameObject.Find("DataCreator").GetComponent<DataCreator>();

            string dataFullDirPath = Path.GetFullPath(dataDirPath);
            if(!Directory.Exists(dataFullDirPath)) {
                Directory.CreateDirectory(dataFullDirPath);
            }

            reqWriter_ = new System.IO.StreamWriter(Path.Combine(dataDirPath, "res.txt"), false);
            resWriter_ = new System.IO.StreamWriter(Path.Combine(dataDirPath, "req.txt"), false);
        }

        private IEnumerator GetSeveralData() {


                var model = new json.model.SeveralModel();
                model.floatData = 1.23f;
                model.doubleData = 3.14;
                model.intData = 2;
                model.longData = 2222222222222;
                model.boolData = true;
                model.stringData = "applibot@test";
                model.list = dataCreator_.data;

            for (int i = 0; i < 50; ++i) {
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
                    if (request.responseCode == 200) {
                        string json = request.downloadHandler.text;
                        var res = JsonUtility.FromJson<json.model.SeveralModel>(json);
                        resWriter_.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                        resWriter_.Flush();
                    }
                }
            }
        }



    }
}