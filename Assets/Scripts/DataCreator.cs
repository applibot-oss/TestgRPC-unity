using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreator : MonoBehaviour {

	public List<string> data { get; private set; }

	private void Awake() {
        data = GetBigData();
    }

    public List<string> GetBigData() {
        List<string> list = new List<string>();
        for (int i = 0; i < 5000; ++i) {
            list.Add(System.Guid.NewGuid().ToString("N").Substring(0, 20));
        }
        return list;
    }

}
