using Discount.gRPC;
using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
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
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Copun>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            _context.Copuns.Add(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Copun>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            _context.Copuns.Update(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _context
             .Copuns
             .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            _context.Copuns.Remove(coupon);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
