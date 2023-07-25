using AutoMapper.Internal;
using Magic_Villa_Web.Models;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse  responseModel { get; set;}
        Task<T> SendAsync<T>(APIRequest apirequest);
    }
}
