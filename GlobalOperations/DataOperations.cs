using EasyITSystemCenter.GlobalClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace EasyITSystemCenter.GlobalOperations {

    /// <summary>
    /// Centralized DataOperations as Cleaning dataset Language Dictionary Auto filing
    /// </summary>
    public static class DataOperations {

        /// <summary>
        /// Convert Dictionary string,string To Json Suportted Values Bool, Int, String
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public static string ConvertDictionaryListToJson(Dictionary<string, string> keyList) {
            bool tempBool; int tempInt = 0;
            Dictionary<string, object> exportJsonList = new Dictionary<string, object>();
            keyList.ToList().ForEach(key => {
                string valueType = bool.TryParse(key.Value, out tempBool) ? "bool" : int.TryParse(key.Value, out tempInt) ? "int" : "string";
                exportJsonList.Add(key.Key, (valueType == "bool" ? (object)tempBool : valueType == "int" ? (object)tempInt : (object)key.Value));
            });
            return new JavaScriptSerializer().Serialize(exportJsonList);
        }

        /// <summary>
        /// !!! SYSTEM RULE: ClassList with joining fields names must be nullable before API
        /// operation !!! ClassName must contain: "Extended" WORD Extension field in Class - Dataset
        /// must be set as null before Database Operation else is joining to other dataset is valid
        /// and can be blocked by fail key Its Check Extended in ClassName - SYSTEM RULE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void NullSetInExtensionFields<T>(ref T dataset) {
            if (dataset.GetType().Name.ToLower().Contains("extended")) {
                foreach (PropertyInfo prop in dataset.GetType().GetProperties()) { if (prop.DeclaringType.Name.Contains("Extended")) prop.SetValue(dataset, null); }
            }
        }

        /// <summary>
        /// Object To Json Serializer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(this object obj) {
            return JsonSerializer.Serialize(obj, App.appRuntimeData.JsonSerializeOptions);
        }

        /// <summary>
        /// Return Requested User or if not exist default DB parameter CamelCase Ignored
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static async Task<string> ParameterCheck(string parameterName) {
            string result = null;
            try {
                if (App.UserData.Authentification != null && App.ParameterList.Any()) {
                    result = App.ParameterList.Where(a => a.SystemName.ToLower() == parameterName.ToLower() && a.UserId == App.UserData.Authentification.Id).Select(a => a.Value).FirstOrDefault();
                }
            } catch (Exception Ex) { App.ApplicationLogging(Ex); }

            try {
                if (result == null && App.ParameterList.Any()) {
                    result = App.ParameterList.Where(a => a.SystemName.ToLower() == parameterName.ToLower() && a.UserId == null).Select(a => a.Value).FirstOrDefault();
                    if (result == null) App.ApplicationLogging(new Exception(), $"{await DBOperations.DBTranslation("Missing Server Parameter")}: {parameterName}");
                }
                return result;
            } catch { return result; }
        }


        /// <summary>
        /// Convert String To ByteArray
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string input) {
            return Encoding.UTF8.GetBytes(input);
        }



        /// <summary>
        /// Convert UTF8 to UNICODE
        /// </summary>
        /// <param name="utf8String"></param>
        /// <returns></returns>
        public static string Utf8toUnicode(this string utf8String) {
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i) { utf8Bytes[i] = (byte)utf8String[i]; }
            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        /// <summary>
        /// Remove Diactritics
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string RemoveDiacritism(string Text) {
            string stringFormD = Text.Normalize(NormalizationForm.FormD);
            StringBuilder retVal = new StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++) {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Convert Generic Tabla To Standard Table For Use Standard System Fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<GenericTable> ConvertGenericClassListToStandard<T>(T data) {
            return JsonSerializer.Deserialize<List<GenericTable>>(JsonSerializer.Serialize(data), App.appRuntimeData.JsonSerializeOptions);
        }

        /// <summary>
        /// Convert Generic Tabla To Standard Table For Use Standard System Fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GenericTable ConvertGenericClassToStandard<T>(T data) {
            return JsonSerializer.Deserialize<GenericTable>(JsonSerializer.Serialize(data), App.appRuntimeData.JsonSerializeOptions);
        }

        /// <summary>
        /// Convert String to Generic Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GenericToEnum<T>(this string value) {
            try {
                Type enumType = typeof(T);
                if (!enumType.IsEnum) {
                    App.ApplicationLogging(new Exception("DataOperation GenericToEnum Method DataOperations  T GenericToEnum<T>: T must be an Enumeration type." + enumType.ToString()));
                }
            } catch (Exception Ex) { App.ApplicationLogging(Ex); }
            return (T)Enum.Parse(typeof(T), value, true);
        }


        /// <summary>
        /// Generic Convert Data Table To DataList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static List<T> GenericConvertTableToClassList<T>(DataTable dt) {
            List<T> result = new List<T>();
            try {
                foreach (DataRow dr in dt.Rows) {
                    var typeObject = Activator.CreateInstance<T>();
                    foreach (var fieldInfo in typeof(T).GetProperties()) {
                        foreach (DataColumn dc in dt.Columns) {
                            if (fieldInfo.Name == dc.ColumnName) { fieldInfo.SetValue(typeObject, dr[dc.ColumnName]); break; }
                        }
                    }; result.Add(typeObject);
                };
            } catch { }
            return result;
        }


        /// <summary>
        ///  Convert Data Table To DataList of Type
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static List<object> ConvertTableToClassListByType(DataTable dt, Type classType) {
            List<object> result = new List<object>();
            try {
                foreach (DataRow dr in dt.Rows) {
                    var typeObject = Activator.CreateInstance(classType);
                    foreach (var fieldInfo in classType.GetProperties()) {
                        foreach (DataColumn dc in dt.Columns) {
                            if (fieldInfo.Name == dc.ColumnName) { fieldInfo.SetValue(typeObject, dr[dc.ColumnName]); break; }
                        }
                    }; result.Add(typeObject);
                };
            } catch { }
            return result;
        }


        /// <summary>
        /// Converts the data table to xml.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="tablename">The tablename.</param>
        /// <returns>A XElement.</returns>
        public static XElement ConvertDataTableToXml(DataTable dataTable, string tablename) {
            var xmlTable = new XElement(tablename, dataTable.Rows.Cast<DataRow>()
            .GroupBy(row => (string)row[0]).Select(g =>
           new XElement(dataTable.Columns[0].ColumnName,
               new XElement("label", g.Key),
               g.GroupBy(row => (string)row[1]).Select(g1 =>
                   new XElement(dataTable.Columns[1].ColumnName, new XElement("label", g1.Key),
                       new XElement(dataTable.Columns[2].ColumnName, g1.Select(row => new XElement("label", (string)row[2])))
                   )
               )
            )));
            return xmlTable;
        }



        /// <summary>
        /// Ignores the case replace string.
        /// </summary>
        /// <param name="originalValue">The original value.</param>
        /// <param name="replaceFrom">The replace from.</param>
        /// <param name="ReplaceTo">The replace to.</param>
        /// <returns>A string.</returns>
        public static string IgnoreCaseReplaceString(string originalValue,string replaceFrom,string replaceTo = "") {
            return Regex.Replace(originalValue, replaceFrom, replaceTo, RegexOptions.IgnoreCase);
        }
    }
}