using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.Views
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
