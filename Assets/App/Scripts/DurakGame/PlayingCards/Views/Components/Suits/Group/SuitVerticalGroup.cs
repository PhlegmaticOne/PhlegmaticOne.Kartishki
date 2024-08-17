using System.Collections.Generic;
using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Suits.Group
{
    public class SuitVerticalGroup
    {
        private readonly SuitVerticalGroupLayoutRebuilder _layoutRebuilder;
        private readonly SuitEntryView _suitEntryViewPrefab;
        private readonly Transform _parent;
        private readonly List<SuitEntryView> _suitViews;

        public SuitVerticalGroup(SuitEntryView suitEntryViewPrefab, Transform parent, float height)
        {
            _suitEntryViewPrefab = suitEntryViewPrefab;
            _parent = parent;
            _layoutRebuilder = new SuitVerticalGroupLayoutRebuilder(parent, height);
            _suitViews = new List<SuitEntryView>();
        }

        public void HandleSuitItemsUpdate(int newSuitsCount, IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            if (_suitViews is null)
            {
                return;
            }
            
            DropRedundantSuits(newSuitsCount);
            InstantiateMissingSuits(newSuitsCount);
            _layoutRebuilder.Rebuild(_suitViews);
            UpdateSuits(viewModel, viewConfig);
        }
        
        private void DropRedundantSuits(int suitsCount)
        {
            for (var i = _suitViews.Count - 1; i >= suitsCount; i--)
            {
                var suitView = _suitViews[i];
                _suitViews.RemoveAt(i);
                DestroySuitView(suitView);
            }
        }

        private void InstantiateMissingSuits(int suitsCount)
        {
            for (var i = _suitViews.Count; i < suitsCount; i++)
            {
                var suitView = Object.Instantiate(_suitEntryViewPrefab, _parent);
                _suitViews.Add(suitView);
            }
        }

        public void Release()
        {
            for (var i = _suitViews.Count - 1; i >= 0; i--)
            {
                var suitView = _suitViews[i];
                DestroySuitView(suitView);
            }
            
            _suitViews.Clear();
        }

        internal static void DestroySuitViews(IEnumerable<SuitEntryView> suitEntryViews)
        {
            foreach (var entryView in suitEntryViews)
            {
                DestroySuitView(entryView);
            }
        }

        private static void DestroySuitView(Component suitView)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(suitView.gameObject);
            }
            else
            {
                Object.DestroyImmediate(suitView.gameObject);
            }
        }

        private void UpdateSuits(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            foreach (var suitView in _suitViews)
            {
                var rotation = suitView.transform.position.y < 0 ? 180 : 0;
                suitView.transform.rotation = Quaternion.Euler(0, 0, rotation);
                suitView.UpdateView(viewModel, viewConfig);
            }
        }
    }
}