using myHell.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace myHell.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        MyHellServices _myHellServices = new MyHellServices();

        [HttpPost]
        [Route("api/category/savecategory/")]
        public JsonResult SaveCategory([FromBody]dynamic data)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.InsertCategory(data.Id == null ? 0 : (int)data.Id, data.Category_Name.ToString(), data.Category_Type.ToString(), (bool)data.Active);

            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/category/getcategorybyid/{id}")]
        public JsonResult GetCategoryById(int id)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetCategoryById(id);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/category/getallcategories/")]
        public JsonResult GetAllCategories()
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetAllCategories(0, 0);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }
    }
}

