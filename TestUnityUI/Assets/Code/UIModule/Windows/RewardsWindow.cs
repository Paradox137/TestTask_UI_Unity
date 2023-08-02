using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Framework.Inventory;
using Framework.UI;
using TMPro;
using UIModule.Extensions;
using UIModule.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UIModule.Windows
{
    public class RewardsWindow : BaseWindow
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _rewardsText;
        [SerializeField] private RectTransform _rewardsHolder;
        [SerializeField] private RectTransform _rewardsViewport;
        [SerializeField] private RewardView _rewardView;
        [SerializeField] private ScrollRect _scrollRect;
        
        private RewardView[] _rewardViews;
        private readonly Vector2 LeftCornerPivot = new Vector2(0f,0.5f);
        private readonly Vector2 MiddleCornerPivot = new Vector2(0.5f,0.5f);
        protected override void Awake()
        {
            base.Awake();
            _closeButton.onClick.AddListener(Hide);
        }

        protected override async void OnShow(object[] args)
        {
            _titleText.text = (string)args[0];

            var items = (List<InventoryItem>)args[1];

            CreateRewardsViews((List<InventoryItem>)args[1]);
            
            await AlignItems();

            _rewardsHolder.GetComponent<ContentSizeFitter>().enabled = false;
        }
        
        private void CreateRewardsViews(List<InventoryItem> __rewardItems)
        {
           _rewardViews = new RewardView[__rewardItems.Count];
            
            for (int i = 0; i < _rewardViews.Length; i++)
            {
                RewardView rewardView = Object.Instantiate(_rewardView, _rewardsHolder);
                
                rewardView.Init(__rewardItems[i]);
                
                _rewardViews[i] = rewardView;

                rewardView.onClicked += OpenPopUp;
            }
        }
        
        private async UniTask AlignItems()
        {
            await UniTask.NextFrame();
            
            Vector3[] childCorners = new Vector3[4];
            _rewardViews[^1].Rect.GetWorldCorners(childCorners);

            bool isPlaceable =  _rewardsViewport.IsInsideHorizontal(_rewardViews[0].Rect, true, false)
                && _rewardsViewport.IsInsideHorizontal(_rewardViews[^1].Rect, false, true);

            if (!isPlaceable)
                _rewardsHolder.pivot = LeftCornerPivot;
            else
                _scrollRect.enabled = false;
        }

        private void OpenPopUp(InventoryItem __inventoryItem)
        {
            Get<RewardPopUpWindow>().Show(__inventoryItem);
        }
        
        protected override void OnHide()
        {
            foreach (RewardView rewardView in _rewardViews)
            {
                Object.Destroy(rewardView.gameObject);
            }
            
            // to method: restore ui?
            _scrollRect.enabled = true;
            _rewardsHolder.GetComponent<ContentSizeFitter>().enabled = true;
            _rewardsHolder.pivot = MiddleCornerPivot;
        }
    }
}