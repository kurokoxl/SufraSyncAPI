namespace SufraSyncAPI.Models.Entities
{
    public enum OrderStatus
    {
        Pending = 1,    // Order placed
        Ready = 2,      // Waiting for pickup/driver
        Delivered = 3,  // Customer has it
        Cancelled = 4   // Order stopped
    }
}