using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Main.Common;
using DG.Tweening;
using Effect;

namespace Main.View
{
    /// <summary>
    /// ビュー
    /// バグ
    /// </summary>
    public class BugView : MonoBehaviour, IBugView, IBodySprite
    {
        /// <summary>クリアカラー</summary>
        [SerializeField] private Color clearedColor = Color.yellow;
        /// <summary>既にクリア済みのクリアカラー</summary>
        [SerializeField] private Color alreadyClearedColor = Color.blue;
        /// <summary>ボディのスプライト</summary>
        [SerializeField] private BodySprite bodySprite;
        /// <summary>演出</summary>
        [SerializeField] private float[] durations = { .25f, .25f };

        /// <summary>トランスフォーム</summary>
        private Transform _transform;

        private void Reset()
        {
            bodySprite = transform.GetChild(0).GetComponent<BodySprite>();
        }

        public IEnumerator PlayFadeAnimation(IObserver<bool> observer)
        {
            return bodySprite.PlayFadeAnimation(observer);
        }

        public bool SetColorCleared(bool isAlreadyCleared)
        {
            if (_transform == null)
                _transform = transform;
            return _transform.GetChild(0).GetComponent<BodySprite>().SetColorSpriteRenderer(isAlreadyCleared ? alreadyClearedColor : clearedColor);
        }

        public bool SetColorSpriteRenderer(Color color)
        {
            throw new NotImplementedException();
        }

        public bool PlayCorrectOrWrong()
        {
            try
            {
                if (_transform == null)
                    _transform = transform;
                Direction();
                DOVirtual.DelayedCall(durations[0], () => Direction())
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(durations[1], () => Direction());
                    });

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        private void Direction()
        {
            if (!MainGameManager.Instance.ParticleSystemsOwner.PlayParticleSystems(GetInstanceID(), EnumParticleSystemsIndex.ParticleJigglyBubbleSoapyPlayer, _transform.position))
                Debug.LogError("指定されたパーティクルシステムを再生する呼び出しの失敗");
            MainGameManager.Instance.AudioOwner.PlaySFX(Audio.ClipToPlay.se_swim);
        }

        public bool PlayBugAura()
        {
            try
            {
                if (!MainGameManager.Instance.ParticleSystemsOwner.PlayParticleSystems(GetInstanceID(), EnumParticleSystemsIndex.BugAura, _transform.position))
                    Debug.LogError("指定されたパーティクルシステムを再生する呼び出しの失敗");
                var bugAura = MainGameManager.Instance.ParticleSystemsOwner.GetParticleSystemsTransform(GetInstanceID(), EnumParticleSystemsIndex.BugAura);
                if (bugAura.GetComponent<TrackMovement>() == null)
                {
                    bugAura.gameObject.AddComponent<TrackMovement>();
                    if (bugAura.GetComponent<TrackMovement>().Target == null ||
                        (bugAura.GetComponent<TrackMovement>().Target != null &&
                            !bugAura.GetComponent<TrackMovement>().Target.Equals(transform)))
                        if (!bugAura.GetComponent<TrackMovement>().SetTarget(transform))
                            throw new System.Exception("ターゲットをセットする呼び出しの失敗");
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }

        public bool StopBugAura()
        {
            try
            {
                if (!MainGameManager.Instance.ParticleSystemsOwner.StopParticleSystems(GetInstanceID(), EnumParticleSystemsIndex.BugAura))
                    Debug.LogError("指定されたパーティクルシステムを再生する呼び出しの失敗");

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }

    /// <summary>
    /// ビュー
    /// バグ
    /// インターフェース
    /// </summary>
    public interface IBugView
    {
        /// <summary>
        /// カラーを設定
        /// </summary>
        /// <param name="isAlreadyCleared">クリア済みならTrue</param>
        /// <returns>成功／失敗</returns>
        public bool SetColorCleared(bool isAlreadyCleared);
        /// <summary>
        /// バグ消失パーティクルを再生
        /// </summary>
        /// <returns>成功／失敗</returns>
        public bool PlayCorrectOrWrong();
        /// <summary>
        /// バグオーラを発生
        /// </summary>
        /// <returns>成功／失敗</returns>
        public bool PlayBugAura();
        /// <summary>
        /// バグオーラを停止
        /// </summary>
        /// <returns>成功／失敗</returns>
        public bool StopBugAura();
    }
}
