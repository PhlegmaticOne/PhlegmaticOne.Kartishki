using System.Collections.Generic;
using App.Scripts.Cards;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views
{
    public class PlayingCardEditorPreview : SerializedMonoBehaviour
    {
        [SerializeField] private IPlayingCardView _view;

        [SerializeField] private bool _isJoker;

        [ShowIf(nameof(_isJoker))] 
        [ValueDropdown(nameof(GetJokerColors))]
        [SerializeField] private int _jokerColor;
        
        [HideIf(nameof(_isJoker))] 
        [ValueDropdown(nameof(GetRankStringValues))]
        [SerializeField] private string _rank;

        [HideIf(nameof(_isJoker))] 
        [ValueDropdown(nameof(GetSuitStringValues))] 
        [SerializeField] private string _suit;

        [Button]
        public void PreviewCard()
        {
            var card = CreateCard();
            var viewModel = PlayingCardViewModel.FromCard(card);
            _view.UpdateView(viewModel);
        }

        [Button]
        public void Release()
        {
            _view.Release();
        }

        private PlayingCard CreateCard()
        {
            return _isJoker ? 
                PlayingCard.Create().Joker().WithColor(_jokerColor) :
                PlayingCard.Parse(_rank + _suit);
        }

        private static ValueDropdownList<int> GetJokerColors()
        {
            return new ValueDropdownList<int>
            {
                new ValueDropdownItem<int>("Red", PlayingCardConsts.RedColor),
                new ValueDropdownItem<int>("Black", PlayingCardConsts.BlackColor)
            };
        }

        private static IEnumerable<string> GetRankStringValues()
        {
            for (var i = 2; i <= 10; i++)
            {
                yield return i.ToString();
            }

            yield return PlayingCardConsts.Joker;
            yield return PlayingCardConsts.Queen;
            yield return PlayingCardConsts.King;
            yield return PlayingCardConsts.Ace;
        }

        private static IEnumerable<string> GetSuitStringValues()
        {
            yield return PlayingCardConsts.SpadesSuitChar.ToString();
            yield return PlayingCardConsts.HeartsSuitChar.ToString();
            yield return PlayingCardConsts.DiamondsSuitChar.ToString();
            yield return PlayingCardConsts.ClubsSuitChar.ToString();
        }
    }
}