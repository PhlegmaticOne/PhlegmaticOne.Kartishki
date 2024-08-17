namespace App.Scripts.Durak.Handlers.Attacking.Siege.Policies
{
    public interface ISiegePolicy
    {
        bool CanBesiege(SiegePolicyData policyData);
    }
}