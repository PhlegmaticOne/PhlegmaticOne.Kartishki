using App.Scripts.Durak.Players.Models;

namespace App.Scripts.Durak.Players.Policies.FirstAttacker
{
    public interface IFirstAttackerPolicy
    {
        DurakPlayer GetFirstAttacker(FirstAttackerPolicyData policyData);
    }
}