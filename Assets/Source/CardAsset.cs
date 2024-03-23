using UnityEngine;

namespace CardObjects
{
    [CreateAssetMenu(menuName ="CardGameObjects/new CardAsset", fileName ="CardAsset")]
    public class CardAsset : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public Sprite Face { get; private set; }

        [field: SerializeField] public Color color { get; private set; }

        [field: SerializeField] public Sprite Back { get; private set;}
    }
}
