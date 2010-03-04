<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="StoryQRun">
    <html>
      <head>
        <title>StoryQ test run</title>
        <style type="text/css">
          <![CDATA[
body                  {font-family:"Segoe UI", Tahoma, Geneva, Verdana; font-size:85%;}
table                 {border-collapse:collapse; border:1px solid #bbb; margin-left: 50px;}
td                    {padding:1px 10px;}
.prefix               {color: #656565;}
.result               {font-style:italic; background-color: #f3f3f3;}
tr.Failed .result     {background-color: #f4aaaa;}
tr.Pending .result    {background-color: #f4f4aa;}
tr.Passed .result     {background-color: #aaf4aa;}
h2                    {margin-left: 10px;}
h3                    {margin-left: 20px;}
h4                    {margin-left: 30px;}
h5                    {margin-left: 40px;}
.heading              {border-color: #ddd; border-style: solid; border-width: 0px 0px 0px 4px; padding-left: 5px;}
h1.heading            {border-width: 0px 0px 0px 6px;}
h1.heading            {border-width: 0px 0px 0px 5px;}
.categoryIndicator    {color: #888;}
.heading.Failed       {border-color: #f44;}
.heading.Pending      {border-color: #e5e500;}
.heading.Passed       {border-color: #1b2;}
.exceptions           {background-color: #ddd; padding: 10px; border: 1px solid #ccc; margin-top: 50px}
.exceptions pre       {background-color: #eee; padding:5px border: 1px solid #bbb;}
          ]]>
        </style>
      </head>
      <body>
        <xsl:apply-templates select="Project">
          <xsl:sort select="@Name"/>
        </xsl:apply-templates>
        <xsl:apply-templates select="Namespace">
          <xsl:sort select="@Name"/>
        </xsl:apply-templates>
        <xsl:apply-templates select="Class">
          <xsl:sort select="@Name"/>
        </xsl:apply-templates>
        <xsl:apply-templates select="Method">
          <xsl:sort select="@Name"/>
        </xsl:apply-templates>

        <xsl:if test="//Exception">
          <xsl:call-template name="ExceptionAppendix">
            <xsl:with-param name="Exceptions" select="//Exception"/>
          </xsl:call-template>
        </xsl:if>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Project">
    <h1>
      <xsl:call-template name="applyStatus"/>
      <xsl:value-of select="@Name"/>
      <span class="categoryIndicator">.dll</span>
    </h1>
    <xsl:apply-templates select="Namespace">
      <xsl:sort select="@Name"/>
    </xsl:apply-templates>
  </xsl:template>

  <xsl:template match="Namespace">
    <h2>
      <xsl:call-template name="applyStatus"/>
      <xsl:value-of select="@Name"/>
      <span class="categoryIndicator"> {}</span>
    </h2>
    <xsl:apply-templates select="Class">
      <xsl:sort select="@Name"/>
    </xsl:apply-templates>
  </xsl:template>

  <xsl:template match="Class">
    <h3>
      <xsl:call-template name="applyStatus"/>
      <xsl:value-of select="@Name"/>
    </h3>
    <xsl:apply-templates select="Method"/>
  </xsl:template>

  <xsl:template match="Method">
    <h4>
      <xsl:call-template name="applyStatus"/>
      <xsl:value-of select="@Name"/>
    </h4>
    <xsl:apply-templates select="Story"/>
  </xsl:template>

  <xsl:template match="Story">
    <h5>
      <xsl:call-template name="applyStatus"/>
      <xsl:value-of select="@Name"/>
    </h5>
    <table>
      <xsl:apply-templates select="Result"/>
    </table>
  </xsl:template>

  <xsl:template match="Result">
    <tr>
      <xsl:attribute name="class">
        <xsl:value-of select="@Type"/>
      </xsl:attribute>
      <td>
        <span class="prefix">
          <xsl:call-template name="repeatText">
            <xsl:with-param name="text" select="'&#xA0;&#xA0;'"/>
            <xsl:with-param name="count" select="number(@IndentLevel)"/>
          </xsl:call-template>
          <xsl:value-of select="@Prefix"/>
        </span>
        <xsl:text> </xsl:text>
        <span class="text">
          <xsl:value-of select="@Text"/>
        </span>
      </td>
      <td class="result">
        <xsl:choose>
          <xsl:when test="@Type != 'NotExecutable'">
            <xsl:value-of select="@Type"/>
            <xsl:if test="Exception">
              <xsl:text>: </xsl:text>
              <a class="exception">
                <xsl:attribute name="href">
                  <xsl:text>#Exception</xsl:text>
                  <xsl:value-of select="Exception/@ID"/>
                </xsl:attribute>
                <xsl:attribute name="title">
                  <xsl:value-of select="Exception"/>
                </xsl:attribute>
                <xsl:text> </xsl:text>
                <xsl:value-of select="Exception/@Message"/>
                <xsl:text> [</xsl:text>
                <xsl:value-of select="Exception/@ID"/>
                <xsl:text>]</xsl:text>
              </a>
            </xsl:if>
          </xsl:when>
          <xsl:otherwise>
            <xsl:text>&#xA0;</xsl:text>
          </xsl:otherwise>
        </xsl:choose>
      </td>

    </tr>
  </xsl:template>


  <xsl:template name="ExceptionAppendix">
    <xsl:param name="Exceptions" />
    <div class="exceptions">
      <h1>Exceptions</h1>
      <xsl:for-each select="$Exceptions">
        <a>
          <xsl:attribute name="name">
            <xsl:text>Exception</xsl:text>
            <xsl:value-of select="./@ID"/>
          </xsl:attribute>
          <b>
            <xsl:value-of select="./@Message"/>
            <xsl:text> [</xsl:text>
            <xsl:value-of select="./@ID"/>
            <xsl:text>]</xsl:text>
          </b>
        </a>
        <pre>
          <xsl:value-of select="."/>
        </pre>
        <br/>
      </xsl:for-each>
    </div>
  </xsl:template>

  <xsl:template name="applyStatus">
    <xsl:attribute name="class">
      <xsl:text>heading </xsl:text>
      <xsl:choose>
        <xsl:when test=".//Result[@Type='Failed']">
          <xsl:text>Failed</xsl:text>
        </xsl:when>
        <xsl:when test=".//Result[@Type='Pending']">
          <xsl:text>Pending</xsl:text>
        </xsl:when>
        <xsl:when test=".//Result[@Type='Passed']">
          <xsl:text>Passed</xsl:text>
        </xsl:when>
      </xsl:choose>
    </xsl:attribute>

    <xsl:attribute name="title">
      <xsl:call-template name="pluraliser">
        <xsl:with-param name="count" select="count(.//Result[@Type='Failed'])"/>
        <xsl:with-param name="description" select="'failing step'"/>
      </xsl:call-template>
      <xsl:text>, </xsl:text>
      <xsl:call-template name="pluraliser">
        <xsl:with-param name="count" select="count(.//Result[@Type='Pending'])"/>
        <xsl:with-param name="description" select="'pending step'"/>
      </xsl:call-template>
      <xsl:text>, </xsl:text>
      <xsl:call-template name="pluraliser">
        <xsl:with-param name="count" select="count(.//Result[@Type='Passed'])"/>
        <xsl:with-param name="description" select="'passing step'"/>
      </xsl:call-template>
      <xsl:text>, and </xsl:text>
      <xsl:call-template name="pluraliser">
        <xsl:with-param name="count" select="count(.//Result[@Type='NotExecutable'])"/>
        <xsl:with-param name="description" select="'non-executable step'"/>
      </xsl:call-template>
    </xsl:attribute>
  </xsl:template>

  <xsl:template name="pluraliser">
    <xsl:param name="count"/>
    <xsl:param name="description"/>
    <xsl:value-of select="$count"/>
    <xsl:text> </xsl:text>
    <xsl:value-of select="$description"/>
    <xsl:if test="$count != 1">
      <xsl:text>s</xsl:text>
    </xsl:if>
  </xsl:template>

  <xsl:template name="repeatText">
    <xsl:param name="text"/>
    <xsl:param name="count"/>
    <xsl:if test="$count > 0">
      <xsl:value-of select="$text"/>
      <xsl:call-template name="repeatText">
        <xsl:with-param name="text" select="$text"/>
        <xsl:with-param name="count" select="$count - 1"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
    

</xsl:stylesheet>
