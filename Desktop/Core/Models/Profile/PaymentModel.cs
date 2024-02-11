namespace Metflix.Core.Models;

public class PaymentModel
{
    private DateTime _dateTime;
    private string _description;
    private string _period;
    private ViewPaymentModel _paymentModel;
    private string _paymentAmmount;
    private string _resultPayment;

    public PaymentModel(DateTime dateTime, string description, string period, ViewPaymentModel paymentModel, string paymentAmmount, string resultPayment)
    {
        _dateTime = dateTime;
        _description = description;
        _period = period;
        _paymentModel = paymentModel;
        _paymentAmmount = paymentAmmount;
        _resultPayment = resultPayment;
    }

    public DateTime DateTime
    {
        get => _dateTime;
        set => _dateTime = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public string Period
    {
        get => _period;
        set => _period = value;
    }

    public ViewPaymentModel PaymentCardModel
    {
        get => _paymentModel;
        set => _paymentModel = value;
    }

    public string PaymentAmmount
    {
        get => _paymentAmmount;
        set => _paymentAmmount = value;
    }

    public string ResultPayment
    {
        get => _resultPayment;
        set => _resultPayment = value;
    }
}
