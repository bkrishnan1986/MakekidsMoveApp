using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MakeKidsMoveApp.ErrorHandler
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorMessageType
    {
        Unknown = 0,
        FieldValidation = 1,
        RecordExists = 2,
        RecordNotFound = 3,
        InsertFailed = 4,
        UpdateFailed = 5

    }

    public class ApiException : Exception
    {
        public ApiException()
        {

        }
        public ApiException(string apiErrorMessage) : base(apiErrorMessage)
        {

        }
        [JsonProperty(PropertyName = "recordId")]
        public string RecordId { get; set; }

        [JsonProperty(PropertyName = "ExceptionMessage")]
        public string ExceptionMessage { get; set; }


        [JsonProperty(PropertyName = "errorType")]
        public ErrorMessageType ErrorType { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string ErrorMessage { get; set; }

        //public List<string> ErrorMessages { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "errors")]
        public List<ValidationErrorDetails> DetailErrorDetails { get; set; } = new List<ValidationErrorDetails>();
    }
    public class ValidationErrorDetails
    {
        public string PropertyName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
