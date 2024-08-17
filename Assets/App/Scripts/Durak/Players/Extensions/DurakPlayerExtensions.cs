using System.Collections.Generic;
using App.Scripts.Durak.Players.Base;
using Kartishki.Core;

namespace App.Scripts.Durak.Players.Extensions
{
    public static class DurakPlayerExtensions
    {
        public static T PushCards<T>(this T player, params PlayingCard[] cards)
            where T : IPlayingCardConsumer
        {
            return PushCards(player, cards as IEnumerable<PlayingCard>);
        }
        
        public static T PushCards<T>(this T player, IEnumerable<PlayingCard> cards)
            where T : IPlayingCardConsumer
        {
            foreach (var card in cards)
            {
                player.PushCard(card);
            }

            return player;
        }
    }
}