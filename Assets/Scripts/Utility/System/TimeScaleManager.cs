
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Utility.System
{
    public class TimeScaleManager : SingletonMonoBehaviour<TimeScaleManager>
    {
        private const float systemTimeScale = 1f; // システム時間は常に等速
        [SerializeField] private float playerTimeScale = 1f;       // ユーザの操作するキャラクターと同じ時間軸が対象
        [SerializeField] private float worldTimeScale = 1f;        // 敵キャラなどゲーム世界と同じ時間軸が対象

        public Action SetWorldTimeScaleSlowAction;
        public Action SetWorldTimeScaleNormalAction;
        public Action SetWorldTimeScaleFastAction;

        public const float WORLD_TIME_SCALE_SLOW = 0.3f;
        public const float WORLD_TIME_SCALE_NORMAL = 1f;
        public const float WORLD_TIME_SCALE_FAST = 2f;

        const float PICH_WORLD_TIME_SCALE_SLOW = 0.8f;
        const float PICH_WORLD_TIME_SCALE_NORMAL = 1f;
        const float PICH_WORLD_TIME_SCALE_FAST = 1.4f;

        public enum TimeScaleType{
            SYSTEM_TIMESCALE,
            PLAYER_TIMESCALE,
            WORLD_TIMESCALE,
        }

        new void Awake()
        {
            base.Awake();

            playerTimeScale = 1f;
            worldTimeScale = 1f;
        }

        public float GetTimescale(TimeScaleType tsType){
            switch (tsType)
            {
                case TimeScaleType.SYSTEM_TIMESCALE:
                    return systemTimeScale;
                case TimeScaleType.PLAYER_TIMESCALE:
                    return playerTimeScale;
                case TimeScaleType.WORLD_TIMESCALE:
                    return worldTimeScale;
            }
            Debug.Log("ERROR: unknown timescale type...");
            return 1f;
        }

        public void SetPlayerTimeScale(float timescale){//明示的な雪駄で変更するように
            playerTimeScale = timescale;
        }

        public void SetWorldTimeScale(float timescale){
            worldTimeScale = timescale;
        }
            
        public void SetWorldTimeScaleSlow(){
            worldTimeScale = WORLD_TIME_SCALE_SLOW;
            Sound.PichBgm(PICH_WORLD_TIME_SCALE_SLOW);
            if (SetWorldTimeScaleSlowAction != null)
            {
                SetWorldTimeScaleSlowAction();
            }
        }

        public void SetWorldTimeScaleNormal(){
            worldTimeScale = WORLD_TIME_SCALE_NORMAL;
            Sound.PichBgm(PICH_WORLD_TIME_SCALE_NORMAL);
            if (SetWorldTimeScaleNormalAction != null)
            {
                SetWorldTimeScaleNormalAction();
            }
        }

        public void SetWorldTimeScaleFast(){
            worldTimeScale = WORLD_TIME_SCALE_FAST;
            Sound.PichBgm(PICH_WORLD_TIME_SCALE_FAST);
            if (SetWorldTimeScaleFastAction != null)
            {
                SetWorldTimeScaleFastAction();
            }
        }
    }
}
