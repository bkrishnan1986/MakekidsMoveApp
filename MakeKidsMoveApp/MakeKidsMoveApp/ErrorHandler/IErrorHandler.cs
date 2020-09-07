using System;
using System.Collections.Generic;
using System.Text;

namespace MakeKidsMoveApp.ErrorHandler
{
  public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
