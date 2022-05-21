namespace AirPurity.API.DTOs
{
    public class ResponseModel
    {
        public ResponseModel(bool success = true, string message = null, object data = null)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
