using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IValidator<T>
    {
        string ErrorMessage { get; set; }

        bool Validation(T entity);
    }
}
