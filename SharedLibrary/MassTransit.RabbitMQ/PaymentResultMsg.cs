namespace SharedLibrary.MassTransit.RabbitMQ;

public class PaymentResultMsg
{
    int TransactionId { get; set; }
    bool Result { get; set; }
    string? FailureReason { get; set; }
}