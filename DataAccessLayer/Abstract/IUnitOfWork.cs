using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        ICartRepository CartRepository { get; }

        IOrderRepository OrderRepository { get; }

        void Save();
    }
}
