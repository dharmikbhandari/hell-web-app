﻿using myHell.Data;
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
        public IEnumerable<UserModel> Get()
        {
            DataSet dataSet = new DataSet();
            dataSet = _myHellServices.GetUserByName("dharmik");
            List<UserModel> model = new List<UserModel>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                UserModel user = new UserModel();
                user.Id = Convert.ToInt32(row[0].ToString());
                user.Name = row[1].ToString();
                user.Email = row[2].ToString();
                user.Password = row[3].ToString();
                user.Active =Convert.ToBoolean(row[4].ToString());

                model.Add(user);
            }
            return model;
        }

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
    }
}