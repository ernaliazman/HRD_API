namespace hrd_backend.Model.API
{
    public class APIResponse<T>
    {
        public int status_code { get; set; }

        public string message { get; set; }

        public T result { get; set; }
    }
}
