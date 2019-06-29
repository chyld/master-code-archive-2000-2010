//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : Transform.cs
// Description : Manages the XSLT.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.Xml;
using HtmlAgilityPack;

namespace Cingulariti.ANDROMEDA.HtmlParser
{
    public static class Transform
    {
        public static void HtmlToXml(String p_InputUrl, String p_InputXslFilePath, String p_OutputXmlFilePath)
        {
            HtmlWeb hw = new HtmlWeb();

            using (XmlTextWriter xmlWriter = new XmlTextWriter(p_OutputXmlFilePath, System.Text.Encoding.UTF8))
            {
                hw.LoadHtmlAsXml(p_InputUrl, p_InputXslFilePath, null, xmlWriter);
                xmlWriter.Flush();
            }
        }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
