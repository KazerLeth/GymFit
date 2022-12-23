using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVistas.Models;

namespace WebVistas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Simular pedir los empleados a la base de datos y entrega una lista
            List<Empleado> losEmpleados = obtenerEmpleados();

            return View(losEmpleados);
        }

       

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            Empleado e = buscarId(id);
            if(e != null)
                return View(e);
            else
                return Content("<h1> No existe </h1>");
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            //Buscar el id y crear un objeto Empleado
            Empleado e = buscarId(id);

            return View(e);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //Buscar en la base el id que se modificará
                //update
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            //Buscar el numero de empleado (id) en la base y entregue el objeto empleado
            Empleado emp = buscarId(id);
            return View(emp);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                eliminarEmpleado(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private List<Empleado> obtenerEmpleados() {
            List<Empleado> losEmpleados = new List<Empleado>();
            String cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnStr)) {
                cnn.Open();

                SqlCommand cmdSql = new SqlCommand("Select * from Empleados order by Nombre", cnn);

                SqlDataReader dr = cmdSql.ExecuteReader();

                while (dr.Read()) {
                    //Debug.WriteLine("----------------------------------------");
                    //Debug.Write("Empleado " + dr.GetInt32(0));
                    //Debug.WriteLine(" - Nombre " + dr["Nombre"].ToString());
                    losEmpleados.Add(new Empleado(dr.GetInt32(0), dr["Nombre"].ToString()));
                }

                cnn.Close();
                cnn.Dispose();
            }

            return losEmpleados;
        }

        private Empleado buscarId(int id) {
            Empleado empleado = null;
            String cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnStr)) {
                cnn.Open();

                SqlCommand cmdSql = new SqlCommand("Select * from Empleados where numEmpleado = @id", cnn);
                cmdSql.Parameters.AddWithValue("@id", id);

                SqlDataReader dr = cmdSql.ExecuteReader();

                if (dr.Read()) {
                    empleado = new Empleado(dr.GetInt32(0), dr.GetString(1));
                }


                cnn.Close();
                cnn.Dispose();
            }

            return empleado;
        }

        private int eliminarEmpleado(int id) {
            String cnnStr = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnStr)) {
                cnn.Open();

                SqlCommand cmdSql = new SqlCommand("delete from Empleados where numEmpleado = @id", cnn);
                cmdSql.Parameters.AddWithValue("@id", id);

                return cmdSql.ExecuteNonQuery();
            }
        }



    }
}
