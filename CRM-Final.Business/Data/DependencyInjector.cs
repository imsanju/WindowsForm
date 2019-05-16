namespace CRM_Final.Business.Data
{
    public class DependencyInjector
    {
        public static IProductInventoryUtility GetProductInventoryUtility()
        {
            return new DbProductInventoryUtility();
        }

        public static ICustomerUtility GetCustomerUtility()
        {
            return new DbCustomerUtility();
        }

        public static IOrderUtility GetOrderUtility()
        {
            return new DbOrderUtility();
        }

        public static IOrderItemUtility GetOrderItemUtility()
        {
            return new DbOrderItemUtility();
        }

        public static IAddressUtility GetAddressUtility()
        {
            return new DbAddressUtility();
        }


    }
}
