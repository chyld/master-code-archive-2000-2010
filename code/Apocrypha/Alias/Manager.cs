using System;
using System.Collections.Generic;
using KellermanSoftware.EncryptionLibrary;

namespace Chyld.Apocrypha.Alias
{
    public class Manager
    {
        private const String m_Username = "Chyld Medford 100721";
        private const String m_Password = "HUhWwsKTn1FmM1YWt8WZPU/UcmFFl7OVmFCqnVnoN0M=";
        private Encryption m_Encryption;
        Dictionary<String, String> m_Output = new Dictionary<String, String>();

        public Dictionary<String, String> Compute(Dictionary<String, String> input)
        {
            ClearAll();

            m_Encryption = new Encryption(m_Username, m_Password);

            String passphrase = input["A"].Trim();
            String crc = input["E"].Trim();
            String plaintext = input["I"].Trim();
            String ciphertext = input["J"].Trim();
            String password = input["D"].Trim();
            String username = input["C"].Trim();
            String encryptionkey = input["N"].Trim();

            if ((!String.IsNullOrEmpty(passphrase)) && (!String.IsNullOrEmpty(crc)) && (crc.Length == 2) && (passphrase.Length >= 2))
            {
                if ((passphrase[0] == crc[0]) && (passphrase[passphrase.Length - 1] == crc[1]))
                {
                    m_Output["B"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.MD5, passphrase))).ToLower();
                    m_Output["F"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.MD5, passphrase.Reverse()))).ToLower();

                    m_Output["C"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.SHA512, passphrase))).MakeUser(16);
                    m_Output["G"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.SHA512, passphrase.Reverse()))).MakeUser(8);

                    m_Output["D"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.Ripemd320, passphrase))).MakePass(16);
                    m_Output["H"] = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.Ripemd320, passphrase.Reverse()))).MakePass(8);
                }
            }

            if (!String.IsNullOrEmpty(plaintext) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(username))
            {
                String hash = m_Encryption.BytesToHexString(m_Encryption.DecodeBase64(m_Encryption.HashString(HashProvider.GostHash, password + username))).MakePass(32);
                m_Output["J"] = m_Encryption.EncryptString(EncryptionProvider.Rijndael, hash, plaintext);
                m_Output["N"] = hash;
            }

            if (!String.IsNullOrEmpty(ciphertext) && !String.IsNullOrEmpty(encryptionkey))
            {
                m_Output["I"] = m_Encryption.DecryptString(EncryptionProvider.Rijndael, encryptionkey, ciphertext);
            }

            return m_Output;
        }

        private void ClearAll()
        {
            m_Output["A"] = "";
            m_Output["B"] = "";
            m_Output["C"] = "";
            m_Output["D"] = "";
            m_Output["E"] = "";
            m_Output["F"] = "";
            m_Output["G"] = "";
            m_Output["H"] = "";
            m_Output["I"] = "";
            m_Output["J"] = "";
            m_Output["K"] = "";
            m_Output["L"] = "";
            m_Output["M"] = "";
            m_Output["N"] = "";
            m_Output["O"] = "";
            m_Output["P"] = "";
            m_Output["Q"] = "";
            m_Output["R"] = "";
            m_Output["S"] = "";
            m_Output["T"] = "";
        }
    }
}
