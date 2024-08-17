using App.Scripts.DurakGame.PlayingCards.Views.Components.Suits;
using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using TMPro;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Ranks
{
    public class RankCornerView : MonoBehaviour, IPlayingCardViewComponent
    {
        [SerializeField] private TextMeshPro _rankText;
        [SerializeField] private SuitEntryView _suitEntryView;

        public bool IsActive(IPlayingCardViewModel viewModel)
        {
            return viewModel.CardType is PlayingCardViewModelType.Letter or PlayingCardViewModelType.Numeric;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            _rankText.text = viewModel.RankView;
            _rankText.color = viewConfig.GetColor(viewModel.Color);
            _suitEntryView.UpdateView(viewModel, viewConfig);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Release() { }
    }
}