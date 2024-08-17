namespace App.Scripts.Durak.Handlers.Transfer.Policies
{
    public interface ITransferPolicy
    {
        bool CanTransfer(TransferPolicyData policyData);
    }
}