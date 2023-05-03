using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Main.View
{
    /// <summary>
    /// ヒトデゲージカウンター
    /// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class SeastarGageCounter : MonoBehaviour, ISeastarGageCounter
    {
        /// <summary>デフォルト表示</summary>
        [SerializeField] private string slashAndSpace = " / ";
        /// <summary>分母</summary>
        private int _denominator = -1;
        /// <summary>テキストメッシュプロ</summary>
        private TextMeshPro _textMeshPro;
        /// <summary>アニメーション時間</summary>
        [SerializeField] private float duration = 1.25f;

        public bool PlayCounterBetweenAnimation(int numerator)
        {
            return PlayCounterBetweenAnimation(numerator, _denominator);
        }

        public bool PlayCounterBetweenAnimation(int numerator, int denominator)
        {
            try
            {
                if (_textMeshPro == null)
                    _textMeshPro = GetComponent<TextMeshPro>();
                if (denominator < 0)
                    throw new System.Exception("分母の未設定");
                _denominator = denominator;
                var prev = _textMeshPro.text;
                var update = $"{numerator}{slashAndSpace}{_denominator}";
                if (!prev.Equals(update))
                {
                    DOVirtual.DelayedCall(duration, () =>
                    {
                        // 差異があった場合のみ更新
                        DOTween.To(() => int.Parse(prev.Replace($"{slashAndSpace}{_denominator}", "")),
                            x => _textMeshPro.text = $"{x}{slashAndSpace}{_denominator}",
                            numerator,
                            duration);
                    });
                }

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public bool SetCounterBetween(int numerator)
        {
            return SetCounterBetween(numerator, _denominator);
        }

        public bool SetCounterBetween(int numerator, int denominator)
        {
            try
            {
                if (_textMeshPro == null)
                    _textMeshPro = GetComponent<TextMeshPro>();
                if (denominator < 0)
                    throw new System.Exception("分母の未設定");
                _denominator = denominator;
                var prev = _textMeshPro.text;
                var update = $"{numerator}{slashAndSpace}{_denominator}";
                if (!prev.Equals(update))
                    // 差異があった場合のみ更新
                    _textMeshPro.text = update;

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
    /// ヒトデゲージカウンター
    /// インターフェース
    /// </summary>
    public interface ISeastarGageCounter
    {
        /// <summary>
        /// カウンターをセット
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <returns>成功／失敗</returns>
        public bool SetCounterBetween(int numerator);
        /// <summary>
        /// カウンターをセット
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns>成功／失敗</returns>
        public bool SetCounterBetween(int numerator, int denominator);
        /// <summary>
        /// カウンターセットアニメーション
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <returns>成功／失敗</returns>
        public bool PlayCounterBetweenAnimation(int numerator);
        /// <summary>
        /// カウンターセットアニメーション
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns>成功／失敗</returns>
        public bool PlayCounterBetweenAnimation(int numerator, int denominator);
    }
}