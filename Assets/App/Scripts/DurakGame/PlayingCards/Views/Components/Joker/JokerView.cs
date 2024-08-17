using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using TMPro;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Joker
{
    public class JokerView : MonoBehaviour, IPlayingCardViewComponent
    {
        [SerializeField] private SpriteRenderer _jokerSprite;
        [SerializeField] private TextMeshPro[] _jokerTexts;

        public bool IsActive(IPlayingCardViewModel viewModel)
        {
            return viewModel.CardType is PlayingCardViewModelType.Joker;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            var color = viewConfig.GetColor(viewModel.Color);
            _jokerSprite.color = color;

            foreach (var jokerText in _jokerTexts)
            {
                jokerText.color = color;
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Release() { }
    }
}