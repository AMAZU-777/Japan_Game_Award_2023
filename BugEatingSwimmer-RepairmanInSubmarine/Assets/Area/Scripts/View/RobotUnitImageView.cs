using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Area.Common;

namespace Area.View 
{
    ///<summary>
    /// 各ユニットの制御
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RobotUnitImageConfig))]
    public class RobotUnitImageView : MonoBehaviour, IRobotUnitImageView
    {
        /// <summary>各ユニットの制御設定</summary>
        [SerializeField] private RobotUnitImageConfig robotUnitImageConfig;
        /// <summary>各ユニットの制御設定</summary>
        public RobotUnitImageConfig RobotUnitImageConfig => robotUnitImageConfig;
        /// <summary>画像</summary>
        [SerializeField] private Image image;
        /// <summary>非選択状態のカラー</summary>
        [SerializeField] private Color disabledColor = new Color(.4f, .4f, .4f);
        /// <summary>選択状態のカラー</summary>
        [SerializeField] private Color enabledColor = Color.white;
        /// <summary>エラー状態のカラー</summary>
        [SerializeField] private Color errorColor = new Color(.99f, .26f, .16f);

        private void Reset()
        {
            robotUnitImageConfig = GetComponent<RobotUnitImageConfig>();
            image = GetComponent<Image>();
        }

        public void SetPositionAndEulerAngle()
        {
            Transform myTransform = this.transform;
            (myTransform as RectTransform).anchoredPosition = robotUnitImageConfig.Pos;
            myTransform.eulerAngles = robotUnitImageConfig.Rotate;
        }

        public void SetImageAltha(bool visibled)
        {
            var color = image.color;
            var colorwork = new Color(color.r, color.g, color.b, visibled ? 1.0f : 0.0f);
            image.color = colorwork;
        }

        public IEnumerator PlayFadeAnimation(System.IObserver<bool> observer, bool visibled)
        {
            image.DOFade(endValue: visibled ? 1.0f : 0.0f, robotUnitImageConfig.Durations[0])
                .SetUpdate(true)
                .OnComplete(() => observer.OnNext(true));
            yield return null;
        }

        public IEnumerator PlayAnimationMove(System.IObserver<bool> observer)
        {
            Transform myTransform = this.transform;
            (myTransform as RectTransform).DOAnchorPos(robotUnitImageConfig.Pos, robotUnitImageConfig.Durations[0])
                .SetUpdate(true)
                .OnComplete(() => observer.OnNext(true));
            myTransform.eulerAngles = robotUnitImageConfig.Rotate;

            yield return null;
        }

        public void RendererDisableMode()
        {
            image.color = disabledColor;
        }

        public IEnumerator PlayRenderEnable(System.IObserver<bool> observer)
        {
            image.DOColor(enabledColor, robotUnitImageConfig.Durations[0])
                .OnComplete(() => observer.OnNext(true));

            yield return null;
        }

        public void RendererEnableMode()
        {
            image.color = enabledColor;
        }

        public IEnumerator PlayAnimationMoveAndErrorSignal(System.IObserver<bool> observer)
        {
            transform.eulerAngles = robotUnitImageConfig.Rotate;
            Sequence sequence = DOTween.Sequence()
                .Append((transform as RectTransform).DOAnchorPos(robotUnitImageConfig.Pos, robotUnitImageConfig.Durations[0]))
                .Append(image.DOColor(errorColor, robotUnitImageConfig.Durations[1])
                    .From(image.color = enabledColor)
                    .SetLoops(7, LoopType.Yoyo))
                .SetUpdate(true)
                .OnComplete(() => observer.OnNext(true));

            yield return null;
        }

        public IEnumerator PlayRepairEffect(System.IObserver<bool> observer)
        {
            if (!AreaGameManager.Instance.ParticleSystemsOwner.PlayParticleSystems(GetInstanceID(), EnumParticleSystemsIndex.ParticlesOfLightGatherAround, transform.position))
                Debug.LogError("指定されたパーティクルシステムを再生する呼び出しの失敗");
            var t = AreaGameManager.Instance.ParticleSystemsOwner.GetParticleSystemsTransform(GetInstanceID(), EnumParticleSystemsIndex.ParticlesOfLightGatherAround);
            DOVirtual.DelayedCall(RobotUnitImageConfig.Durations[0], () => t.gameObject.SetActive(false))
                .OnComplete(() => observer.OnNext(true));

            yield return null;
        }
    }

    ///<summary>
    /// 各ユニットの制御
    /// インターフェース
    /// </summary>
    public interface IRobotUnitImageView
    {
        /// <summary>
        /// 位置と角度をセット
        /// </summary>
        public void SetPositionAndEulerAngle();
        /// <summary>
        /// アルファ値をセット
        /// </summary>
        /// <param name="visibled">表示／非表示</param>
        public void SetImageAltha(bool visibled);
        /// <summary>
        /// 非選択状態
        /// </summary>
        public void RendererDisableMode();
        /// <summary>
        /// 選択状態
        /// </summary>
        public void RendererEnableMode();
        /// <summary>
        /// 選択状態解放演出
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayRenderEnable(System.IObserver<bool> observer);
        /// <summary>
        /// フェードのDOTweenアニメーション再生
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>成功／失敗</returns>
        public IEnumerator PlayFadeAnimation(System.IObserver<bool> observer, bool visibled);
        /// <summary>
        /// 位置を動かすアニメーションを再生
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayAnimationMove(System.IObserver<bool> observer);
        /// <summary>
        /// 位置を動かしてエラーになるアニメーションを再生
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayAnimationMoveAndErrorSignal(System.IObserver<bool> observer);
        /// <summary>
        /// 対象ユニットへエフェクト演出
        /// </summary>
        /// <param name="observer">バインド</param>
        /// <returns>コルーチン</returns>
        public IEnumerator PlayRepairEffect(System.IObserver<bool> observer);
    }
}
