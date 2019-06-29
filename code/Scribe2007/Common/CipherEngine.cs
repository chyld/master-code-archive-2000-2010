#region assembly references
using System;
using KellermanSoftware.EncryptionLibrary;
#endregion

namespace Chyld.Scribe.Common
{
    public class CipherEngine
    {
        #region fields
        private const String m_Username = "Chyld Medford";
        private const String m_Password = "HUhWwsKTn1FmM1YWt8WZPVzFL/4FGAHvtS0SX/4cPiw=";
        private const String m_AllowableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        private const EncryptionProvider m_EncryptionProvider = EncryptionProvider.Rijndael;
        private Encryption m_Encryption;
        private NoteCT m_NoteCT;
        #endregion

        #region constructors
        #region constructor :: public CipherEngine(NoteCT p_NoteCT)
        public CipherEngine(NoteCT p_NoteCT)
        {
            // -> Checking input parameter
            if(p_NoteCT == null)
                throw new NullReferenceException("NoteCT parameter is null.");

            // -> Instantiating Encryption object with username & password
            m_Encryption = new Encryption(m_Username, m_Password);

            // -> If note doesn't have Key, Salt, or IV, then generate and add to its dictionary
            if(p_NoteCT.Metadata.Count == 0)
            {
                Random random = new Random(DateTime.Now.Millisecond);
                String key = String.Empty;
                String salt = String.Empty;
                String iv = String.Empty;

                for(Int32 count = 0; count < 16; count++)
                    key += m_AllowableCharacters[random.Next(0, 64)];
                for(Int32 count = 0; count < 16; count++)
                    salt += m_AllowableCharacters[random.Next(0, 64)];
                for(Int32 count = 0; count < 16; count++)
                    iv += m_AllowableCharacters[random.Next(0, 64)];

                p_NoteCT.Metadata.Add("Key", key);
                p_NoteCT.Metadata.Add("Salt", salt);
                p_NoteCT.Metadata.Add("IV", iv);
            }

            // -> Set the Encryption object's Key and IV property
            m_Encryption.Salt = p_NoteCT.Metadata["Salt"];
            m_Encryption.InitializationVector = p_NoteCT.Metadata["IV"];

            // -> Save note
            m_NoteCT = p_NoteCT;
        }
        #endregion
        #endregion

        #region methods
        #region method :: public Byte[] EncryptBytes(Byte[] p_PlainText)
        public Byte[] EncryptBytes(Byte[] p_PlainText)
        {
            Byte[] bytes = m_Encryption.EncryptBytes(m_EncryptionProvider, m_NoteCT.Metadata["Key"], p_PlainText);
            if(bytes == null)
                throw new ChyldException("The bytes were not encrypted.");
            return bytes;
        }
        #endregion

        #region method :: public Byte[] DecryptBytes(Byte[] p_CipherText)
        public Byte[] DecryptBytes(Byte[] p_CipherText)
        {
            Byte[] bytes = m_Encryption.DecryptBytes(m_EncryptionProvider, m_NoteCT.Metadata["Key"], p_CipherText);
            if(bytes == null)
                throw new ChyldException("The bytes were not decrypted.");
            return bytes;
        }
        #endregion

        #region method :: public void EncryptFile(String p_InputFile, String p_OutputFile)
        public void EncryptFile(String p_InputFile, String p_OutputFile)
        {
            if(!m_Encryption.EncryptFile(m_EncryptionProvider, m_NoteCT.Metadata["Key"], p_InputFile, p_OutputFile))
                throw new ChyldException("The file was not encrypted.");
        }
        #endregion

        #region method :: public Boolean DecryptFile(String p_InputFile, String p_OutputFile)
        public void DecryptFile(String p_InputFile, String p_OutputFile)
        {
            if(!m_Encryption.DecryptFile(m_EncryptionProvider, m_NoteCT.Metadata["Key"], p_InputFile, p_OutputFile))
                throw new ChyldException("The file was not decrypted.");
        }
        #endregion
        #endregion
    }
}
