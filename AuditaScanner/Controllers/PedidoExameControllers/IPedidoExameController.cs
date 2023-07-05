using AuditaScanner.Models.PedidosExameModels;

namespace AuditaScanner.Controllers.PedidoExameControllers;

public interface IPedidoExameController
{
    Task<TResponse> GetPedidosExames<TResponse>(int idpedido, string TipoPedidoExame);
}
