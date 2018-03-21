using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace json.client {
    [RequireComponent(typeof(Button))]
    public class SeveralClient : MonoBehaviour {

        private const string url = "http://localhost:7000/several/data";

        private Button severalButton_;

        private void Awake() {
            severalButton_ = GetComponent<Button>();
            severalButton_.onClick.AddListener(() => StartCoroutine(GetSeveralData()));
        }

        private IEnumerator GetSeveralData() {

            Debug.Log("network start!");
            SeveralModel model = new SeveralModel();
            model.id = 123456;
            model.name = "test";
            model.email = "test@applibot";

            string jsonData = JsonUtility.ToJson(model);
            Debug.Log("reqeustData: " + jsonData);

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
                    Debug.Log(res.id);
                    Debug.Log(res.name);
                    Debug.Log(res.email);
                }
            }
        }


    }
}