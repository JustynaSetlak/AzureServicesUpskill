namespace Orders.Common.Results
{
    public class Result
    {
        public Result(bool isSuccessfull)
        {
            this.IsSuccessfull = isSuccessfull;
        }

        public bool IsSuccessfull { get; }

    }
}
