using System.Collections;
using System.Collections.Generic;
using broccoli.Controller;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

namespace broccoli.Cards
{
    public class Card : MonoBehaviour
    {
        [Header("Card Visuals")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _hiddenIconSprite;

        private Sprite _iconSprite;
        private bool _isSelected;
        private CardController _controller;

        /// <summary>
        /// The controller managing this card. Set by the CardController.
        /// </summary>
        public CardController Controller
        {
            get => _controller;
            set => _controller = value;
        }

        /// <summary>
        /// The sprite shown when the card is revealed.
        /// </summary>
        public Sprite IconSprite => _iconSprite;

        /// <summary>
        /// Whether the card is currently selected/revealed.
        /// </summary>
        public bool IsSelected => _isSelected;

        /// <summary>
        /// Assigns the icon sprite for this card.
        /// </summary>
        public void SetIconSprite(Sprite sprite)
        {
            _iconSprite = sprite;
        }

        #region Unity Methods
        private void Start()
        {
            var button = GetComponent<Button>();
            if (button != null)
                button.onClick.AddListener(OnCardClick);
        }
        #endregion

        /// <summary>
        /// Reveals the card's icon.
        /// </summary>
        public void Show()
        {
            if (_iconImage == null && _iconSprite == null)
                return;
                

            Tween.Rotation(transform,
                new Vector3(0f, 180f, 0f),
                0.2f);

            Tween.Delay(0.1f, () =>
            {
                _iconImage.sprite = _iconSprite;
                _isSelected = true;
            });
        }

        /// <summary>
        /// Hides the card's icon (shows the back).
        /// </summary>
        public void Hide()
        {
            if (_iconImage == null && _hiddenIconSprite == null)
                return;
            
            Tween.Rotation(transform, new Vector3(0f, 0.1f, 0f), 0.2f);
            Tween.Delay(0.1f, () =>
            {
                _iconImage.sprite = _hiddenIconSprite;
                _isSelected = false;
            });
                
        }

        /// <summary>
        /// Handles the card click event.
        /// </summary>
        private void OnCardClick()
        {
            _controller?.SetSelected(this);
        }
    }
}

