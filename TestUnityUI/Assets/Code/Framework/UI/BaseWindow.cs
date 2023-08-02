using UnityEngine;

namespace Framework.UI
{
    public abstract class BaseWindow : MonoBehaviour
    {
        protected Canvas _windowCanvas;
        public static TWindow Get<TWindow>() where TWindow : BaseWindow
        {
            return FindObjectOfType<TWindow>(true);
        }
        
        protected virtual void Awake()
        {
            _windowCanvas = gameObject.GetComponent<Canvas>();
            
            _windowCanvas.enabled = false;
        }

        public void Show(params object[] args)
        {
            OnShow(args);
            
            _windowCanvas.enabled = true;
        }
    
        public void Hide()
        {
            _windowCanvas.enabled = false;
            
            OnHide();
        }

        protected abstract void OnShow(object[] args);
        protected abstract void OnHide();
    }
}