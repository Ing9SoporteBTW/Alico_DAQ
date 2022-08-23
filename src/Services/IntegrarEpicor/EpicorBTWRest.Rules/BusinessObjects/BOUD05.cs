using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.DataAccess.Models.TimeReport;
using EpicorBTWRest.DataAccess.UD05;
using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Models;


namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOUD05
    {
        public string strConexionERP { get; set; }
        private const string strModule = "UD05";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";

        public EpicorCredentials epicorCredentials { get; set; }
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;

        public BOUD05(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<string> GetByID (string jobnum)
        {

            GetByID getByID_input = new GetByID()
            {
                jobNum = jobnum,
            };

            string method = "Erp.BO.JobEntrySvc/";
            string inst = "GetByID";
            string type = "returnObj";
            string entity = JsonConvert.SerializeObject(getByID_input);

            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("GetByID");

            string ds = (response as JObject).SelectToken("JobHead[0].Plant", false).ToObject<string>();

            if (ds == null) return "Error al obtener la planta";

            return ds;
        }
        public async Task<bool> CreateLostTimeInEpicor(Epicor_UD05 _ud)
        {

            if (_ud == null)
                throw new Exception("No tengo información para trabajar");

            string Planta = await GetByID (_ud.jobnum);
            string LaborDtlSeq = string.Empty;
            string LaborSeq = string.Empty;
            string Codigo = string.IsNullOrEmpty(_ud.Codi_Paro) ? _ud.Codi_Ejecu : _ud.Codi_Paro;

            if (Codigo.Length == 1)
                Codigo = "00" + Codigo;
            if (Codigo.Length == 2)
                Codigo = "0" + Codigo;

            int Indirect = 0, Prod = 0, Setup = 0;
            if (_ud.TipoLabor == "I")
                Indirect = 1;
            else if (_ud.TipoLabor == "P")
                Prod = 1;
            else
                Setup = 1;
            
            string cadenaAct = "Registro DAQ";
            string Sentencia = string.Concat(new string[] { "Insert Into Ice.UD05 ",
                    "(Company,",
                    "Key1,",
                    "Key2,",
                    "Key3,",
                    "ShortChar01,",
                    "ShortChar02,",
                    "ShortChar03,",
                    "ShortChar04,",
                    "ShortChar07,",
                    "ShortChar08,",
                    "ShortChar05,",
                    "ShortChar06,",
                    "Character02,",
                    "CheckBox01,",
                    "CheckBox02,",
                    "CheckBox03,",
                    "Number03,",
                    "Character03,",
                    "Character10,",
                    "ShortChar09,",
                    "Date04,",
                    "ShortChar20,",
                    "ShortChar18,",
                    "ShortChar19)",
                    " Values (",
                    "'Alico',",
                    "(select COUNT(*) + 1 [ID] From Ice.UD05 Where Company = 'Alico'),",
                    "'" + LaborSeq + "',",
                    "'" + LaborDtlSeq + "',",
                    "'" + _ud.jobnum + "',",
                    "'0',",
                    "'" + _ud.Oper + "',",
                    "'" + _ud.ResourceID + "',",
                    "'" + _ud.Entrada + "',",
                    "'" + _ud.Final + "',",
                    "'" + Codigo + "',",
                    "'" + Planta + "',",
                    "(Select Description From erp.Reason Where Company = 'Alico' and ReasonCode = '" + Codigo + "' and ReasonType = 'S'),",
                    Indirect + ",",
                    Prod + ",",
                    Setup + ",",
                    "(SELECT Max(Number03) + 1 FROM Ice.UD05 where company = 'Alico'), ",
                    "'Notas DAQ',",
                    "'" + cadenaAct + "',",
                    "'" + _ud.Entrada.ToString("yyyy/MM/dd") + "',",
                    "'" + _ud.Final.ToString("yyyy-MM-dd") + "',",
                    "'" + _ud.EmployeeNum + "',",
                    "'" + _ud.Codi_Ejecu + "',",
                    "'" + _ud.Codi_Paro + "')"});

            strConexionERP = "Data Source= ALICODB02;" +
                "Initial Catalog=Epicor10Tran;User ID=EpicorReporter;Password=EpicorReporter";

            clsConexion objCnx = new clsConexion
            {
               strConnection = strConexionERP,
               strSQL = Sentencia,
            };
            if (!objCnx.EjecutarSentencia(false))
            {
                throw new Exception("Error Tiempo Perdido: " + objCnx.Error); 
                objCnx = null;
                return false;
            }
            objCnx = null;
            return true;
        }
        public async Task<bool> Actualizar(IntermediaContext context, Epicor_UD05 _ud)
        {

            _ud.Sincronizado = true;

            context.Epicor_UD05s.Update(_ud);
            return true;
        }
    }
        

}
