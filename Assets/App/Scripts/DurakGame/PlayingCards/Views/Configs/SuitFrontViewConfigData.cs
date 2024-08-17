using System;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Configs
{
    [Serializable]
    public struct SuitFrontViewConfigData
    {
        [SerializeField] private int _leftGroupSuitsCount;
        [SerializeField] private int _centerGroupSuitsCount;
        [SerializeField] private int _rightGroupSuitsCount;
        
        public SuitFrontViewConfigData(int leftGroupSuitsCount, int centerGroupSuitsCount, int rightGroupSuitsCount)
        {
            _leftGroupSuitsCount = leftGroupSuitsCount;
            _centerGroupSuitsCount = centerGroupSuitsCount;
            _rightGroupSuitsCount = rightGroupSuitsCount;
        }

        public int LeftGroupSuitsCount => _leftGroupSuitsCount;
        public int CenterGroupSuitsCount => _centerGroupSuitsCount;
        public int RightGroupSuitsCount => _rightGroupSuitsCount;
    }
}