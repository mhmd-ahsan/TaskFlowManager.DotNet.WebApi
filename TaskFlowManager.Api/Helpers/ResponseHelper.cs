namespace TaskFlowManager.Api.Helpers
{
    public class ResponseHelper<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }


        public static ResponseHelper<T> Ok(T data, string? message = "")
        {
            return new ResponseHelper<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseHelper<T> Fail(string message)
        {
            return new ResponseHelper<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}