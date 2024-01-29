using AutoMapper;
using CouponService.Data;
using CouponService.Models;
using CouponService.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponService.Controllers
{
    [Route("api/CouponAPI")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IList<Coupon> objList = _appDbContext.Coupons.ToList();
                _responseDto.Result = _mapper.Map<IList<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(x => x.CouponId == id);
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
        [HttpGet]
        [Route("GetByCode /{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
               
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
        [HttpPost]
        public ResponseDto Post(CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDbContext.Add(obj);
                _appDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
        [HttpPut]
        public ResponseDto Put(CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDbContext.Update(obj);
                _appDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _appDbContext.Coupons.First(x=>x.CouponId == id);
                _appDbContext.Remove(obj);
                _appDbContext.SaveChanges();
                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;

            }
            return _responseDto;

        }
    }
}
