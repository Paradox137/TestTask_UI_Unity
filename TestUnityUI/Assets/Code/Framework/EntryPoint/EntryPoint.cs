using Framework.UI;
using UIModule;
using UIModule.Windows;
using UnityEngine;

namespace Framework.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        public void Start()
        {
            BaseWindow.Get<MainWindow>().Show();
        }
    
    }
}