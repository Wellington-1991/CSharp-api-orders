using api_orders.src.dto;
using api_orders.src.model;
using Microsoft.AspNetCore.Mvc;

namespace api_orders.src.model.interfaces;

public interface IOrderService
{
    ActionResult<OrdersResponseDto> GetOrdersList();
    ActionResult<ProductionResponseDto> GetProduction(string email);
    IActionResult SetProduction(ProductionInputDto newProduction);
}
