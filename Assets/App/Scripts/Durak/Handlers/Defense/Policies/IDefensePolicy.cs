namespace App.Scripts.Durak.Handlers.Defense.Policies
{
    public interface IDefensePolicy
    {
        bool CanDefend(DefensePolicyData policyData);
    }
}