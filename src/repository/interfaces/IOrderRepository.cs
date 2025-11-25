using api_orders.src.dto;
using api_orders.src.model;
using Microsoft.AspNetCore.Mvc;

namespace api_orders.src.model.interfaces;

public interface IOrderRepository
{
    OrdersResponseDto GetOrdersList();
    ProductionResponseDto GetProductionByEmail(string email);
    void InsertNewProduction(Production newProduction);
    User GetUserByEmail(string email);
    Order GetOrderByCode(string orderCode);
    Product GetProductByCode(string productCode);
    bool IsMaterialInOrder(string productCode, string materialCode);
}
