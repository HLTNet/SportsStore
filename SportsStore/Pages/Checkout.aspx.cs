using SportsStore.Models;
using SportsStore.Models.Respository;
using SportsStore.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SportsStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            checkoutForm.Visible = true;
            checkoutMessage.Visible = false;
            if(IsPostBack)
            {
                Order order = new Order();
                if (TryUpdateModel(order, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    order.OrderLines = new List<OrderLine>();

                    Cart myCart = SessionHelper.GetCart(Session);
                    foreach(CartLine line in myCart.Lines)
                    {
                        order.OrderLines.Add(new OrderLine
                        {
                            Order = order,
                            Product = line.Product,
                            Quantity = line.Quantity
                        });
                    }

                    new Repository().SaveOrder(order);
                    myCart.Clear();

                    checkoutForm.Visible = false;
                    checkoutMessage.Visible = true;
                }
            }

        }
    }
}