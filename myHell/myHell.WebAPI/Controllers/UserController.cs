using myHell.Data;
using myHell.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace myHell.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        MyHellServices _myHellServices = new MyHellServices();

        // GET: api/User
        //public IEnumerable<UserModel> Get()
        //{
        //    DataSet dataSet = new DataSet();
        //    dataSet = _myHellServices.GetAllUser(0,0);
        //    List<UserModel> model = new List<UserModel>();
        //    foreach(DataRow row in dataSet.Tables[0].Rows)
        //    {
        //        UserModel user = new UserModel();
        //        user.Id = Convert.ToInt32(row[0].ToString());
        //        user.Name = row[1].ToString();
        //        user.Email = row[2].ToString();
        //        user.Password = row[3].ToString();
        //        user.Active =Convert.ToBoolean(row[4].ToString());

        //        model.Add(user);
        //    }
        //    return model;
        //}

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
           
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }


        [HttpPost]
        [Route("api/user/saveuser/")]
        public JsonResult SaveUser([FromBody]dynamic data)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.InsertUser(0, data.Name.ToString(), data.Email.ToString(), data.Password.ToString(), (bool)data.Active);

            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/user/getuserbyid/{id}")]
        public JsonResult GetUserById(int id)
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetUserById(id);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }

        [Route("api/user/getallusers/")]
        public JsonResult GetAllUsers()
        {
            JsonResult jsonResult = new JsonResult();
            StoredProcedureOutput storedProcedureOutput = new StoredProcedureOutput();
            storedProcedureOutput = _myHellServices.GetAllUser(0,0);
            jsonResult.Object = storedProcedureOutput.DataSet;
            jsonResult.Message = storedProcedureOutput.Message;
            jsonResult.Error = storedProcedureOutput.Error;
            return jsonResult;
        }
    }
    //[HttpPost]
    //[Route("api/contentfile/{files}")] //{files} here tells routing to look for a parameter in the *Route* e.g api/contentfile/something
    //public IHttpActionResult Post([FromBody] List<ContentFile> files)
}
