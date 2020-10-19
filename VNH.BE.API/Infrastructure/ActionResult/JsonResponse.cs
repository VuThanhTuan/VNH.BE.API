using Newtonsoft.Json;

namespace VNH.BE.API.Infrastructure.ActionResult
{
    public class JsonResponse<T>
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "developerMessage")]
        public string DeveloperMessage { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public Metadata Meta { get; set; }

        public JsonResponse(int statusCode, string message = "", string developerMessage = "", int? pageIndex = null,
            int? pageSize = null, long? count = null, T data = default(T))
        {
            StatusCode = statusCode;
            Message = message;
            DeveloperMessage = developerMessage;
            Meta = new Metadata
            {
                PageIndex = pageIndex.GetValueOrDefault(0),
                PageSize = pageSize.GetValueOrDefault(0),
                Count = count.GetValueOrDefault(0)
            };
            Data = data;
        }
    }

    public class Metadata
    {
        [JsonProperty(PropertyName = "pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        [JsonProperty(PropertyName = "count")]
        public long Count { get; set; }
    }
}
