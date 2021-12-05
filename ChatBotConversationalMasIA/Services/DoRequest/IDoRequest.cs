using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.DoRequest
{
    public interface IDoRequest{
        Task<T> Get<T>(string path, IDictionary<string, string> headers);
        Task<T> Post<T>(string path, IDictionary<string, string> headers, string body, string type = "application/json");
        Task<T> Post<T>(string path, IDictionary<string, string> headers, IDictionary<string, string> form);
    }
}