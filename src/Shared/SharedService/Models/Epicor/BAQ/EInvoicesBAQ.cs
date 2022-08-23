namespace SharedService.Models.Epicor.BAQ
{
    public class EInvoicesBAQ
    {
        public string InvcHead_Company { get; set; }
        public int InvcHead_InvoiceNum { get; set; }
        public string InvcHead_LegalNumber { get; set; }
        public string Calculated_InvoiceType { get; set; }
        public DateTime InvcHead_InvoiceDate { get; set; }
        public string Customer_CustID { get; set; }
        public string Customer_Name { get; set; }
        public decimal InvcHead_InvoiceAmt { get; set; }
        public bool InvcHead_EstadoTimbre_c { get; set; }
        public string InvcHead_DescTimbrado_c { get; set; }
        public bool InvcHead_AceptaCliente_c { get; set; }
        public string InvcHead_ObservacionCliente_c { get; set; }
        public string RowIdent { get; set; }
    }
}
