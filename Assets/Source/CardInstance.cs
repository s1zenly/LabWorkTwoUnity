namespace CardObjects
{
    public class CardInstance
	{
		private CardAsset cardAsset;

        public int layoutType = 1;

        public int LayoutId;

        public int CardPosition;

        public CardAsset GetCardAsset
        {
            get
            {
                return cardAsset;
            }
        }


        public CardInstance(CardAsset cardAsset)
        {
            this.cardAsset = cardAsset;
        }

    }
}

