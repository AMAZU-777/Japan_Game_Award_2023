using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Area.Common
{
    public enum EnumEventCommand
    {
        /// <summary>デフォルト</summary>
        Default = -1,
        /// <summary>選択された</summary>
        Selected = 0,
        /// <summary>選択解除された</summary>
        DeSelected = 1,
        /// <summary>実行された</summary>
        Submited = 2,
        /// <summary>キャンセルされた</summary>
        Canceled = 3,
        /// <summary>いずれかのキーの入力</summary>
        AnyKeysPushed = 4,
    }
}
