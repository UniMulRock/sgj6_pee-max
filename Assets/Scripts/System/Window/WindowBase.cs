using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Utility.Window
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject ScreenRoot;

        private const float SCALE_DURATION = 0.5f;

        private void Awake()
        {
            if (ScreenRoot != null)
            {
                if(ScreenRoot.activeSelf)
                    ScreenRoot.SetActive(false);
                ScreenRoot.transform.localScale = Vector3.zero;
            }
        }

        [ContextMenu("Open")]
        virtual public void Open()
        {
            if (ScreenRoot == null)
                return;

            ScreenRoot.SetActive(true);

            Sequence seq = DOTween.Sequence();
            seq.Append
            (
                ScreenRoot.transform.DOScale(Vector3.one, SCALE_DURATION)
            ).OnComplete(()=>{});
        }

        [ContextMenu("Close")]
        virtual public void Close()
        {
            if (ScreenRoot == null)
                return;
            Sequence seq = DOTween.Sequence();
            seq.Append
            (
                ScreenRoot.transform.DOScale(Vector3.zero, SCALE_DURATION)
            ).OnComplete(() =>
            {
                ScreenRoot.SetActive(false);
            });
        }
    }
}
