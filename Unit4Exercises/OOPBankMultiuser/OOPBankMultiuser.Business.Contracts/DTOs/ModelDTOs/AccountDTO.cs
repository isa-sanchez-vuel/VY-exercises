namespace OOPBankMultiuser.Application.Contracts.DTOs.ModelDTOs
{
    public class AccountDTO
    {
        public string OwnerName { get; set; }
        public string Iban { get; set; }
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
        public decimal TotalBalance { get; set; }
    }
}
