using System;
using System.Collections.Generic;

namespace Metflix.Core.Models;

public class PaymentDataModel
{
    private PlanModel _plan;
    private DateTime _nextPaymentDate;
    private List<PaymentModel> _payments;

    public PaymentDataModel(PlanModel plan, DateTime nextPaymentDate, List<PaymentModel> payments)
    {
        _plan = plan;
        _nextPaymentDate = nextPaymentDate;
        _payments = payments;
    }

    public PlanModel Plan
    {
        get => _plan;
        set => _plan = value;
    }

    public DateTime NextPaymentDate
    {
        get => _nextPaymentDate;
        set => _nextPaymentDate = value;
    }

    public List<PaymentModel> Payments
    {
        get => _payments;
        set => _payments = value;
    }
}