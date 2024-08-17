namespace App.Scripts.Durak.Handlers.Transfer.Results
{
    public enum TransferFailureReason
    {
        None = 0,
        PlayerIsNotDefender = 1,
        InvalidTransferCard = 2,
        InvalidGameState = 3
    }
}