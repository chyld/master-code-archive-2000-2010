#region assembly references
using System;
using System.Collections.Generic;
#endregion

namespace Chyld.Scribe.Common
{
    public class NoteCT
    {
        #region fields
        private Int64 m_NoteId;
        private DateTime m_DateCreated;
        private Byte[] m_Text;
        private List<Byte[]> m_Tags;
        private Dictionary<Byte[], Byte[]> m_Files;
        private Dictionary<String, String> m_Metadata;
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

        #region property :: public Byte[] Text
        public Byte[] Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        #endregion

        #region property :: public List<Byte[]> Tags
        public List<Byte[]> Tags
        {
            get { return m_Tags; }
            set { m_Tags = value; }
        }
        #endregion

        #region property :: public Dictionary<Byte[], Byte[]> Files
        public Dictionary<Byte[], Byte[]> Files
        {
            get { return m_Files; }
            set { m_Files = value; }
        }
        #endregion

        #region property :: public Dictionary<String, String> Metadata
        public Dictionary<String, String> Metadata
        {
            get { return m_Metadata; }
            set { m_Metadata = value; }
        }
        #endregion
        #endregion

        #region constructors
        #region constructor :: public NoteCT()
        public NoteCT()
        {
            NoteId = 0;
            DateCreated = DateTime.Now;
            Text = null;
            Tags = new List<Byte[]>();
            Files = new Dictionary<Byte[], Byte[]>();
            Metadata = new Dictionary<String, String>();
        }
        #endregion
        #endregion
    }
}
