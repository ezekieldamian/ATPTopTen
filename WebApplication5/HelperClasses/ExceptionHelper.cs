using System;

namespace WebApplication5.HelperClasses
{
    public static class ExceptionHelper
    {
        internal static Exception FindInnermostException(Exception ex)
        {
            while (true)
            {
                if (ex.InnerException == null) return ex;
                ex = ex.InnerException;
            }
        }
    }
}