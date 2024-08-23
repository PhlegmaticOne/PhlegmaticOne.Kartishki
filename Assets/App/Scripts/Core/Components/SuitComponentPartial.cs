namespace Kartishki.Core.Components
{
    public partial struct SuitComponent
    {
        private class SuitData
        {
            public readonly string Name;
            public readonly int Color;

            public SuitData(string name, int color)
            {
                Name = name;
                Color = color;
            }
        }
        
        public static SuitComponent Invalid = new('\0', string.Empty);
        
        public static SuitComponent Spades => 
            new(PlayingCardConsts.SpadesSuitChar, DefaultSuits[PlayingCardConsts.SpadesSuitChar].Name);
        
        public static SuitComponent Hearts => 
            new(PlayingCardConsts.HeartsSuitChar, DefaultSuits[PlayingCardConsts.HeartsSuitChar].Name);
        
        public static SuitComponent Diamonds => 
            new(PlayingCardConsts.DiamondsSuitChar, DefaultSuits[PlayingCardConsts.DiamondsSuitChar].Name);
        
        public static SuitComponent Clubs => 
            new(PlayingCardConsts.ClubsSuitChar, DefaultSuits[PlayingCardConsts.ClubsSuitChar].Name);
        public static SuitComponent Joker => 
            new(PlayingCardConsts.JokerSuitChar, DefaultSuits[PlayingCardConsts.JokerSuitChar].Name);
    }
}