using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using TMPro;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Suits
{
    public class SuitEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _suitText;

        public void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            _suitText.text = viewModel.SuitView;
            _suitText.color = viewConfig.GetColor(viewModel.Color);
        }
    }
}