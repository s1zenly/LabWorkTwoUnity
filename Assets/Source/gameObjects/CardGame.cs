using System.Collections.Generic;
using System.Linq;
using CardObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class CardGame
{
    private static int cardNameCounter;

    private static CardGame instance;
    private CardLayout removed;
    public CardLayout field;

    private List<CardAsset> startedPack;

    public List<CardLayout> CardLayouts = new();

    private readonly Dictionary<CardInstance, CardView> _cardsDict = new();

    public int HandCapacity;

    public static CardGame Instance
    {
        get
        {
            instance ??= new CardGame();
            return instance;
        }
    }

    public void Init(List<CardLayout> cardLayouts, List<CardAsset> cardAssets, int handCapacity, CardLayout field, CardLayout removed)
    {
        this.field = field;
        this.removed = removed;

        HandCapacity = handCapacity;

        CardLayouts = cardLayouts;

        startedPack = cardAssets;

        StartGame();
    }

    private void CreateCardView(CardInstance instance)
    {
        GameObject newCardInstance = new($"Карта {cardNameCounter}");
        ++cardNameCounter;

        CardView cardView = newCardInstance.AddComponent<CardView>();
        Image image = newCardInstance.AddComponent<Image>();

        cardView.Init(instance, image);

        Button button = newCardInstance.AddComponent<Button>();

        button.onClick.AddListener(cardView.PlayCard);

        Transform transform = CardLayouts[instance.LayoutId].transform;

        newCardInstance.transform.SetParent(transform);

        _cardsDict[instance] = cardView;
    }

    public List<CardView> GetCardsInLayout(int id)
    {
        return _cardsDict.Where(x => x.Key.LayoutId == id).Select(x => x.Value).ToList();
    }

    private List<CardInstance> GetLayoutInstances(int id)
    {
        return _cardsDict.Where(x => x.Key.LayoutId == id).Select(x => x.Key).ToList();
    }

    private void RecalculateLayout(int id)
    {
        var cardsList = GetCardsInLayout(id);

        int position = 0;
        foreach (var cardView in cardsList)
        {
            cardView.Position = position++;
        }
    }

    public void MoveToField(CardInstance cardInstance)
    {
        int currentLayout = cardInstance.LayoutId;

        cardInstance.LayoutId = field.LayoutId;

        _cardsDict[cardInstance].transform.SetParent(field.transform);

        RecalculateLayout(currentLayout);
        RecalculateLayout(field.LayoutId);
    }

    public void MoveToRemoved(CardInstance cardInstance)
    {
        int currentLayout = cardInstance.LayoutId;

        cardInstance.LayoutId = removed.LayoutId;
        _cardsDict[cardInstance].transform.SetParent(removed.transform);
        Button button = _cardsDict[cardInstance].GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.enabled = false;
        RecalculateLayout(removed.LayoutId);
        RecalculateLayout(currentLayout);
    }

    private void MoveToLayout(CardInstance cardInstance, int id)
    {
        int currentLayout = cardInstance.LayoutId;
        cardInstance.LayoutId = id;

        Transform transform = CardLayouts[id].transform;
        _cardsDict[cardInstance].transform.SetParent(transform);

        RecalculateLayout(id);
        RecalculateLayout(currentLayout);
    }

    private void CreateCard(CardAsset cardAsset, int layout)
    {
        var instance = new CardInstance(cardAsset)
        {
            LayoutId = layout,
            CardPosition = CardLayouts[layout].amountOfCards++
        };
        CreateCardView(instance);
        MoveToLayout(instance, layout);
    }

    private void StartGame()
    {
        for (int i = 1; i < CardLayouts.Count - 1; i++)
        {
            foreach (var startCard in startedPack)
            {
                CreateCard(startCard, CardLayouts[i].LayoutId);
            }
        }
    }

    private void Shuffle(int layoutID)
    {
        Random rnd = new();

        var cards = GetLayoutInstances(layoutID);

        List<(int, int)> pairs = new();
        for (int firstCard = 0; firstCard < cards.Count; ++firstCard)
        {
            for (int secondCard = firstCard + 1; secondCard < cards.Count; ++secondCard)
            {
                pairs.Add((firstCard, secondCard));
            }
        }

        pairs = pairs.OrderBy(_ => rnd.Next()).ToList();

        for (var i = 1; i < cards.Count; ++i)
        {
            var pair_item = pairs[i].Item1;
            var item = cards[pair_item];
            _cardsDict[item].transform.SetSiblingIndex(pairs[i].Item2);
        }
    }

    public void StartTurn()
    {
        for (int i = 1; i < CardLayouts.Count -1; i++)
        {
            Shuffle(CardLayouts[i].LayoutId);

            CardLayouts[i].FaceUp = true;

            var cards = GetCardsInLayout(CardLayouts[i].LayoutId);

            for (int j = 0; j < HandCapacity; ++j)
            {
                cards[j].LayoutType = 3;
            }
        }
    }
}