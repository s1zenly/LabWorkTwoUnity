using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CardObjects {
	public class CardView : MonoBehaviour
	{
        private Image _image;

        private CardInstance _cardInstance;

        public int LayoutType
        {
            get => _cardInstance.layoutType;
            set => _cardInstance.layoutType = value;
        }

        public int Position
        {
            get => _cardInstance.CardPosition;
            set => _cardInstance.CardPosition = value;
        }

        public void Rotate(bool up)
        {
            _image.sprite = up ? _cardInstance.GetCardAsset.Face : _cardInstance.GetCardAsset.Back;
        }

        public void Init(CardInstance cardInstance, Image image)
		{
            _image = image;

            _cardInstance = cardInstance;
        }

		public void PlayCard()
		{
            if (_cardInstance.layoutType == 2)
            {
                _cardInstance.layoutType = 4;
                CardGame.Instance.MoveToRemoved(_cardInstance);
            }
            else if (_cardInstance.layoutType == 3)
			{
                _cardInstance.layoutType = 2;
                CardGame.Instance.MoveToField(_cardInstance);
			}
        }
	}
}

