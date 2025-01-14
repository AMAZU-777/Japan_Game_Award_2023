using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select.Common;

namespace Select.Model
{
    /// <summary>
    /// 始点
    /// 静的に設定する用
    /// </summary>
    public class PivotConfig : MonoBehaviour
    {
        /// <summary>方角モードのデフォルト値</summary>
        [SerializeField] private EnumDirectionMode enumDirectionModeDefault = EnumDirectionMode.Up;
        /// <summary>方角モードのデフォルト値</summary>
        public EnumDirectionMode EnumDirectionModeDefault => enumDirectionModeDefault;
        /// <summary>ノードコードの識別ID</summary>
        [SerializeField] private EnumNodeCodeID enumNodeCodeId = EnumNodeCodeID.StartNode_1;
        /// <summary>ノードコードの識別ID</summary>
        public EnumNodeCodeID EnumNodeCodeID => enumNodeCodeId;
    }
}
