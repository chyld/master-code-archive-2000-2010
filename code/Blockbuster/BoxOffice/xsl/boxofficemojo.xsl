<?xml version="1.0" ?>
<!--

//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : boxofficemojo.xsl
// Description : Used in XSLT conversion.  Converting HTML from boxofficemojo.com into a 
//               structured XML file.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

-->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<movies>
			<xsl:for-each select="/html/body/div/center/table/tr/td[2]/table[2]/tr/td/center/table/tr[2]/td/table/tr">
				<xsl:if test="(position() &gt; 1) and (position() &lt; 12)">
					<movie>
						<rank><xsl:value-of select="td[1]/font[1]" /></rank>
						<title><xsl:value-of select="td[2]/font[1]" /></title>
						<studio><xsl:value-of select="td[2]/font[2]" /></studio>
						<theaters><xsl:value-of select="translate(td[2]/font[3], ',- ', '')" /></theaters>
						<xsl:for-each select="td">
							<xsl:if test="(position() &gt; 2) and (b)">
								<dailygross><xsl:value-of select="translate(b/font[1], ',$- ', '')" /></dailygross>

                <xsl:variable name="fontposition">
                  <xsl:choose>
                    <xsl:when test="b/font[@color='#aaaaaa']">3</xsl:when>
                    <xsl:otherwise>2</xsl:otherwise>
                  </xsl:choose>
                </xsl:variable>

                <percentchange><xsl:value-of select="translate(b/font[$fontposition + 0]/text(), ',% ', '')" /></percentchange>
                <average><xsl:value-of select="translate(substring-after(b/font[$fontposition + 0]/font[1], '/'), ',$- ', '')" /></average>
								<xsl:variable name="composite" select="b/font[$fontposition + 1]" />
								<totalgross><xsl:value-of select="translate(substring-before($composite, '/'), ',$- ', '')" /></totalgross>
								<daynumber><xsl:value-of select="translate(substring-after($composite, '/'), ',- ', '')" /></daynumber>
							</xsl:if>
						</xsl:for-each>
					</movie>
				</xsl:if>
			</xsl:for-each>
		</movies>
	</xsl:template>
</xsl:stylesheet>

<!--

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

-->
