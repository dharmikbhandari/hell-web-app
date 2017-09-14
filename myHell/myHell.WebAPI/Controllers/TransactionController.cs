using myHell.Data;
using System.Web.Http;

namespace myHell.WebAPI.Controllers
{
    public class TransactionController : ApiController
    {
        MyHellServices _myHellServices = new MyHellServices();


        [HttpPost]
        [Route("api/transaction/savetransaction/")]
        public JsonResult SaveTransaction([FromBody]dynamic data)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.InsertTransaction(data.Id == null ? 0 : (int)data.Id,(decimal)data.Amount,(int)data.CategoryId,(int)data.UserId, (bool)data.Active);

            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/transaction/gettransactionbyid/{id}")]
        public JsonResult GetTransactionById(int id)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetTransactionById(id);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/transaction/getalltransactions/")]
        public JsonResult GetAllTransactions()
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetAllTransactions(0, 0);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }
    }

}

