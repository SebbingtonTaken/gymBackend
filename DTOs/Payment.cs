
namespace DTOs
{
    public class Payments : BaseDTO
    {

        public int CustomerID { get; set; }
        public double? RegistrationAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public double? CoupongAmount { get; set; }
        public double TotalAmount { get; set; }
        public char PaymentStatus { get; set; }
        public Membership UserMembership { get; set; }
       // public File? ReceiptFile { get => receiptFile; set => receiptFile = value; }



    }
}
