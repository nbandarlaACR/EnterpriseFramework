
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Autofac.Builder;
using Autofac;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Enterprise.Framework.GenericLib;

namespace Enterprise.Framework.DataLib
{
    public class JSONReader
    {
        public dynamic jsonobj;
        private string jsonPath;
        public static String dirPath = Assembly.GetExecutingAssembly().Location;
        public String path = Path.GetDirectoryName(dirPath);

        public dynamic LoadJson(string filename)
        {
            /*var configuration = SelectConfig.get().getConfiguration();

            string env = configuration["Environment"];*/

            string env = Environment.GetEnvironmentVariable("Environment");

            if (Convert.ToString(env) == "test")
            {
                jsonPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\Resources\St1\" + filename + ".json"));
            }

            else if (Convert.ToString(env) == "preprod")
            {
                jsonPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\Resources\PreProd\" + filename + ".json"));
            }

            using (StreamReader r = new StreamReader(jsonPath))
            {
                string json = r.ReadToEnd();
                jsonobj = JsonConvert.DeserializeObject<dynamic>(json);
                return jsonobj;
            }
        }

        public string removeSpecialCharactersAndSpacesFromAString(String str)
        {
            Encoding encodingASCII = Encoding.ASCII;
            Byte[] encodedBytes = encodingASCII.GetBytes(str);
            char[] asciiChars = encodingASCII.GetChars(encodedBytes);
            String decodedString = new String(asciiChars);
            decodedString = Regex.Replace(decodedString, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);

            return decodedString;
        }
    }
}
