using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Models
{
    public class RolesModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];


    }
}