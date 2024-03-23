using System;
using System.Collections.Generic;
using CardObjects;
using UnityEngine;

public class CardLayout : MonoBehaviour
{

    public bool FaceUp;

    internal int amountOfCards;

    public int LayoutId;

	[SerializeField] private Vector2 Offset;

    private void Update()
    {
       List<CardView> cardViews = CardGame.Instance.GetCardsInLayout(LayoutId);

        foreach (var card in cardViews)
        {
            Transform transform = card.GetComponent<Transform>();
            if (card.LayoutType == 1)
            {
                transform.localPosition = GetCardPosition(card.Position, 1);
                FaceUp = false;
                card.Rotate(FaceUp);
            }
            else if (card.LayoutType == 2)
            {
                transform.localPosition = new Vector2(0, 0);
                FaceUp = true;
                card.Rotate(FaceUp);
            }
            if (card.LayoutType == 3)
            {
                transform.localPosition = GetCardPosition(card.Position, 3);
                FaceUp = true;
                card.Rotate(FaceUp);
            }
            else if (card.LayoutType == 4)
            {
                transform.localPosition = GetCardPosition(card.Position, 4);
                FaceUp = false;
                card.Rotate(FaceUp);
            }
        }
    }

    private Vector3 GetCardPosition(int getCardPosition, int v)
    {
        Vector2 vector;
        if (v == 1)
        {
            vector = new Vector2(getCardPosition * Offset.x, 0);
        }
        else if (v == 3)
        {
            float new_position = getCardPosition * Offset.x;
            vector = new Vector2(new_position, Offset.y);
        }
        else if (v == 4)
        {
            vector = new Vector2(0, getCardPosition * Offset.y);
        }
        else
        {
            throw new ArgumentNullException();
        }
        return vector;
    }
}

