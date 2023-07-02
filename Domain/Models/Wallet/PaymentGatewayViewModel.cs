using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Models.Wallet
{
    public class PaymentRequestViewModel
    {
        [Display(Name = "MerchantId")]
        public string MerchantId { get; set; }

        [Display(Name = "TerminalId")]
        public string TerminalId { get; set; }

        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Display(Name = "OrderId")]
        public long OrderId { get; set; }

        [Display(Name = "DateAndTimeOfSentTransaction")]
        public DateTime LocalDateTime { get; set; }

        [Display(Name = "ReturnUrl")]
        public string ReturnUrl { get; set; }

        [Display(Name = "TransactionInformationInFormOfEncrypted")]
        public string SignData { get; set; }

        [Display(Name = "AdditionalDataOfTransaction")]
        public string? AdditionalData { get; set; }

        [Display(Name = "ApplicantApplicationName")]
        public string? ApplicationName { get; set; }
    }

    public class PaymentRequestResultViewModel
    {
        [Display(Name = "TransactionResult")]
        public int ResCode { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "TransactionResultDescription")]
        public string Description { get; set; }
    }

    public class VerifyPaymentViewModel
    {
        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "TokenInFormOfEncrypted")]
        public string SignData { get; set; }
    }

    public class VerifyPaymentResultViewModel
    {
        [Display(Name = "TransactionResult")]
        public int ResCode { get; set; }

        [Display(Name = "TransactionAmount")]
        public int Amount { get; set; }

        [Display(Name = "TransactionAmountDescription")]
        public string Description { get; set; }

        [Display(Name = "RetrivalReferenceNumber")]
        public string RetrivalRefNo { get; set; }

        [Display(Name = "SystemTraceNumber")]
        public string SystemTraceNo { get; set; }

        [Display(Name = "OrderNumber")]
        public string OrderId { get; set; }
    }

    public class CallbackRequestPaymentViewModel
    {
        [Display(Name = "PrimaryAccountNumber")]
        public string PrimaryAccNo { get; set; }

        [Display(Name = "CardHashNumber")]
        public string HashedCardNo { get; set; }

        [Display(Name = "orderNumber")]
        public long OrderId { get; set; }

        [Display(Name = "TransactionResponse")]
        public string SwitchResCode { get; set; }

        [Display(Name = "TransactionResult")]
        public string ResCode { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }
    }
}
