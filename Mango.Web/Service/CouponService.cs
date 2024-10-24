using Mango.Services.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
                _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data= couponDto,
                Url = SD.CouponApiBase + "/api/Coupon"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponAsync()
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string CouponCode)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/GetByCode" + CouponCode
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }
    }
}
