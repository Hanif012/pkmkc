using UnityEngine;

public class OrderClass : MonoBehaviour
{
    public class Order
    {
        public string orderName;
        public int orderNumber;
        public string orderDescription;
        public enum OrderStatus
        {
            Pending,
            Completed,
            Failed
        }
        public enum OrderType
        {
            FakeOrder,
            RealOrder
        }
    }

    // public OrderClass order;
    // public Order.OrderStatus orderStatus;
    // public Order.OrderType orderType;

}