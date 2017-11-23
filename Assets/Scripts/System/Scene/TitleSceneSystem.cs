using Utility.System;
using GodTouches;
using UnityEngine;
using System.Collections;
using effects;

namespace System.Scene
{
    public class TitleSceneSystem : SingletonMonoBehaviour<TitleSceneSystem>
    {
        [SerializeField]
        private EffectTweener effects;
        private bool isNextScene;
        void Start()
        {
            if (!Sound.IsPlayingBgm())
            {
                Sound.PichBgm(1f);
                Sound.PlayBgm("asr_story1_looped");
            }
            effects.SetHidden(false);
            isNextScene = false;
        }

        void Update()
        {
            if (!UnityEngine.Rendering.SplashScreen.isFinished)
                return;
            
            // タッチを検出
            var phase = GodTouch.GetPhase();
            if (phase == GodPhase.Began)
            {
                if (isNextScene)
                    return;
                isNextScene = true;
                Sound.PlaySe("jingle_start");
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
