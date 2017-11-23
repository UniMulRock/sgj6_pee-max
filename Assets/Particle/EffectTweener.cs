using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace effects
{
    public class EffectTweener : MonoBehaviour
    {
        [SerializeField]
        Image _image;

        Material _material;
        int _heightPropertyID;
        [SerializeField]
        bool _isHidden = false;

        void Awake()
        {
            _heightPropertyID = Shader.PropertyToID("_Height");
            _material = _image.material;
            if (_isHidden)
            {
                _material.SetFloat(_heightPropertyID, 0);
            }
            else
            {
                _material.SetFloat(_heightPropertyID, 1);
            }
        }

        public void SetHidden(bool isHidden)
        {
            _isHidden = isHidden;
            if (_isHidden)
            {
                _material.SetFloat(_heightPropertyID, 0);
            }
            else
            {
                _material.SetFloat(_heightPropertyID, 1);
            }
        }

        [ContextMenu("Play Effects")]
        public void PlayEffects()
        {
            if (_isHidden)
            {
                Play(from: 0f, to: 1f);
            }
            else
            {
                Play(from: 1f, to: 0f);
            }
            _isHidden = !_isHidden;
        }

        void Play(float from, float to)
        {
            DOTween.Kill(this);
            DOTween.To(
                () => from,
                a => _material.SetFloat(_heightPropertyID, a),
                to,
                1.5f)
            .SetId(this);
        }
    }
}