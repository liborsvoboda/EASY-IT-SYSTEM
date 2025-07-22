using System;

namespace EasyITSystemCenter.GlobalOperations {

    internal class ProgrammaticOperations {

        /// <summary>
        /// Global DataTypes Chwecker with Bool Result Used on System Parameters
        /// </summary>
        public static (object, bool) ConvertStringTypeValueToSpecTypeByKnownTypeName(string typeString, string value) {
            bool result = false;
            try {
                switch (typeString) {
                    case "string":
                        result = !string.IsNullOrWhiteSpace(value);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "bit":
                        result = bool.TryParse(value, out bool tempBit);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "int":
                        result = int.TryParse(value, out int tempIntValue);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "numeric":
                        result = double.TryParse(value, out double tempNumValue);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "date":
                        result = DateTime.TryParse(value, out DateTime tempDateValue);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "time":
                        result = TimeSpan.TryParse(value, out TimeSpan tempTimeValue);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    case "timeFormat":
                        value = DateTime.Now.ToString(value);
                        Convert.ChangeType(value, value.GetTypeCode());
                        result = true;
                        break;

                    case "datetime":
                        result = DateTime.TryParse(value, out DateTime tempDateTimeValue);
                        Convert.ChangeType(value, value.GetTypeCode());
                        break;

                    default: throw new Exception();
                }
            } catch { Convert.ChangeType(value, value.GetTypeCode()); }

            return (value, result);
        }



        /// <summary>
        /// Detection of Value Type for Xaml  Generators 
        /// By Type are Defined Input Fields
        /// </summary>
        /// <param name="typeString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static (object, string) DetectTypeValueFromObject(object fieldForTypeDetect) {
            bool result = false; string typeName = ""; TimeSpan conertTimespan = new TimeSpan();
            try {

                if (!result && bool.TryParse(fieldForTypeDetect.ToString(), out bool tempBit)) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.Boolean);
                    result = true; typeName = "bool";
                }
                if (!result && int.TryParse(fieldForTypeDetect.ToString(), out int tempIntValue)) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.Int32);
                    result = true; typeName = "int";
                }
                if (!result && double.TryParse(fieldForTypeDetect.ToString(), out double tempNumValue)) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.Double);
                    result = true; typeName = "double";
                }
                if (!result && TimeSpan.TryParse(fieldForTypeDetect.ToString(), out TimeSpan tempTimeValue)) {
                    TimeSpan.TryParse(fieldForTypeDetect.ToString(), out conertTimespan);
                    result = true; typeName = "time";
                }
                if (!result && fieldForTypeDetect.ToString().Length < 10 && DateTime.TryParse(fieldForTypeDetect.ToString(), out DateTime tempDateValue)) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.DateTime);
                    result = true; typeName = "date";
                }
                if (!result && DateTime.TryParse(fieldForTypeDetect.ToString(), out DateTime tempDateValue1)) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.DateTime);
                    result = true; typeName = "datetime";
                }
                if (!result) {
                    Convert.ChangeType(fieldForTypeDetect, TypeCode.String);
                    result = true; typeName = "string";
                }

            } catch (Exception ex) { App.ApplicationLogging(new Exception("DetectTypeValueFromObject ProgrammaticOperations : convert failed" + SystemOperations.GetExceptionMessagesAll(ex))); }

            if (typeName == "time") { return (conertTimespan, typeName); }
            else { return (fieldForTypeDetect, typeName); }
        }
    }
}