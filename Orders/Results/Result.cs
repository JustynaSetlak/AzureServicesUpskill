using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Results
{
    public class Result<T>
    {
        public Result(bool isSuccessfull)
        {
            this.IsSuccessfull = isSuccessfull;
        }

        public Result(bool isSuccessfull, T value) : this(isSuccessfull)
        {
            this.Value = value;
        }

        public bool IsSuccessfull { get; }

        public T Value { get; }
    }
}
