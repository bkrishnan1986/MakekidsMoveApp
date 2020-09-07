using System;
using System.Collections.Generic;
using System.Text;

namespace MakeKidsMoveApp.ErrorHandler
{
  
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
        public List<ValidationErrorDetails> DetailErrorDetails { get; set; } = new List<ValidationErrorDetails>();
    }
    public class ValidationErrorDetails
    {
        public string PropertyName { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
