using Newtonsoft.Json;

namespace DTO.O
{
    public class ErrorDTO
    {

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}