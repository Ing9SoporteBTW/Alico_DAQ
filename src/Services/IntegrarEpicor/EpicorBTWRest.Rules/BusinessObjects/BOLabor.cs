
using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.DataAccess.Labor;
using EpicorBTWRest.DataAccess.Models.Labor;
using EpicorBTWRest.DataAccess.Models.TimeReport;
using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Models;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOLabor
    {
        private const string strModule = "Labor";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";
        
        public EpicorCredentials epicorCredentials { get; set; }
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;
        
        public BOLabor(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        #region Métodos de Epicor 
        private async Task<Erp.Tablesets.LaborTableset> GetNewLaborDtlNoHdr(Epicor_Labor _labor)
        {

            GetNewLaborDtlNoHdr getNewLaborDtlNoHdr_input = new GetNewLaborDtlNoHdr()
            {
                ds = new Erp.Tablesets.LaborTableset(),
                EmployeeNum = _labor.EmployeeNum,
                ShopFloor = false,
                ClockInDate = _labor.ClockInDate,
                ClockInTime = _labor.ClockinTime,
                ClockOutDate = _labor.ClockInDate,
                ClockOutTime = _labor.ClockOutTime,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "GetNewLaborDtlNoHdr";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(getNewLaborDtlNoHdr_input);
            
            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("No se puede crear el DS");

            var ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"GetNewLaborDtlNoHdr");

            return ds;

        }
        private async Task<Erp.Tablesets.LaborTableset> DefaultLaborType(Erp.Tablesets.LaborTableset ds,
            string ipLaborType)
        {

            DefaultLaborType defaultLaborType_input = new DefaultLaborType()
            {
                ds = ds,
                ipLaborType = ipLaborType,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultLaborType";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultLaborType_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultLaborType");

            return ds;

        }
        public async Task<Erp.Tablesets.LaborTableset> ChangeLaborType(Erp.Tablesets.LaborTableset ds)
        {
            
           ChangeLaborType changeLaborType_input = new ChangeLaborType()
           {
              ds = ds,
           };

            string method = "Erp.BO.LaborSvc/";
            string inst = "ChangeLaborType";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(changeLaborType_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("ChangeLaborType");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"ChyangeLaborType");

            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> DefaultJobNum(Erp.Tablesets.LaborTableset ds,
            string jobNum)
        {
            ds.LaborDtl[0].LaborNote = jobNum;
            DefaultJobNum defaultJobNum_input = new DefaultJobNum()
            {
                ds = ds,
                jobNum = jobNum,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultJobNum";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultJobNum_input);
            log.LogInformation($"entity{entity}");

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);
            log.LogInformation($"response JobNum{response}");

            if (response == null) throw new Exception("DefaultJobNum");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultJobNum");
            log.LogInformation($"ds JobNum{ds}");
            

            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> LaborRateCalc(Erp.Tablesets.LaborTableset ds)
        {
            LaborRateCalc laborRateCalc_input = new LaborRateCalc()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "LaborRateCalc";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(laborRateCalc_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("LaborRateCalc");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"LaborRateCalc");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> DefaultAssemblySeq(Erp.Tablesets.LaborTableset ds,
            int assemblySeq)
        {
            DefaultAssemblySeq defaultAssemblySeq_input = new DefaultAssemblySeq()
            {
                ds = ds,
                assemblySeq = assemblySeq,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultAssemblySeq";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultAssemblySeq_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);
            
            if (response == null) throw new Exception("DefaultAssemblySeq");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultAssemblyseq");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> DefaultOprSeq(Erp.Tablesets.LaborTableset ds,
            int oprSeq)
        {

            DefaultOprSeq defaultOprSeq_input = new DefaultOprSeq()
            {
                ds = ds,
                oprSeq = oprSeq,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultOprSeq";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultOprSeq_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);
            if (response == null) throw new Exception("DefaultOprSeq");
            
            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultOprSeq");
            return ds;
        }
        public async Task<bool> GetDspClockTime(decimal clckTm)
        {

            GetDspClockTime getDspClockTime_input = new GetDspClockTime()
            {

                clckTm = clckTm,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "GetDspClockTime";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(getDspClockTime_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("GetDspClockTime");
            log.LogInformation($"GetDspClockTime");
            return true;
           
        }
        public async Task<Erp.Tablesets.LaborTableset> DefaultDtlTime(Erp.Tablesets.LaborTableset ds)
        {
            ds.LaborDtl[0].LaborNote = ds.LaborDtl[0].JobNum;
            ds.LaborDtl[0].RowMod = "U";
            DefaultDtlTime defaultDtlTime_input = new DefaultDtlTime()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultDtlTime";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultDtlTime_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("DefaultDtlTime");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultDtlTime");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> DefaultComplete(Erp.Tablesets.LaborTableset ds, bool cmplete)
        {
            DefaultComplete defaultComplete_input = new DefaultComplete()
            {
                ds = ds,
                cmplete = cmplete,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "DefaultComplete";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(defaultComplete_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("DefaultComplete");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"DefaultComplete");
            return ds;
        }

        public async Task<Erp.Tablesets.LaborTableset> CheckWarnings(Erp.Tablesets.LaborTableset ds)
        {

            CheckWarnings checkWarnings_input = new CheckWarnings()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "CheckWarnings";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(checkWarnings_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("CheckWarnings");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"CheckWarnings");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> Update(Erp.Tablesets.LaborTableset ds)
        {

            Update update_input = new Update()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "Update";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(update_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("Update");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"Update");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> ValidateChargeRateForTimeType(Erp.Tablesets.LaborTableset ds)
        {

            ValidateChargeRateForTimeType validateChargeRateForTimeType_input = new ValidateChargeRateForTimeType()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "ValidateChargeRateForTimeType";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(validateChargeRateForTimeType_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("ValidateChargeRateForTimeType");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"ValidateChargeRateForTimeType");
            return ds;
        }

        public async Task<Erp.Tablesets.LaborTableset> SubmitForApproval(Erp.Tablesets.LaborTableset ds,
            bool lWeeklyView)
        {

            SubmitForApproval submitForApproval_input = new SubmitForApproval()
            {
                ds = ds,
                lWeeklyView = lWeeklyView,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "SubmitForApproval";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(submitForApproval_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("SubmitForApproval");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"SubmitForApproval");
            return ds;
              }
        public async Task<Erp.Tablesets.LaborTableset> ChangeIndirectCode(Erp.Tablesets.LaborTableset ds)
        {

            ChangeIndirectCode changeIndirectCode_input = new ChangeIndirectCode()
            {
                ds = ds,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "ChangeIndirectCode";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(changeIndirectCode_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("ChangeIndirectCode");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"ChangeIndirectCode");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> Overrides (Erp.Tablesets.LaborTableset ds,
            string inOpCode, string inResGrpID)
        {

            Overrides overrides_input = new Overrides()
            {
                ds = ds,
                inOpCode = inOpCode,
                inResGrpID= inResGrpID,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "Overrides";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(overrides_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("Overrides");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"Overrides");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> OverridesResource (Erp.Tablesets.LaborTableset ds,
           string ProposedResourceID)
        {

            OverridesResource overridesResource_input = new OverridesResource()
            {
                ds = ds,
                ProposedResourceID= ProposedResourceID,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "OverridesResource";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(overridesResource_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("OverridesResource");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"OverridesResource");
            return ds;
        }
        public async Task<Erp.Tablesets.LaborTableset> CheckResourceGroup(Erp.Tablesets.LaborTableset ds,
           string ProposedResourceID)
        {

            CheckResourceGroup overridesResource_input = new CheckResourceGroup()
            {
                ds = ds,
                ProposedResourceID = ProposedResourceID,
            };

            string method = "Erp.BO.LaborSvc/";
            string inst = "CheckResourceGroup";
            string type = "parameters";
            string entity = JsonConvert.SerializeObject(overridesResource_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("CheckResourceGroup");

            ds = (response as JObject).SelectToken("ds", false).ToObject<Erp.Tablesets.LaborTableset>();
            log.LogInformation($"ChackResourceGroup");
            return ds;
        }
        #endregion

        #region Métodos para el reporte de tiempo
        public async Task<bool> CreateTimeDirectInEpicor(Epicor_Labor _labor)
        {
            
            if (_labor == null)
                throw new Exception("No se tiene información");

            // Ingreso de Tiempo
            
            Erp.Tablesets.LaborTableset ds = await GetNewLaborDtlNoHdr(_labor);
            ds = await DefaultLaborType(ds,_labor.LaborType);
            ds = await ChangeLaborType(ds);

            if (_labor.LaborType == "I")
            {
                return await Indirectos(ds,_labor);
            }

            ds = await DefaultJobNum(ds,_labor.jobnum);
            ds = await LaborRateCalc(ds);
            ds = await DefaultAssemblySeq(ds,_labor.Ensamble);
            ds = await DefaultOprSeq(ds,_labor.OprSeq);
            await GetDspClockTime(_labor.ClockOutTime);
            ds = await CambiarRecurso(ds,_labor);
            ds = await CheckWarnings(ds);
            ds = await Update(ds);

            return await AprobarTiempo(ds, _labor);
        }
        public async Task<bool> Indirectos(Erp.Tablesets.LaborTableset ds,Epicor_Labor _labor)
        {
            ds = await ChangeIndirectCode(ds);

            ds = await CheckWarnings(ds);
            ds.LaborDtl[0].IndirectCode = _labor.CodigoParo;
            ds.LaborDtl[0].LaborNote = _labor.LaborNote;
            BOIndirect _BO;
            _BO = new BOIndirect(log);

            ds.LaborDtl[0].ExpenseCode = await _BO.ObtenerInfoIndirecto(_labor.CodigoParo, epicorCredentials);
            ds.LaborDtl[0].ResourceGrpID = _labor.GrupoRecurso;
            ds = await Update(ds);

            return await AprobarTiempo(ds, _labor);
        }
        public async Task<Erp.Tablesets.LaborTableset> CambiarRecurso(Erp.Tablesets.LaborTableset ds, Epicor_Labor _labor)
        {
            ds = await Overrides(ds,"",_labor.GrupoRecurso);
            ds = await CheckResourceGroup(ds,_labor.ResourceID);
            ds = await OverridesResource(ds,_labor.ResourceID);
            return ds;
        }
        public async Task<bool> AprobarTiempo (Erp.Tablesets.LaborTableset ds, Epicor_Labor _labor)
        {
            // Aprobar el tiempo ingresado

            ds = await DefaultDtlTime(ds);
            ds = await DefaultComplete(ds, true);
            ds = await ValidateChargeRateForTimeType(ds);
            ds = await SubmitForApproval(ds, false);
            ds = await Update(ds);

            return true;
        } 
        public async Task<bool> Actualizar (IntermediaContext context,Epicor_Labor _labor)
        {

            _labor.Sincronizado=true;

            context.Epicor_Labors.Update(_labor);
            return true;
        }
        #endregion

    }
}
