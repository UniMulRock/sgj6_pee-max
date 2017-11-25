using Utility.System;
using GodTouches;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using effects;
using DG.Tweening;

namespace System.Scene
{
    public class TitleSceneSystem : SingletonMonoBehaviour<TitleSceneSystem>
    {
        /// <summary>
        /// 遷移エフェクト対象
        /// </summary>
        [SerializeField]
        private EffectTweener effects;

        /// <summary>
        /// キャラクター位置
        /// </summary>
        [SerializeField]
        private Transform characterPosition;

        /// <summary>
        /// キャラクター
        /// </summary>
        [SerializeField]
        private Image characterImage;

        /// <summary>
        /// テキスト
        /// </summary>
        [SerializeField]
        private UnityEngine.UI.Text startText;

        /// <summary>
        /// 遷移が呼ばれているか
        /// </summary>
        private bool isNextScene;

        /// <summary>
        /// タッチできるか
        /// </summary>
        private bool canTouch;

        void Start()
        {
            canTouch = false;
            effects.SetHidden(false);
            isNextScene = false;
            if (characterImage != null)
            {
                characterImage.transform.DOMove(
                    characterPosition.position,
                    1f
                ).OnComplete(()=>{
                    // アニメーション終了時
                    canTouch = true;
                });
            }
            if (startText != null)
            {
                startText.transform.DOScale(
                    1.5f,
                    1f
                ).SetLoops(
                    -1,
                    LoopType.Yoyo
                );
            }
        }

        void Update()
        {
            if (!canTouch)
                return;
            
            // タッチを検出
            var phase = GodTouch.GetPhase();
            if (phase == GodPhase.Began)
            {
                if (isNextScene)
                    return;
                isNextScene = true;
                characterImage.DOFade(0, 1.0f);
                startText.DOFade(0, 1.0f);
                effects.PlayEffects();
                StartCoroutine(WaitNextScene(1f));
            }
        }

        IEnumerator WaitNextScene(float waitTime){
            yield return new WaitForSeconds(waitTime);
            NextScene();
        }

        private void NextScene()
        {
            SceneManager.Instance.ChangeState(SceneManager.STATE.GAME);
            Sound.StopBgm();
        }
    }
}
