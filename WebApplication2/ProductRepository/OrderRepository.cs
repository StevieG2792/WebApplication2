﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.CompilerServices;
using WebApplication2.DBContexts;
using WebApplication2.Models.Orders;
using static WebApplication2.Models.Orders.RetrieveOrderDto;

namespace WebApplication2.ProductRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductContext _dbContext;

        public OrderRepository(ProductContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IActionResult InsertOrder(CreateOrderDto orderDto)
        {
            var order = new Order()
            {
                Customer = orderDto.Customer,
                Address = orderDto.Address,
                Status = "Active",
                OrderDetails = (from item in orderDto.Details
                                select new OrderDetail()
                                { ProductId = item.ProductId, Quantity = item.Quantity }).ToList()
            };

            _dbContext.Add(order);
            Save();
            return new OkObjectResult("Order Saved, ID: " + order.Id);
        }

        public void UpdateOrderAddress(string address, int orderId)
        {
            var order = new Order() { Address = address, Id = orderId };
            _dbContext.Orders.Attach(order).Property(x => x.Address).IsModified = true;
            Save();
        }

        public IActionResult UpdateOrder(int orderId, UpdateOrderDto orderDto)
        {
            var checkStatus = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId && x.Status != "Completed"); //We won't update submitted orders
            if (checkStatus == null) 
            {
                return new BadRequestObjectResult("Order Id: " + orderId + " has already been processed for delivery."); 
            }

            var orderDetails = (from item in orderDto.Details
                                        select new OrderDetail()
                                        { ProductId = item.ProductId, Quantity = item.Quantity, OrderId = orderId }).ToList();

            foreach (OrderDetail detail in orderDetails)
            {
                var originalItem = _dbContext.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == detail.ProductId);
                if (originalItem != null)
                {
                    originalItem.Quantity = detail.Quantity;
                }
                else
                {
                    _dbContext.OrderDetails.Add(detail);
                }
            }

            Save();
            return new OkObjectResult("Order Id: " + orderId + " has been updated.");
        }

        public void CancelOrder(int orderId)
        {
            var order = new Order() { Status = "Cancelled", Id = orderId };
            _dbContext.Orders.Attach(order).Property(x => x.Status).IsModified = true;
            Save();
        }

        public void SubmitOrder(int orderId)
        {
            var order = new Order() { Status = "Completed", Id = orderId };
            _dbContext.Orders.Attach(order).Property(x => x.Status).IsModified = true;
            Save();
        }

        public IEnumerable<RetrieveOrderDto> GetOrderByPage(int page)
        {
            int RecordsToSkip = (page - 1) * 2; //As 2 records per page
            var order = _dbContext.Orders.Include("OrderDetails.Product").OrderBy(b => b.Id).Skip(RecordsToSkip).Take(2).ToList();
            var orderInfo = new List<RetrieveOrderDto>();
            foreach (var item in order)
            {
                orderInfo.Add(new RetrieveOrderDto
                {
                    Customer = item.Customer,
                    Address = item.Address,
                    Status = item.Status,
                    Details = from d in item.OrderDetails
                              select new RetrieveOrderDto.Detail()
                              {
                                  ProductId = d.Product.Id,
                                  Product = d.Product.Name,
                                  Price = d.Product.Price,
                                  Quantity = d.Quantity
                              }
                }); 
              
            };

            return orderInfo;
        }

        public RetrieveOrderDto GetOrderByID(int orderId)
        {
            Order order = _dbContext.Orders.Include("OrderDetails.Product")
            .First(o => o.Id == orderId);

            return new RetrieveOrderDto()
            {
                Customer = order.Customer,
                Address = order.Address,
                Status = order.Status,
                Details = from d in order.OrderDetails
                          select new RetrieveOrderDto.Detail()
                          {
                              ProductId = d.Product.Id,
                              Product = d.Product.Name,
                              Price = d.Product.Price,
                              Quantity = d.Quantity
                          }
            };


        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
