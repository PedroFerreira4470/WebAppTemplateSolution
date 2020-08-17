namespace Application.Common.Contracts.V1.ResponseType
{
    public class Response<T>
    {
        public Response(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
