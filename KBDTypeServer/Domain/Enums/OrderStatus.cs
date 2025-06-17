namespace KBDTypeServer.Domain.Enums
{
    public enum OrderStatus
    {
        Created,      // Створено
        WaitingForPayment, // Очікує на оплату
        Paid,         // Оплачено
        WhatingForShipping, // Очікує на відправку
        Shipped,      // Відправлено
        Delivered,    // Доставлено
        WaitingForRefund, // Очікує повернення
        Refunded,      // Повернено
        Cancelled,     // Скасовано
        Success,     // Успішно 
        Failed      // Помилка

    }
}
