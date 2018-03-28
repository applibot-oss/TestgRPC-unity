using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace json.model {

    [System.Serializable]
    public class SeveralModel {
        public float floatData;
        public double doubleData;
        public int intData;
        public long longData;
        public bool boolData;
        public string stringData;
        public List<string> list;
    }
}
