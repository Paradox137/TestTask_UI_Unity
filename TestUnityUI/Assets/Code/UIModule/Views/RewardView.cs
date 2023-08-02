using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Framework.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIModule.Views
{
	public class RewardView : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
	{
		[SerializeField] private Image _rewardIcon;
		
		private InventoryItem _inventoryItem;
		private const float WAIT_SECONDS = 1.5f;
		private CancellationTokenSource _cancellationTokenSource;
		private CancellationToken _cancellationToken;
		
		public event Action<InventoryItem> onClicked;
		public RectTransform Rect { get; private set; }
		
		public void Init(InventoryItem __inventoryItem)
		{
			Rect = gameObject.GetComponent<RectTransform>();
			_inventoryItem = __inventoryItem;
			
			_rewardIcon.sprite = _inventoryItem.Icon;
		}
		
		public void OnPointerUp(PointerEventData eventData)
		{
			_cancellationTokenSource.Cancel();
		}
		public void OnPointerDown(PointerEventData eventData)
		{
			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationToken = _cancellationTokenSource.Token;
			
			WaitFixedTime();
		}
		private async void WaitFixedTime()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(WAIT_SECONDS), DelayType.Realtime, PlayerLoopTiming.Update, _cancellationToken);
			onClicked?.Invoke(_inventoryItem);
		}
		private void OnDestroy()
		{
			onClicked = null;
		}
		
	}
}
