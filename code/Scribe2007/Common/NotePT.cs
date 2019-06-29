#region assembly references
using System;
using System.Collections.Generic;
#endregion

namespace Chyld.Scribe.Common
{
    public class NotePT
    {
        #region fields
        private Int64 m_NoteId;
        private DateTime m_DateCreated;
        private String m_Text;
        private List<String> m_Tags;
        private List<String> m_Filenames;
        #endregion

        #region properties
        #region property :: public Int64 NoteId
        public Int64 NoteId
        {
            get { return m_NoteId; }
            set { m_NoteId = value; }
        }
        #endregion

        #region property :: public DateTime DateCreated
        public DateTime DateCreated
        {
            get { return m_DateCreated; }
            set { m_DateCreated = value; }
        }
        #endregion

        #region property :: public String Text
        public String Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        #endregion

        #region property :: public List<String> Tags
        public List<String> Tags
        {
            get { return m_Tags; }
            set { m_Tags = value; }
        }
        #endregion

        #region property :: public List<String> Filenames
        public List<String> Filenames
        {
            get { return m_Filenames; }
            set { m_Filenames = value; }
        }
        #endregion
        #endregion

        #region constructors
        #region constructor :: public NotePT()
        public NotePT()
        {
            NoteId = 0;
            DateCreated = DateTime.Now;
            Text = String.Empty;
            Tags = new List<String>();
            Filenames = new List<String>();
        }
        #endregion
        #endregion
    }
}
