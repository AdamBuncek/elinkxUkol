using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController: ControllerBase
    {
        private List<Customer> Customers = new List<Customer>();

        private void LoadData()
        {
            Customers.Clear();
            using (StreamReader r = new StreamReader("customers.json"))
            {
                string json = r.ReadToEnd();
                Customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }
        }

        private void SaveData()
        {
            using (StreamWriter w = new StreamWriter("customers.json"))
            {
                string json = JsonConvert.SerializeObject(Customers);
                w.Write(json);
            }
        }

        /*
            Metoda Get - získání dat a zobrazení
        */
        [HttpGet]
        public List<Customer> Get()
        {
            LoadData();
            return Customers;
        }

        /*
            Metoda Post - získání dat, přidání nového záznamu a zápis
        */
        [HttpPost]
        public List<Customer> Post(Customer newCustomer)
        {
            LoadData();
            Customers.Add(newCustomer);
            SaveData();

            return Customers;
        }

        /*
            Metoda Put - získání dat, nalezení, editace záznamu a zápis
        */
        [HttpPut]
        public List<Customer> Put(Customer editedCustomer)
        {
            LoadData();
            foreach (Customer c in Customers)
            {
                if(c.CustomerId == editedCustomer.CustomerId)
                {
                    c.FirstName = editedCustomer.FirstName;
                    c.LastName = editedCustomer.LastName;
                    c.Email = editedCustomer.Email;
                }
            }

            SaveData();

            return Customers;
        }

        /*
            Metoda Delete - získání dat, nalezení, smazání záznamu a zápis
        */
        [HttpDelete]
        public JsonResult Delete(Customer deletedCustomer)
        {
            LoadData();
            int index = 0;
            for(int i = 0; i<Customers.Count; i++)
            {
                if(Customers[i].CustomerId == deletedCustomer.CustomerId)
                {
                    index = i;
                }
            }
            Customers.RemoveAt(index);  
 
            SaveData();
            
            return new JsonResult("Deleted successfully");
        }
    }
}
