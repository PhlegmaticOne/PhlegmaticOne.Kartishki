using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Configs
{
    [CreateAssetMenu(menuName = "Configs/Cards/Playing Card View Config", fileName = nameof(PlayingCardViewConfig))]
    public class PlayingCardViewConfig : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<int, SuitFrontViewConfigData> _suitsViewData;
        [SerializeField] private Dictionary<int, Color> _colorMap;

        public Color GetColor(int colorType)
        {
            return _colorMap[colorType];
        }
        
        public SuitFrontViewConfigData GetSuitDataForRank(int rank)
        {
            return _suitsViewData[rank];
        }
    }
}