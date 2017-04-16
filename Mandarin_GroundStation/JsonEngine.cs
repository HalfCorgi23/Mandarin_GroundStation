using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mandarin_GroundStation
{
    public class JSON_Base
    {
        public string JSON_File_Path;
        private string JSON_Content;
        public  JObject JSON_Object;
        public JSON_Base(string json_path)
        {
            JSON_File_Path = json_path;
            Load_Json_File();
        }
        public void Load_Json_File()
        {
            StreamReader sr = File.OpenText(JSON_File_Path);
            StringBuilder jsonArrayText_Tmp = new StringBuilder();
            string input = null;
            while ((input = sr.ReadLine()) != null)
            {
                jsonArrayText_Tmp.Append(input);
            }
            sr.Close();
            JSON_Content = jsonArrayText_Tmp.Replace(" ", "").ToString();
            JSON_Object = JObject.Parse(JSON_Content);
        }
    }

    public class JSON_Unserial : JSON_Base
    {
        public JSON_Unserial(string json_path) : base(json_path){ }
        public string Read(string item)
        {
            return JSON_Object[item].ToString();
        }
        public string Read(string index, string item)
        {
            return JSON_Object[index][item].ToString();
        }
        public string Read(string index, string index_1, string item)
        {
            return JSON_Object[index][index_1][item].ToString();
        }
        public string Read(string index, string index_1, string index_2, string item)
        {
            return JSON_Object[index][index_1][index_2][item].ToString();
        }
    }
}
