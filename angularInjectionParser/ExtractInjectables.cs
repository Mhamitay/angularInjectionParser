using System;
using System.Collections.Generic;
using System.Text;

namespace angularInjectionParser
{
    public static class ExtractInjectables
    {
        public static string GetInjectable(string text, string v)
        {
            var t = text;
            var s = t.IndexOf("[");
            t = t.Substring(s);
            var e = t.IndexOf("]);");
            if (e == -1)
            {
                return "";
            }
            t = t.Substring(1, e);
            return t;
        }
        public static string GetInjectableFromService(string text,string serviceName)
        {
            var t = text;
            var s = t.IndexOf("[");
            var e = t.IndexOf("]);");

            if (s == -1)
            {
                return "";
            }
            t = t.Substring(s);
            e = t.IndexOf("]);");
            if (e == -1)
            {
                return "";
            }
            t = t.Substring(1, e -1);

            return t;
        }

        public static string GetInjectableFromDirective(string txt)
        {
            var t = txt;
            var s = t.IndexOf("[");
            t = t.Substring(s);
            var e = t.IndexOf(", function");
            if (e == -1)
            {
                return "";
            }
            t = t.Substring(1, e);
            return t;
        }
    }
}
