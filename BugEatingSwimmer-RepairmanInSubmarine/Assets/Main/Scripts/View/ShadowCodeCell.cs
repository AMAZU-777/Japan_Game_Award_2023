using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Main.Common;

namespace Main.View
{
    /// <summary>
    /// コードセル
    /// </summary>
    public class ShadowCodeCell : ShadowCodeCellParent, IShadowCodeCell
    {
        /// <summary>ターンアニメーション時間</summary>
        [SerializeField] private float turnDuration = .35f;

        public IEnumerator PlayLightAnimation(IObserver<bool> observer, EnumDirectionMode enumDirectionMode)
        {
            throw new NotImplementedException();
        }

        public IEnumerator PlaySpinAnimation(IObserver<bool> observer, Vector3 vectorDirectionMode)
        {
            if (_transform == null)
                _transform = transform;
            _transform.DOLocalRotate(vectorDirectionMode, turnDuration)
                .OnComplete(() => observer.OnNext(true));

            yield return null;
        }

        public bool SetSpinDirection(Vector3 vectorDirectionMode)
        {
            throw new NotImplementedException();
        }

        public bool SetAlphaOff()
        {
            throw new NotImplementedException();
        }

        public bool InitializeLight(EnumDirectionMode enumDirectionMode)
        {
            throw new NotImplementedException();
        }

        public IEnumerator PlayErrorLightFlashAnimation(IObserver<bool> observer)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// コード
    /// インターフェース
    /// </summary>
    public interface IShadowCodeCell
    {
        /// <summary>
        /// 回転アニメーション
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <param name="vectorDirectionMode">方角モードのベクター</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlaySpinAnimation(System.IObserver<bool> observer, Vector3 vectorDirectionMode);

        /// <summary>
        /// 回転方角セット
        /// </summary>
        /// <param name="vectorDirectionMode">方角モードのベクター</param>
        /// <returns>成功／失敗</returns>
        public bool SetSpinDirection(Vector3 vectorDirectionMode);

        /// <summary>
        /// ライト点灯アニメーション
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <param name="enumDirectionMode">方角モード</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayLightAnimation(System.IObserver<bool> observer, EnumDirectionMode enumDirectionMode);

        /// <summary>
        /// ライト点灯初期設定
        /// </summary>
        /// <param name="enumDirectionMode">方角モード</param>
        /// <returns>成功／失敗</returns>
        public bool InitializeLight(EnumDirectionMode enumDirectionMode);

        /// <summary>
        /// アルファ値をセット
        /// </summary>
        /// <returns>成功／失敗</returns>
        public bool SetAlphaOff();

        /// <summary>
        /// エラーライト点滅アニメーション
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayErrorLightFlashAnimation(System.IObserver<bool> observer);
    }
}
