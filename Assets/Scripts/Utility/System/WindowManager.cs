using UnityEngine;
using UnityEngine.Collections;
using System.Collections.Generic;
using Utility.Window;

namespace Utility.System
{
    public class WindowManager : SingletonMonoBehaviour<WindowManager>
    {
        private const string WINDOW_DIC = "Window/";

        public enum WINDOW_TYPE
        {
            BASE,
            SETTING,
        }

        Dictionary<WINDOW_TYPE, string> WindowName = new Dictionary<WINDOW_TYPE, string>
        {
            { WINDOW_TYPE.BASE, "BaseWindow" },
            { WINDOW_TYPE.SETTING, "SettingWindow" },
        };

        Dictionary<WINDOW_TYPE, string> WindowClass = new Dictionary<WINDOW_TYPE, string>
        {
            { WINDOW_TYPE.BASE, "WindowBase" },
            { WINDOW_TYPE.SETTING, "SettingWindow" },
        };

        public WINDOW_TYPE WindowType { get; set; }

        public GameObject WindowRoot{ get; set; }

        new void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public GameObject CreateWindow(WINDOW_TYPE type)
        {
            if (WindowRoot == null)
                return null;

            var window = Resources.Load(WINDOW_DIC + WindowName[type]) as GameObject;
            if (window != null)
            {
                var instantiateObject = Instantiate(window, WindowRoot.transform);
                instantiateObject.name = WindowName[type];
                return instantiateObject;
            }
            else
            {
                Debug.LogWarning("【WindowCreateError】" + WINDOW_DIC + WindowName[type] + "の取得に失敗しました。");
            }
            return null;
        }

        public void OpenWindow(WINDOW_TYPE type)
        {
            if (WindowRoot == null)
                return;
            
            GameObject child;
            if (WindowRoot.transform.childCount > 0)
            {
                child = WindowRoot.transform.Find(WindowName[type]).gameObject;
                if (child == null)
                {
                    child = CreateWindow(type);
                    if (child == null)
                        return;
                }
            }
            else
            {
                child = CreateWindow(type);
                if (child == null)
                    return;
            }

            var windowClass = child.GetComponent(WindowClass[type]) as WindowBase;
            if (windowClass == null)
                return;
            
            switch (type)
            {
                case WINDOW_TYPE.BASE:
                    if(windowClass is WindowBase)
                    {
                        (windowClass as WindowBase).Open();
                    }
                    break;
                default:
                    windowClass.Open();
                    break;
            }
        }

        public void CloseWindow(WINDOW_TYPE type)
        {
            if (WindowRoot == null)
                return;
            var child = WindowRoot.transform.Find(WindowName[type]).gameObject;
            if (child == null)
                return;

            var windowClass = child.GetComponent(WindowClass[type]) as WindowBase;
            if (windowClass == null)
                return;
            switch (type)
            {
                case WINDOW_TYPE.BASE:
                    if(windowClass is WindowBase)
                    {
                        (windowClass as WindowBase).Close();
                    }
                    break;
                default:
                    windowClass.Close();
                    break;
            }
        }
    }
}
