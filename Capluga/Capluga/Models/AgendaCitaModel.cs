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
    public class AgendaCitaModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];


     
        public string RegistrarCita(AgendaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCita";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<AgendaEnt> ConsultaCitas()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCitas";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<AgendaEnt>>().Result;
            }
        }

        public AgendaEnt ConsultaCita(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCita?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<AgendaEnt>().Result;
            }
        }
        public string Actualizarcita(AgendaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Actualizarcita";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

    }
}