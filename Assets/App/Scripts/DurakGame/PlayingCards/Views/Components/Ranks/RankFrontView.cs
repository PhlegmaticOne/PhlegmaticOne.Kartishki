using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using Kartishki.Core.Components;
using TMPro;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Ranks
{
    public class RankFrontView : MonoBehaviour, IPlayingCardViewComponent
    {
        [SerializeField] private TextMeshPro _rankText;
        
        public bool IsActive(IPlayingCardViewModel viewModel)
        {
            return viewModel.CardType is PlayingCardViewModelType.Letter && !IsAce(viewModel);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            _rankText.text = viewModel.RankView;
            _rankText.color = viewConfig.GetColor(viewModel.Color);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Release() { }
        
        private static bool IsAce(IPlayingCardViewModel viewModel)
        {
            return viewModel.Rank.Equals(RankComponent.Ace);
        }
    }
}