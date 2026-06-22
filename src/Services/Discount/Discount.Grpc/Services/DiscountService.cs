using Discount.gRPC;
using Discount.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Mapster;
namespace Discount.Grpc.Services
{
    public class DiscountService
        (
        DiscountContxt _context,
        ILogger<DiscountService> _logger
        ):DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            //var coupons = await _context.Copuns.ToListAsync();
            var coupon = await _context.Copuns.FirstOrDefaultAsync(d => d.ProductName == request.ProductName);
            _logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}",
                request.ProductName, coupon?.Amount);
            if (coupon == null)
            {
                coupon = new Models.Copun
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount Desc",
                };
            }
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            return base.CreateDiscount(request, context);
        }
        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }
        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
