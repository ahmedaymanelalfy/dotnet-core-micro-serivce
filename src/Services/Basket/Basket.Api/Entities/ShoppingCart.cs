using System.Collections.Generic;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }
        public decimal TotalPrice
        {
            get { 
                decimal totalPrice = 0;
                if (this.Items!= null && this.Items.Count > 0 )
                {
                    Items.ForEach(Item =>
                     {
                         totalPrice += Item.Price * Item.Quantity;
 
                     }) ;
                }
                return totalPrice;
            }
        }
    }
}
