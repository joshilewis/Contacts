namespace Contacts.Domain
{
    public abstract class Result
    {
        public EResultCode ResultCode { get; set; }
        public string Message { get; set; }

        protected Result(string message)
        {
            ResultCode = EResultCode.Error;
            Message = message;
        }

        protected Result()
        {
            ResultCode = EResultCode.Success;
            Message = "Success";
        }

        public bool IsSuccessful => ResultCode == EResultCode.Success;
    }

    public class Result<T> : Result
    {
        public T? Payload { get; }

        public Result(string message) : base(message)
        {
            Payload = default(T);
        }

        public Result(T payload)
        {
            Payload = payload;
        }

        public Result() : base()
        {
            Payload = default(T);
        }
    }

}
