namespace SharedLibrary.MassTransit.RabbitMQ;

public class PaymentRequestMsg
{
    int TransactionId { get; set; }
    string PaymentInfo { get; set; }
}