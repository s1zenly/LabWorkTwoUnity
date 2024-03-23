using System.Collections.Generic;
using CardObjects;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    [SerializeField] private List<CardAsset> cardAssets;
    [SerializeField] private List<CardLayout> cardLayouts;

    [SerializeField] private int handCapacity;


    private void Start()
    {
        int id = 0;
        foreach (var cardLayout in cardLayouts)
        {
            cardLayout.LayoutId = id++;
        }
        CardGame.Instance.Init(cardLayouts, cardAssets, handCapacity, cardLayouts[0], cardLayouts[cardLayouts.Count - 1]);
        CardGame.Instance.StartTurn();
    }
}

