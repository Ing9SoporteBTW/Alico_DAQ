namespace SharedService.Models.Epicor.BAQ
{
    public class JLP_ActasdeBajaBAQ
    {
        public DateTime NonConf_SysDate { get; set; }
        public int NonConf_FailedQty { get; set; }
        public string JCDept_Description { get; set; }
        public string NonConf_EmpID { get; set; }
        public int NonConf_TranID { get; set; }
        public bool NonConf_InspectionPending { get; set; }
        public int LaborDtl_OprSeq { get; set; }
        public string NonConf_JobNum { get; set; }
        public string NonConf_PartNum { get; set; }
        public string NonConf_Description { get; set; }
        public string NonConf_ReasonCode { get; set; }
        public string NonConf_TrnTyp { get; set; }
        public string NonConf_CommentText { get; set; }
        public int NonConf_PassedQty { get; set; }
        public int NonConf_DMRNum { get; set; }
        public bool DMRHead_OpenDMR { get; set; }
        public int DMRHead_TotRejectedQty { get; set; }
        public int DMRHead_TotAcceptedQty { get; set; }
        public string NonConf_PsdCommentText { get; set; }
        public string RowIdent { get; set; }
    }
}
