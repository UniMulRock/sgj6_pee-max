
using Utility.System;
using UnityEngine;

namespace Utility.Window
{
    public class CompletionWindow : WindowBase
    {
        public void OnClose()
        {
            if (WindowManager.Validation())
            {
                WindowManager.Instance.CloseWindow(WindowManager.WINDOW_TYPE.COMPLETION);
            }
        }
    }
}

