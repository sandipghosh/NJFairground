

namespace NJFairground.Web.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class StackLogger
    {
        private static System.Text.StringBuilder s_LogText = new System.Text.StringBuilder();

        public static void LogMessage(String p_Message)
        {
            var l_CurrentTime = DateTime.Now;
            s_LogText.Append("******************************************  ");
            s_LogText.Append(l_CurrentTime.Year.ToString());
            s_LogText.Append("-");
            s_LogText.Append(l_CurrentTime.Month.ToString().PadLeft(2, '0'));
            s_LogText.Append("-");
            s_LogText.Append(l_CurrentTime.Day.ToString().PadLeft(2, '0'));
            s_LogText.Append(" ");
            s_LogText.Append(l_CurrentTime.Hour.ToString().PadLeft(2, '0'));
            s_LogText.Append(":");
            s_LogText.Append(l_CurrentTime.Minute.ToString().PadLeft(2, '0'));
            s_LogText.Append(":");
            s_LogText.Append(l_CurrentTime.Second.ToString().PadLeft(2, '0'));
            s_LogText.Append(".");
            s_LogText.Append(l_CurrentTime.Millisecond.ToString().PadLeft(3, '0'));
            s_LogText.Append("  ******************************************");
            s_LogText.AppendLine();
            s_LogText.Append(p_Message);
            s_LogText.AppendLine();
            s_LogText.AppendLine();
        }

        public static String GetCurrentLog()
        {
            return s_LogText.ToString();
        }

        public static void ResetCurrentLog()
        {
            s_LogText = new System.Text.StringBuilder();
        }

        public static void AddCurrentStackDefaultLog()
        {
            LogMessage(CurrentStackDefaultLog());
        }

        // The StackTrace.ToString() method includes calls to Logger methods.
        public static String CurrentStackDefaultLog()
        {
            var l_CurrentStack = new System.Diagnostics.StackTrace(true);  // the true value is used to include source file info
            return l_CurrentStack.ToString();
        }

        public static void AddCurrentStackCustomizedLog()
        {
            LogMessage(CurrentStackCustomizedLog());
        }

        public static String CurrentStackCustomizedLog()
        {
            var l_StackLog = new System.Text.StringBuilder();
            var l_CurrentStack = new System.Diagnostics.StackTrace(true);  // the true value is used to include source file info

            for (Int32 x = 0; x < l_CurrentStack.FrameCount; ++x)
            {
                var l_MethodCall = l_CurrentStack.GetFrame(x);
                if (IsMethodToBeIncluded(l_MethodCall))
                    l_StackLog.AppendLine(MethodCallLog(l_MethodCall));
            }
            return l_StackLog.ToString();
        }

        // This method is used to keep Logger methods out of the returned log
        // (the methods actually included in a StackTrace depend on compiler optimizations).
        private static bool IsMethodToBeIncluded(System.Diagnostics.StackFrame p_StackMethod)
        {
            var l_Method = p_StackMethod.GetMethod();
            if (l_Method.DeclaringType == typeof(StackLogger))
                return false;
            else
                return true;
        }

        // Instead of visiting each field of stackFrame, the StackFrame.ToString() method could be used, 
        // but the returned text would not include the class name.
        private static String MethodCallLog(System.Diagnostics.StackFrame p_MethodCall)
        {
            System.Text.StringBuilder l_MethodCallLog = new System.Text.StringBuilder();

            var l_Method = p_MethodCall.GetMethod();
            l_MethodCallLog.Append(l_Method.DeclaringType.ToString());
            l_MethodCallLog.Append(".");
            l_MethodCallLog.Append(p_MethodCall.GetMethod().Name);

            var l_MethodParameters = l_Method.GetParameters();
            l_MethodCallLog.Append("(");
            for (Int32 x = 0; x < l_MethodParameters.Length; ++x)
            {
                if (x > 0)
                    l_MethodCallLog.Append(", ");
                var l_MethodParameter = l_MethodParameters[x];
                l_MethodCallLog.Append(l_MethodParameter.ParameterType.Name);
                l_MethodCallLog.Append(" ");
                l_MethodCallLog.Append(l_MethodParameter.Name);
            }
            l_MethodCallLog.Append(")");

            var l_SourceFileName = p_MethodCall.GetFileName();
            if (!String.IsNullOrEmpty(l_SourceFileName))
            {
                l_MethodCallLog.Append(" in ");
                l_MethodCallLog.Append(l_SourceFileName);
                l_MethodCallLog.Append(": line ");
                l_MethodCallLog.Append(p_MethodCall.GetFileLineNumber().ToString());
            }

            return l_MethodCallLog.ToString();
        }

    }
}