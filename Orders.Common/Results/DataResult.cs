using Orders.Common.Results;

namespace Orders.Results
{
    public class DataResult<T> : Result
    {
        public DataResult(bool isSuccessfull, T value) : base(isSuccessfull)
        {
            this.Value = value;
        }

        public T Value { get; }
    }
}
