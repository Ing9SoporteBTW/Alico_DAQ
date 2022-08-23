namespace EpicorBTWRest.DataAccess.Models.Labor
{

    public class GetNewLaborDtlNoHdr
    {
       public Erp.Tablesets.LaborTableset ds { get; set; }
       public string EmployeeNum { get; set; }
       public bool ShopFloor { get; set; }
       public DateTime ClockInDate { get; set; }
       public decimal ClockInTime { get; set; }
       public DateTime ClockOutDate { get; set; }
       public decimal ClockOutTime { get; set; }
        
    }
    //public  class  DefaultLaborType
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }
    //    public string ipLaborType { get; set; }
    //}
    //public class ChangeLaborType
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }
    //}

    //public class DefaultJobNum
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }
    //    public string jobNum { get; set; }  

    //}
    //public class LaborRateCalc
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }

    //}
    //public class DefaultAssemblySeq
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }
    //    public int assesblySeq { get; set; }

    //}
    //public class DefaultOprSeq
    //{
    //    public Erp.Tablesets.LaborTableset ds { get; set; }
    //    public int oprSet { get; set; }
    //    public string vMessage { get; set; }
    //}
    //public class GetDspClockTime
    //{
    //    public string dspClckTm { get; set; }
    //}
}
