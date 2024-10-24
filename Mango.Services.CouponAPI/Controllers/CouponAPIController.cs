using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDtocs;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _responseDtocs = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objlist=_db.Coupons.ToList();
                 _responseDtocs.Result= _mapper.Map<IEnumerable<CouponDto>>(objlist);
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess=false;
                _responseDtocs.Message=ex.Message;
            }
            return _responseDtocs;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x=>x.CouponId==id);
                
                _responseDtocs.Result = _mapper.Map<CouponDto>(obj); 
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess = false;
                _responseDtocs.Message = ex.Message;
            }
            return _responseDtocs;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
                if(obj==null)
                    _responseDtocs.IsSucess=false;

                _responseDtocs.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess = false;
                _responseDtocs.Message = ex.Message;
            }
            return _responseDtocs;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj=_mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
               _db.SaveChanges();

                _responseDtocs.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess = false;
                _responseDtocs.Message = ex.Message;
            }
            return _responseDtocs;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _responseDtocs.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess = false;
                _responseDtocs.Message = ex.Message;
            }
            return _responseDtocs;
        }

        [HttpDelete]
		[Route("{id:int}")]
		public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.FirstOrDefault(x => x.CouponId == id);  
                _db.Coupons.Remove(obj);
                _db.SaveChanges();

                _responseDtocs.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDtocs.IsSucess = false;
                _responseDtocs.Message = ex.Message;
            }
            return _responseDtocs;
        }

    }
}
