////////////////////////////////////////////////
// © https://github.com/badhitman - @fakegov
////////////////////////////////////////////////
using QIWI.json;
using System;

namespace QIWI_API
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QIWI_API_Class q_raw = new QIWI_API_Class("f98sf8d7f6s9d8f7sd9f6s9df6"); // <сюда введите свой токен API>
            QiwiPaymentsDataClass payments = q_raw.PaymentHistory(); // Получить историю транзакций
            QiwiTotalPaymentsClass total_payments = q_raw.PaymentsTotal(DateTime.Now.AddDays(-90), DateTime.Now); // Получить оборотную суммарную статистику за интересующий период
            QiwiPaymentClass transaction_info = q_raw.TransactionIdInfo(12158385869, "OUT"); // Получить информацию о транзакции
            QiwiCurrentBalanceClass cur_balance = q_raw.CurrentBalance; // Получить информацию о балансе кошелька
            QiwiCommissionByProviderClass CommissionByProvider = q_raw.CommissionByProvider("1963"); // Получить тариф на перевод по коду провайдера
            QiwiResponseCalculationOnlineCommissionClass CalculationOnlineCommission = q_raw.CalculationOnlineCommission("1963", "4444555577773333", 10); // Рассчитать точную коммиссию по полным реквизитам
            ////////////////////////////////////////////////////////////////////
            QiwiTransactionsUniversalDetailsPaymentClass details_universal_transaction = new QiwiTransactionsUniversalDetailsPaymentClass(); // Этот набор реквизитов универсален. Подходит для перевода на QIWI, для пополнения баланса, для перевода на карту банка и другие переводы, которые требуют один реквизит [номер получателя]
            details_universal_transaction.comment = "Отправка денег на QIWI"; // комментарий
            details_universal_transaction.sum.amount = 0.5; // сумма
            details_universal_transaction.fields = new QiwiFieldsPaymentUniversalDirectClass() { account = "79672570993" }; // такой формат подойдёт для пеервода на QIWI, на карту банка или для пополнения баланса телефона
            QiwiResultSendUniversalPaymentClass SendUniversalPayment = q_raw.SendPayment("99", details_universal_transaction); // отправить платёж. Получатель для пополнения баланса мобильного телефона без семёрки/восмёрки (формат: 9995554422). Для перевода на киви в с семёркой (формат: 79995554422). Либо номер карты и т.п.
            //
            details_universal_transaction = new QiwiTransactionsUniversalDetailsPaymentClass(); 
            details_universal_transaction.comment = "Отправка денег на карту сбербанка. "; // Код провайдера предварительно определил при помощи метода DetectCachCardProvider
            details_universal_transaction.sum.amount = 50; 
            details_universal_transaction.fields = new QiwiFieldsPaymentUniversalDirectClass() { account = "4444555577773333" }; 
            SendUniversalPayment = q_raw.SendPayment("1963", details_universal_transaction);
            //
            QiwiTransactionsPartnerBankDetailsPaymentClass details_bank_partner_transaction = new QiwiTransactionsPartnerBankDetailsPaymentClass(); // этот набор реквизитов используется для отправки денег в банк-партнёр. Либо по номеру счёта, либо по номеру карты, либо по номеру договора. Допустимые комбинации реквизитов необходимо уточнять 
            details_bank_partner_transaction.comment = "Вывод средств в банк-партнёр";
            details_bank_partner_transaction.sum.amount = 50;
            details_bank_partner_transaction.fields.account = "1111222233334444";
            details_bank_partner_transaction.fields.exp_date = "0419";
            details_bank_partner_transaction.fields.account_type = "1"; // Тип банковского идентификатора. Примеры значений: для Тинькофф Банк - карта 1, договор 3. для Альфа-Банка - карта 1, счет 2. для Промсвязьбанка -карта 7, счет 9. для банка Русский Стандарт -карта 1, счет 2, договор 3. Для подробной актуальной информации по этому вопросу необходимо уточнять в QIWI
            QiwiResultSendToBankParnerPaymentClass SendBankPartnerPayment = q_raw.SendPayment("466", details_bank_partner_transaction); // Это пример перевода на карту банка партнёра Тинькоф по номеру карты и сроку действия карты
            //
            QiwiResultDetectProviderClass DetectProvider = q_raw.DetectMobileOperatorProvider("+79995552211"); // определить номер провайдера оплаты для пополнения счёта мобильного по номеру сотового (в международном формате +79991112233)
            DetectProvider = q_raw.DetectCachCardProvider("4444555577773333"); // Определить по номеру карты номер провайдера
            Console.ReadLine(); 
        }
    }
}