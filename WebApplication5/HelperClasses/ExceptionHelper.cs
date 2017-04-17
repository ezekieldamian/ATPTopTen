using System;

namespace WebApplication5.HelperClasses
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Return innermost exception if any
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
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