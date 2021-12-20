using System;
using System.Threading.Tasks;
using MangoWeb.Models;

namespace MangoWeb.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ResponseDto ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}