using System.Collections.Generic;
using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Factory
{
    public struct DurakSessionFactoryData
    {
        public List<DurakPlayer> Players { get; set; }
        public DurakSessionPoliciesConfiguration PoliciesConfiguration { get; set; }
    }
}