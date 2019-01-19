﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonIntegration
{
    class JsonHelpers
    {
        private string PathToFile;
        public JsonHelpers(string pathToFile)
        {
            PathToFile = pathToFile;
        }

        string readResult = string.Empty;
        string writeResult = string.Empty;

        public Account GetAccount(string parameter, string parameterName)
        {
            using (StreamReader r = new StreamReader(PathToFile))
            {
                var jsonString = r.ReadToEnd();
                JArray jarr = JArray.Parse(jsonString);
                var token = $"$.[?(@" + parameter + "== '" + parameterName + "')]";
                JToken accountData = jarr.SelectToken(token);
                return accountData.ToObject<Account>();
            }
        }

        public void UpdateAccount(Account account)
        {
            //TODO: Add exception if account isn't exist
            var jsonString = File.ReadAllText(PathToFile);
            string jsonData = JsonConvert.SerializeObject(account, Formatting.Indented);
            File.WriteAllText(PathToFile, jsonData);
        }

        public void AddNewAccount(string accountName)
        {

        }

        public void PrintAllData(Account account)
        {
            Console.WriteLine(account.ToString());
        }
        public void DeleteAccount(Account account)
        {

        }

        public void EditAndUpdateNode(string old = "Kate", string newValue = "T")
        {
            using (StreamReader r = new StreamReader(PathToFile))
            {
                var jsonString = r.ReadToEnd();
                var jobj = JObject.Parse(jsonString);
                readResult = jobj.ToString();
                foreach (var item in jobj.Properties())
                {
                    item.Value = item.Value.ToString().Replace("Kate", "Katyusha");
                }
                writeResult = jobj.ToString();
                Console.WriteLine(jobj);
            }
            Console.WriteLine(readResult);
            File.WriteAllText(PathToFile, writeResult);
        }
    }
}
