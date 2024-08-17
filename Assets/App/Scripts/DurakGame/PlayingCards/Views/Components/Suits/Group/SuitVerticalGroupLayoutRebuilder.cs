using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Suits.Group
{
    public class SuitVerticalGroupLayoutRebuilder
    {
        private readonly Transform _parent;
        private readonly float _height;

        public SuitVerticalGroupLayoutRebuilder(Transform parent, float height)
        {
            _parent = parent;
            _height = height;
        }
        
        public void Rebuild(IReadOnlyList<SuitEntryView> suitViews)
        {
            switch (suitViews.Count)
            {
                case 0:
                    return;
                case 1:
                    ShowAtCenter(suitViews);
                    break;
                case 2:
                    ShowAtTopAndBottom(suitViews);
                    break;
                default:
                    ShowAtTopAndBottom(suitViews);
                    ShowBetween(suitViews);
                    break;
            }
        }
        
        private void ShowAtCenter(IReadOnlyList<SuitEntryView> suitViews)
        {
            ShowSuitAtPosition(suitViews[0], _parent.position);
        }

        private void ShowAtTopAndBottom(IReadOnlyList<SuitEntryView> suitViews)
        {
            var position = _parent.position;
            var topPosition = (Vector2)position + Vector2.up * _height / 2;
            var bottomPosition = (Vector2)position + Vector2.down * _height / 2;
            
            ShowSuitAtPosition(suitViews[0], topPosition);
            ShowSuitAtPosition(suitViews[^1], bottomPosition);
        }

        private void ShowBetween(IReadOnlyList<SuitEntryView> suitViews)
        {
            var position = _parent.position;
            var segmentsCount = suitViews.Count - 1;
            var topPosition = (Vector2)position + Vector2.up * _height / 2;
            var deltaMove = _height / segmentsCount;

            for (var i = 1; i <= segmentsCount - 1; i++)
            {
                var suitPosition = topPosition + Vector2.down * deltaMove * i;
                ShowSuitAtPosition(suitViews[i], suitPosition);
            }
        }

        private static void ShowSuitAtPosition(Component suitView, in Vector2 position)
        {
            suitView.transform.position = position;
        }
    }
}