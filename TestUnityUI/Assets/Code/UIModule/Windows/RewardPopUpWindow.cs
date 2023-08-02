using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Framework.Inventory;
using Framework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIModule.Windows
{
	public class RewardPopUpWindow : BaseWindow
	{
		[SerializeField] private LayoutElement _textLayoutElement;

		[SerializeField] private RectTransform _maxSizePanel;
		[SerializeField] private RectTransform _windowRect;
		
		[SerializeField] private TextMeshProUGUI _itemDescription;
		[SerializeField] private TextMeshProUGUI _itemName;
		[SerializeField] private Image _itemIcon;
		
		private const float MIN_WIDTH = 1100f;
		private float MAX_WIDTH = 2300f;
		
		private const float MIN_HEIGHT = 500f;
		private float MAX_HEIGHT = 700f;

		private bool _listeningInput;

		protected override void Awake()
		{
			base.Awake();

			var rect = _maxSizePanel.rect;
			
			MAX_WIDTH = rect.width;
			MAX_HEIGHT = rect.height;
		}
		protected override async void OnShow(object[] args)
		{
			_listeningInput = true;
			
			InventoryItem item = (InventoryItem)args[0];
			
			_itemDescription.text = item.Description;
			_itemName.text = item.Title;
			_itemIcon.sprite = item.Icon;
			
			await SetPreferredSize();
			
			//ExecuteEvents.Execute<IPointerDownHandler>(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
		}
		protected override void OnHide()
		{
			_listeningInput = false;
			
			_itemDescription.text = null;
			
			_textLayoutElement.layoutPriority = 0;
			
			_textLayoutElement.preferredWidth = MIN_WIDTH;
			_textLayoutElement.preferredHeight = MIN_HEIGHT;
		}
		private void Update()
		{
			if (_listeningInput && Input.GetMouseButtonUp(0))
			{
				base.Hide();
			}
		}
		private async UniTask SetPreferredSize()
		{
			await UniTask.NextFrame();
			
			if (_windowRect.rect.width > MAX_WIDTH && _windowRect.rect.height > MAX_HEIGHT)
			{
				_textLayoutElement.preferredWidth = MAX_WIDTH;
				_textLayoutElement.preferredHeight = MAX_HEIGHT;
				_textLayoutElement.layoutPriority = 1;
				_itemDescription.enableAutoSizing = true;
			}
			else if (_windowRect.rect.height > MAX_HEIGHT)
			{
				_textLayoutElement.preferredWidth = _windowRect.rect.width;
				_textLayoutElement.preferredHeight = MAX_HEIGHT;
				_textLayoutElement.layoutPriority = 1;
				_itemDescription.enableAutoSizing = true;
			}
			else if (_windowRect.rect.width > MAX_WIDTH)
			{
				_textLayoutElement.preferredWidth = MAX_WIDTH;
				_textLayoutElement.preferredHeight = _windowRect.rect.height;
				_textLayoutElement.layoutPriority = 1;
				_itemDescription.enableAutoSizing = true;
			}
		}
	}
}
