using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VikingOne.Common
{
    public class Source : IEnumerator<CharacterTelemetry>
    {
        private Int32 m_Index;
        private String m_FilePath;
        private List<CharacterTelemetry> m_Characters;
        private IEnumerator<CharacterTelemetry> m_Enumerator;

        public Int32 Length
        {
            get { return m_Characters.Count; }
        }

        public Int32 Index
        {
            get { return m_Index; }
        }

        public CharacterTelemetry Current
        {
            get { return m_Enumerator.Current; }
        }

        Object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }
        
        public Source(String filePath)
        {
            m_Index = -1;
            m_FilePath = filePath;
            m_Characters = new List<CharacterTelemetry>();
            LoadFileIntoMemory();
            m_Enumerator = m_Characters.GetEnumerator();
            MoveNext();
        }

        private void LoadFileIntoMemory()
        {
            using (FileStream fs = File.OpenRead(m_FilePath))
            {
                Byte[] b = new Byte[1024];
                UTF8Encoding utf8 = new UTF8Encoding(true);

                Int32 x = 0;
                Int32 y = 0;
                Char tmp = Utility.NullChar;

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    foreach (var c in utf8.GetString(b))
                    {
                        if (c == Utility.NullChar)
                            break;

                        if (Utility.NewLine.Any(nl => nl == tmp) && Utility.NewLine.All(nl => nl != c))
                        {
                            x = 0;
                            y++;
                        }

                        m_Characters.Add(new CharacterTelemetry { Character = c, X = x++, Y = y });
                        tmp = c;
                    }
                }
            }
        }

        public CharacterTelemetry View(Int32 position)
        {
            Int32 index = Index + position;

            if(index >= 0 && index < Length)
                return m_Characters[index];

            return null;
        }

        public Boolean MoveNext()
        {
            if(Index < Length)
                m_Index++;

            return m_Enumerator.MoveNext();
        }

        public void Reset()
        {
        }

        public void Dispose()
        {
        }
    }
}
