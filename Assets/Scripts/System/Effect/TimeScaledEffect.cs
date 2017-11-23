using UnityEngine;

namespace System.Effect
{
    public class TimeScaledEffect: MonoBehaviour
    {
        [SerializeField]
        private float defaultSimulationSpeed = 1f;
        [SerializeField]
        private float destroyTime = 1f;
        [SerializeField]
        private ParticleSystem particle;

        
        void Update(){
            float timescale = Utility.System.TimeScaleManager.Instance.GetTimescale(Utility.System.TimeScaleManager.TimeScaleType.WORLD_TIMESCALE);
            destroyTime -= Time.deltaTime * timescale;
            if (destroyTime < 0f)
            {
                Destroy(gameObject);
            }

            ParticleSystem.MainModule pmain = particle.main;
            pmain.simulationSpeed = timescale*defaultSimulationSpeed;
        }
    }

}