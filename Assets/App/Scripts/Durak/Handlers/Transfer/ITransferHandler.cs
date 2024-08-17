using App.Scripts.Durak.Handlers.Transfer.Results;

namespace App.Scripts.Durak.Handlers.Transfer
{
    public interface ITransferHandler
    {
        TransferResult Handle(in TransferHandlerData handlerData);
    }
}