using api_orders.src.dto;
using api_orders.src.model;
using api_orders.src.model.interfaces;
using api_orders.src.service;
using Microsoft.AspNetCore.Mvc;

namespace api_orders.src.controllers;

[ApiController]
[Route("api/[controller]")]
public class ordersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public ordersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("getOrders")]
    public ActionResult<OrdersResponseDto> GetOrders()
    {
        return _orderService.GetOrdersList();
    }

    [HttpGet("getProduction")]
    public ActionResult<ProductionResponseDto> GetProduction(string email)
    {
        return _orderService.GetProduction(email);
    }

    [HttpPost("setProduction")]
    public IActionResult SetProduction([FromBody] ProductionInputDto dto)
    {
        return _orderService.SetProduction(dto);
    }
}
