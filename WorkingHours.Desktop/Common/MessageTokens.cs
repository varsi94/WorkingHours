using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Desktop.Common
{
    public static class MessageTokens
    {
        public static object LoginNotification { get; } = new object();
        public static object SignUpCompleted { get; } = new object();
        public static object StartSignUp { get; } = new object();
    }
}
