using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using api_orders.Helpers;
using api_orders.src.dto;
using api_orders.src.model;
using api_orders.src.model.interfaces;
using api_orders.src.repository;

namespace api_orders.src.service;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public ActionResult<OrdersResponseDto> GetOrdersList()
    {
        try
        {
            var ordersDto = _orderRepository.GetOrdersList();

            if (ordersDto == null || !ordersDto.Orders.Any())
                return HttpResponseHelper.NotFound(HttpMessages.Error.NOT_FOUND);

            return HttpResponseHelper.Ok(ordersDto);
        }
        catch (Exception ex)
        {
            return HttpResponseHelper.ServerError($"{HttpMessages.Error.INTERNAL_SERVER_ERROR}: {ex.Message}");
        }
    }

    public ActionResult<ProductionResponseDto> GetProduction(string email)
    {
        try
        {
            // Chama o método do repositório que busca os dados no banco
            var productionDto = _orderRepository.GetProductionByEmail(email);

            return HttpResponseHelper.Ok(productionDto);
        }
        catch (Exception ex)
        {
            return HttpResponseHelper.ServerError($"{HttpMessages.Error.INTERNAL_SERVER_ERROR}: {ex.Message}");
        }
    }

    public IActionResult SetProduction(ProductionInputDto dto)
    {
        try
        {
            var user = _orderRepository.GetUserByEmail(dto.Email);
            if (user == null)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Usuário não cadastrado!" }) { StatusCode = 400 };

            if (!DateTime.TryParse(dto.Date, out DateTime productionDateTime))
            {
               
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Formato de data inválido." }) { StatusCode = 400 };
            }

            var userStartDate = user.InitialDate ?? DateTime.MinValue; 
            var userEndDate = user.EndDate ?? DateTime.MaxValue;     

            if (productionDateTime < userStartDate || productionDateTime > userEndDate)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Data do apontamento fora do período permitido para o usuário." }) { StatusCode = 400 };

            var order = _orderRepository.GetOrderByCode(dto.Order);
            if (order == null)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Ordem de produção não encontrada." }) { StatusCode = 400 };

            if (dto.Quantity <= 0 || dto.Quantity > order.Quantity)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = $"Falha no apontamento - Quantidade deve ser maior que 0 e menor ou igual a {order.Quantity}." }) { StatusCode = 400 };

            var isMaterialValid = _orderRepository.IsMaterialInOrder(order.ProductCode, dto.MaterialCode);
            if (!isMaterialValid)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Material não pertence à lista de materiais da ordem." }) { StatusCode = 400 };

            if (dto.CycleTime <= 0)
                return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = "Falha no apontamento - Tempo de ciclo deve ser maior que 0." }) { StatusCode = 400 };

            var newProduction = new Production
            {
                Email = dto.Email,
                OrderCodeFK = dto.Order,
                Date = productionDateTime,
                Quantity = dto.Quantity,
                MaterialCode = dto.MaterialCode,
                CycleTime = dto.CycleTime
            };

            _orderRepository.InsertNewProduction(newProduction);

            var product = _orderRepository.GetProductByCode(order.ProductCode);
            if (dto.CycleTime < product.CycleTime)
            {
                return new ObjectResult(new SetProductionResponseDto { Status = 200, Type = "S", Description = "Apontamento realizado com sucesso, mas o tempo de ciclo informado é menor que o cadastrado." }) { StatusCode = 200 };
            }

            return new ObjectResult(new SetProductionResponseDto { Status = 200, Type = "S", Description = "Apontamento realizado com sucesso." }) { StatusCode = 200 };
        }
        catch (Exception ex)
        {
            return new ObjectResult(new SetProductionResponseDto { Status = 201, Type = "E", Description = $"{HttpMessages.Error.INTERNAL_SERVER_ERROR}: {ex.Message}" }) { StatusCode = 500 };
        }
    }
}
