﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using gicmart.Models;

namespace gicmart.Controllers
{
    public class registrationController : Controller
    {
       public string role;
       public bool status;
        //
        // GET: /registration/
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register( users usr)
        {
            if (ModelState.IsValid)
            {
                string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                //getting pin_no
                string pinsp = "pin_sp";
                SqlCommand cmd1 = new SqlCommand(pinsp, con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@status", "active");
                int pn = cmd1.ExecuteNonQuery();
                //getting user_id
                string usersp = "user_sp";
                SqlCommand cmd2 = new SqlCommand(usersp, con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@password", usr.password);
                cmd2.Parameters.AddWithValue("@name", usr.name);
                cmd2.Parameters.AddWithValue("@role", "user");
                int ui = cmd2.ExecuteNonQuery();
                //getting sponsor_id
                string sponsorsp = "sponsor_sp";
                SqlCommand cmd3 = new SqlCommand(sponsorsp, con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@sponsorname", usr.sponsorname);
                int spi = cmd3.ExecuteNonQuery();
                //getting user details
                string registersp = "registration_sp";
                SqlCommand cmd = new SqlCommand(registersp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", usr.userid);
                cmd.Parameters.AddWithValue("@address", usr.address);
                cmd.Parameters.AddWithValue("@password", usr.password);
                cmd.Parameters.AddWithValue("@sponsorid", usr.sponsorid);
                cmd.Parameters.AddWithValue("@sponsorname", usr.sponsorname);
                cmd.Parameters.AddWithValue("@pin",usr.pin);
                cmd.Parameters.AddWithValue("@name", usr.name);
                cmd.Parameters.AddWithValue("@nomineename", usr.nomineename);
                cmd.Parameters.AddWithValue("@state", usr.state);
                cmd.Parameters.AddWithValue("@city", usr.city);
                cmd.Parameters.AddWithValue("@pancardno", usr.pancardno);
                cmd.Parameters.AddWithValue("@mobileno", usr.mobileno);
                cmd.Parameters.AddWithValue("@terandcontion", usr.termandcondition);
                int wq = cmd.ExecuteNonQuery();
                ViewBag.Message = string.Format("data inserted successfull");
                con.Close();
            }
            return View(usr);
        }
    }
}
