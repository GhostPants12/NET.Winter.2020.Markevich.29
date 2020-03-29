using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BLL.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T obj);
    }
}
