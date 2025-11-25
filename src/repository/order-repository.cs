using api_orders.src.data;
using api_orders.src.dto;
using api_orders.src.model;
using api_orders.src.model.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_orders.src.repository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public OrdersResponseDto GetOrdersList()
    {
        var orders = _context.Orders
        .Select(o => new OrderOutputDto
        {
            Order = o.OrderCode,
            Quantity = o.Quantity,

            ProductCode = o.Product.ProductCode,
            ProductDescription = o.Product.Description,
            Image = o.Product.Image,
            CycleTime = o.Product.CycleTime,

            Materials = o.Product.ProductMaterials
                .Select(pm => new MaterialOutputDto
                {
                    MaterialCode = pm.Material.MaterialCode,
                    MaterialDescription = pm.Material.Description
                })
                .ToList()
        })
        .ToList();

        return new OrdersResponseDto { Orders = orders };
    }

    public ProductionResponseDto GetProductionByEmail(string email)
    {
        var productions = _context.Productions
            .Where(p => p.Email == email)
            .Select(p => new ProductionOutputDto
            {
                Order = p.OrderCodeFK, 
                Date = p.Date,
                Quantity = p.Quantity ?? 0, 
                MaterialCode = p.MaterialCode,
                CycleTime = p.CycleTime ?? 0 
            })
            .ToList();

        return new ProductionResponseDto
        {
            Productions = productions
        };
    }

    public void InsertNewProduction(Production newProduction)
    {
        _context.Productions.Add(newProduction);
        _context.SaveChanges();
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public Order GetOrderByCode(string orderCode)
    {
        return _context.Orders.FirstOrDefault(o => o.OrderCode == orderCode);
    }

    public Product GetProductByCode(string productCode)
    {
        return _context.Products.FirstOrDefault(p => p.ProductCode == productCode);
    }

    public bool IsMaterialInOrder(string productCode, string materialCode)
    {
        return _context.ProductMaterials.Any(pm => pm.ProductCode == productCode && pm.MaterialCode == materialCode);
    }
}