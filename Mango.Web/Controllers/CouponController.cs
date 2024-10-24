using Mango.Services.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto?> List = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSucess)
            {
                List = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(List);
        }

		
		public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSucess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
			ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

			if (response != null && response.IsSucess)
			{
				CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
			}
            else
            {
                TempData["error"] = response?.Message;
            }

            return NotFound();
        }

		[HttpPost]
		public async Task<IActionResult> CouponDelete(CouponDto model)
		{
			ResponseDto? response = await _couponService.DeleteCouponAsync(model.CouponId);

			if (response != null && response.IsSucess)
			{
                TempData["success"] = "Coupon deleted successfully";
                return RedirectToAction(nameof(CouponIndex));
			}
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
		}
	}
}
