namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        //[Identity]
        public string UserName { get; set; }//PK 
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }
        public ShoppingCart()
        {

        }
    }
}
