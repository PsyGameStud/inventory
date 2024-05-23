using MongrelsTeam.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MongrelsTeam.Service;

namespace MongrelsTeam.UI
{
    public class ItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _textCount;

        private Item _item;
        private BaseSlot _parentSlot;
        private CanvasGroup _canvasGroup;
        private Transform _originalParent;

        private InventorySlotsWidget _inventorySlotsWidget;

        public void Setup(Item item, BaseSlot parentSlot, bool isInventory = true)
        {
            _item = item;
            _parentSlot = parentSlot;
            _image.sprite = item.ItemData.Icon;
            _textCount.text = $"{item.CurrentStack}";
            _canvasGroup = GetComponent<CanvasGroup>();
            
            if (isInventory)
            {
                _inventorySlotsWidget = parentSlot.GetComponentInParent<InventorySlotsWidget>();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UseItem();
            }
        }

        private void UseItem()
        {
            if (!_item.ItemData.IsEquiped)
            {
                _item.CurrentStack--;
            }
            else
            {
                _parentSlot.ClearSlot();
            }

            _item.Use();
            var inventory = ServiceLocator.Current.Get<Inventory>();

            if (_item.CurrentStack <= 0)
            {
                _parentSlot.ClearSlot();
            }
            else
            {
                _textCount.text = $"{_item.CurrentStack}";
            }

            inventory.RemoveItem(_item.ItemData.ItemName, 1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;
            _originalParent = transform.parent;
            transform.SetParent(_originalParent.parent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;

            if (eventData.pointerEnter != null)
            {
                EquipedSlot equipedSlot = eventData.pointerEnter.GetComponent<EquipedSlot>();
                if (equipedSlot != null && !equipedSlot.IsOccupied && _item.ItemData.IsEquiped)
                {
                    if (equipedSlot.SlotId != _item.ItemData.ItemName)
                    {
                        return;
                    }

                    equipedSlot.SetItem(_item);
                    _parentSlot?.ClearSlot();
                    return;
                }

                InventorySlot targetSlot = eventData.pointerEnter.GetComponent<InventorySlot>();
                if (targetSlot != null && !targetSlot.IsOccupied)
                {
                    targetSlot.SetItem(_item);
                    _parentSlot.ClearSlot();
                    transform.SetParent(targetSlot.transform);
                    return;
                }
            }

            RectTransform rectTransform = _inventorySlotsWidget.GetComponent<RectTransform>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
            {
                DropItem();
            }
        }

        private void DropItem()
        {
            var inventory = ServiceLocator.Current.Get<Inventory>();
            inventory.DropItem(_item);
            _parentSlot.ClearSlot();
        }
    }
}
