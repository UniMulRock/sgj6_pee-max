using System;
using UnityEngine;

namespace PeeMax.Stage
{
    [Serializable]
    public class StageData
    {
        [CsvColumnAttribute(0)]
        [SerializeField]
        private int uniqueId;

        [CsvColumnAttribute(1)]
        [SerializeField]
        private string stageName;

        [CsvColumnAttribute(2)]
        [SerializeField]
        private string stageDataName;

        public string StageName{ get{ return stageName; } }

        public string StageDataName{ get{ return stageDataName; } }

        public override string ToString()
        {
            return string.Format("uniqueId={0}, stageName={1}, stageDataName={2}", uniqueId, stageName, stageDataName);
        }
    }
}