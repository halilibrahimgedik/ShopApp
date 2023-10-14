using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
           this.unitOfWork = unitOfWork;
        }

        public void Create(Order Entity)
        {
            unitOfWork.OrderRepository.Add(Entity);
            unitOfWork.Save();
        }

        public List<Order> GetOrders(string userId)
        {
            return unitOfWork.OrderRepository.GetOrders(userId);
        }
    }
}
