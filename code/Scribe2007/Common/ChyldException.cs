#region assembly references
using System;
#endregion

namespace Chyld.Scribe.Common
{
    public class ChyldException : ApplicationException
    {
        #region constructors
        #region constructor :: public ChyldException()
        public ChyldException() : base()
        {
        }
        #endregion

        #region constructor :: public ChyldException(String p_Message)
        public ChyldException(String p_Message) : base(p_Message)
        {
        }
        #endregion

        #region constructor ::public ChyldException(String p_Message, Exception p_InnerException)
        public ChyldException(String p_Message, Exception p_InnerException) : base(p_Message, p_InnerException)
        {
        }
        #endregion
        #endregion
    }
}
