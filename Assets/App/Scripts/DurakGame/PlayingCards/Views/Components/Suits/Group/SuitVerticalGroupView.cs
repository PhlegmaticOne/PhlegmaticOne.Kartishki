using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Suits.Group
{
    public class SuitVerticalGroupView : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private SuitEntryView _suitEntryViewPrefab;

        private SuitVerticalGroup _suitVerticalGroup;

        public void Construct()
        {
            _suitVerticalGroup ??= new SuitVerticalGroup(_suitEntryViewPrefab, transform, _height);
        }
        
        public void ShowSuitsCount(int suitsCount, IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            _suitVerticalGroup.HandleSuitItemsUpdate(suitsCount, viewModel, viewConfig);
        }

        public void Release()
        {
            if (!Application.isPlaying)
            {
                SuitVerticalGroup.DestroySuitViews(GetComponentsInChildren<SuitEntryView>());
            }
            else
            {
                _suitVerticalGroup.Release();
            }
        }
    }
}