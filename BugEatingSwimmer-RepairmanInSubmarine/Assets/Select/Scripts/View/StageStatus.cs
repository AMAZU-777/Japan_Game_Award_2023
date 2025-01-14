using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select.Common;

namespace Select.View
{
    /// <summary>
    /// ステージ状態の表示テキスト
    /// </summary>
    public class StageStatus : BodyText, IStageStatus
    {
        /// <summary>メッセージカラー</summary>
        [SerializeField] private Color[] mesColors =
        {
            Color.red,
            Color.green,
            Color.blue,
        };

        public bool SetColorTextMessage(EnumStageStatusMessage enumStageStatusMessage)
        {
            try
            {
                if (!SetColorText(mesColors[(int)enumStageStatusMessage]))
                    throw new System.Exception("テキストのカラーを変更呼び出しの失敗");

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public bool SetTextMessage(EnumStageStatusMessage enumStageStatusMessage)
        {
            try
            {
                text.text = $"{enumStageStatusMessage}";

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }

    /// <summary>
    /// ステージ状態の表示テキスト
    /// インターフェース
    /// </summary>
    public interface IStageStatus
    {
        /// <summary>
        /// テキストメッセージを表示
        /// </summary>
        /// <param name="enumStageStatusMessage">ステージ状態の表示テキストインデックス</param>
        /// <returns>成功／失敗</returns>
        public bool SetTextMessage(EnumStageStatusMessage enumStageStatusMessage);
        /// <summary>
        /// テキストメッセージカラーをセット
        /// </summary>
        /// <param name="enumStageStatusMessage">ステージ状態の表示テキストインデックス</param>
        /// <returns>成功／失敗</returns>
        public bool SetColorTextMessage(EnumStageStatusMessage enumStageStatusMessage);
    }
}
