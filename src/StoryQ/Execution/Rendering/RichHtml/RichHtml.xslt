<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <!--Disable Quirks Mode in IE-->
    <xsl:output method="html"
                indent="yes"
                doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN"
                doctype-system="http://www.w3.org/TR/html4/loose.dtd"
            />

    <xsl:key name="tag-key" match="Tag" use="text()"/>


    <xsl:template match="StoryQRun">
        <html>
            <head>
                <title>StoryQ Report</title>
                <script src="jquery-1.4.2.min.js" type="text/javascript"/>
                <script src="jquery.tagcloud.min.js" type="text/javascript"/>
                <script src="jquery.treeview.min.js" type="text/javascript"/>
                <script src="storyq.js" type="text/javascript"/>
                <link href="storyq.css" rel="stylesheet"/>
            </head>
            <body>
                <div id="filters">
                    <div class="halfPanel">
                        <div class="border">
                            <h1>Class Hierarchy</h1>
                            <ul id="tree">
                                <li>
                                    <xsl:call-template name="treeItem"/>
                                    <ul>
                                        <xsl:for-each select="Project">
                                            <xsl:sort select="@Name"/>
                                            <li class="project">
                                                <xsl:call-template name="treeItem"/>
                                                <ul>
                                                    <xsl:for-each select="Namespace">
                                                        <xsl:sort select="@Name"/>
                                                        <li class="namespace">
                                                            <xsl:call-template name="treeItem"/>
                                                            <ul>
                                                                <xsl:for-each select="Class">
                                                                    <xsl:sort select="@Name"/>
                                                                    <li class="class">
                                                                        <xsl:call-template name="treeItem"/>
                                                                        <ul>
                                                                            <xsl:for-each select="Method">
                                                                                <xsl:sort select="@Name"/>
                                                                                <li class="method">
                                                                                    <xsl:call-template name="treeItem"/>
                                                                                </li>
                                                                            </xsl:for-each>
                                                                        </ul>
                                                                    </li>
                                                                </xsl:for-each>
                                                            </ul>
                                                        </li>
                                                    </xsl:for-each>
                                                </ul>
                                            </li>

                                        </xsl:for-each>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="halfPanel">
                        <div class="border">
                            <h1>Tags</h1>
                            <ul id="tags">
                                <xsl:for-each select="//Tag[generate-id(.)=generate-id(key('tag-key',text()))]/text()">
                                    <xsl:variable name="tag" select="."/>
                                    <xsl:variable name="pending" select="count(//Tag[text()=$tag and ../@Type='Pending'])"/>
                                    <xsl:variable name="failed" select="count(//Tag[text()=$tag and ../@Type='Failed'])"/>
                                    <xsl:variable name="passed" select="count(//Tag[text()=$tag and ../@Type='Passed'])"/>
                                    <xsl:variable name="notExecutable" select="count(//Tag[text()=$tag and ../@Type='NotExecutable'])"/>
                                    <li>
                                        <xsl:attribute name="value">
                                            <xsl:value-of select="count(//Tag[text()=$tag])"/>
                                        </xsl:attribute>
                                        <xsl:attribute name="title">
                                            <xsl:text>Passed: </xsl:text>
                                            <xsl:value-of select="$passed"/>
                                            <xsl:text>, Pending: </xsl:text>
                                            <xsl:value-of select="$pending"/>
                                            <xsl:text>, Failed: </xsl:text>
                                            <xsl:value-of select="$failed"/>
                                            <xsl:text>, Non-executable: </xsl:text>
                                            <xsl:value-of select="$notExecutable"/>
                                        </xsl:attribute>
                                        <a>
                                            <xsl:attribute name="href">
                                                <xsl:text>#</xsl:text>
                                                <xsl:call-template name="string-replace-all">
                                                    <xsl:with-param name="text" select="." />
                                                    <xsl:with-param name="replace" select="' '" />
                                                    <xsl:with-param name="by" select="'+'" />
                                                </xsl:call-template>
                                            </xsl:attribute>
                                            <xsl:value-of select="."/>

                                            <xsl:call-template name="progressBars">
                                                <xsl:with-param name="pending" select="$pending"/>
                                                <xsl:with-param name="failed" select="$failed"/>
                                                <xsl:with-param name="passed" select="$passed"/>
                                                <xsl:with-param name="notExecutable" select="$notExecutable"/>
                                            </xsl:call-template>
                                        </a>
                                    </li>
                                </xsl:for-each>
                            </ul>
                        </div>
                    </div>
                </div>


                <div id="stories">
                    <h1>Stories</h1>
                    <ul>
                        <xsl:for-each select="Project">
                            <xsl:sort select="@Name"/>
                            <li class="project">
                                <xsl:call-template name="storyItemHeading"/>
                                <ul>
                                    <xsl:for-each select="Namespace">
                                        <xsl:sort select="@Name"/>
                                        <li class="namespace">
                                            <xsl:call-template name="storyItemHeading"/>
                                            <ul>
                                                <xsl:for-each select="Class">
                                                    <xsl:sort select="@Name"/>
                                                    <li class="class">
                                                        <xsl:call-template name="storyItemHeading"/>
                                                        <ul>
                                                            <xsl:for-each select="Method">
                                                                <xsl:sort select="@Name"/>
                                                                <li class="method">
                                                                    <xsl:call-template name="storyItemHeading"/>
                                                                    <xsl:for-each select="Story">

                                                                        <div class="story">
                                                                            <xsl:attribute name="classHierarchy">
                                                                                <xsl:call-template name="namePath">
                                                                                    <xsl:with-param name="element" select=".."/>
                                                                                </xsl:call-template>
                                                                            </xsl:attribute>
                                                                            <xsl:attribute name="tags">
                                                                                <xsl:for-each select="Result/Tag">
                                                                                    <xsl:call-template name="string-replace-all">
                                                                                        <xsl:with-param name="text" select="text()" />
                                                                                        <xsl:with-param name="replace" select="' '" />
                                                                                        <xsl:with-param name="by" select="'+'" />
                                                                                    </xsl:call-template>
                                                                                    <xsl:text> </xsl:text>
                                                                                </xsl:for-each>
                                                                            </xsl:attribute>
                                                                            <table>
                                                                                <xsl:for-each select="Result">
                                                                                    <xsl:call-template name="resultRow"/>
                                                                                </xsl:for-each>
                                                                            </table>
                                                                        </div>
                                                                    </xsl:for-each>
                                                                </li>
                                                            </xsl:for-each>
                                                        </ul>
                                                    </li>
                                                </xsl:for-each>
                                            </ul>
                                        </li>
                                    </xsl:for-each>
                                </ul>
                            </li>
                        </xsl:for-each>
                    </ul>
                </div>
                <div id="mask"/>
                <div id="dialog">
                    <div id="dialogHeading">
                        Stack trace
                        <a href="javascript:" title="Close popup">X</a>
                    </div>
                    <div id="dialogContent"/>
                </div>
            </body>
        </html>
    </xsl:template>

    <xsl:template name="storyItemHeading">
        <xsl:attribute name="value">
            <xsl:call-template name="namePath">
                <xsl:with-param name="element" select="."/>
            </xsl:call-template>
        </xsl:attribute>
        <a class="itemHeading">
            <xsl:attribute name="name">
                <xsl:call-template name="namePath">
                    <xsl:with-param name="element" select="."/>
                </xsl:call-template>
            </xsl:attribute>
            <xsl:attribute name="title">
                <xsl:call-template name="summariseSubSteps"/>
            </xsl:attribute>
            <xsl:call-template name="subStepIcon"/>
            <div class="icon auto"/>

            <xsl:value-of select="@Name"/>
        </a>
    </xsl:template>

    <xsl:template name="treeItem">
        <xsl:variable name="summary">
            <xsl:call-template name="summariseSubSteps"/>
        </xsl:variable>
        <a>
            <xsl:attribute name="title">
                <xsl:value-of select="$summary"/>
            </xsl:attribute>
            <xsl:attribute name="href">
                <xsl:text>#</xsl:text>
                <xsl:call-template name="namePath">
                    <xsl:with-param name="element" select="."/>
                </xsl:call-template>
            </xsl:attribute>
            <xsl:if test="name(.)='StoryQRun'">
                <xsl:attribute name="class">selected</xsl:attribute>
            </xsl:if>

            <xsl:call-template name="subStepIcon"/>
            <xsl:choose>
                <xsl:when test="@Name">
                    <div class="icon auto"/>
                    <span>
                        <xsl:value-of select="@Name"/>
                    </span>
                </xsl:when>
                <xsl:otherwise>
                    <span>
                        <i>All Projects</i>
                        <xsl:text> (</xsl:text>
                        <xsl:value-of select="$summary"/>
                        <xsl:text>)</xsl:text>
                    </span>

                </xsl:otherwise>
            </xsl:choose>
        </a>
    </xsl:template>

    <xsl:template name="subStepIcon">
        <div>
            <xsl:attribute name="class">
                <xsl:text>icon </xsl:text>
                <xsl:choose>
                    <xsl:when test=".//Result[@Type='Failed']">
                        <xsl:text>failed</xsl:text>
                    </xsl:when>
                    <xsl:when test=".//Result[@Type='Pending']">
                        <xsl:text>pending</xsl:text>
                    </xsl:when>
                    <xsl:when test=".//Result[@Type='Passed']">
                        <xsl:text>passed</xsl:text>
                    </xsl:when>
                </xsl:choose>
            </xsl:attribute>
        </div>
    </xsl:template>

    <xsl:template name="summariseSubSteps">
        <xsl:text>Passed: </xsl:text>
        <xsl:value-of select="count(.//Result[@Type='Passed'])"/>
        <xsl:text>, Pending: </xsl:text>
        <xsl:value-of select="count(.//Result[@Type='Pending'])"/>
        <xsl:text>, Failed: </xsl:text>
        <xsl:value-of select="count(.//Result[@Type='Failed'])"/>
        <xsl:text>, Non-executable: </xsl:text>
        <xsl:value-of select="count(.//Result[@Type='NotExecutable'])"/>
    </xsl:template>

    <xsl:template name="namePath">
        <xsl:param name="element"/>
        <xsl:if test="$element/../@Name">
            <xsl:call-template name="namePath">
                <xsl:with-param name="element" select="$element/.."/>
            </xsl:call-template>
        </xsl:if>
        <xsl:text>/</xsl:text>
        <xsl:value-of select="$element/@Name"/>
    </xsl:template>

    <xsl:template name="resultRow">
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
                            <a class="exception" href="javascript:">
                                <xsl:attribute name="title">
                                    <xsl:value-of select="Exception"/>
                                </xsl:attribute>
                                <xsl:value-of select="Exception/@Message"/>
                            </a>
                        </xsl:if>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:text>&#xA0;</xsl:text>
                    </xsl:otherwise>
                </xsl:choose>
                <xsl:for-each select="Tag">
                    <xsl:text> #</xsl:text>
                    <xsl:value-of select="."/>
                </xsl:for-each>
            </td>
        </tr>
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

    <xsl:template name="progressBars">
        <xsl:param name="pending"/>
        <xsl:param name="failed"/>
        <xsl:param name="passed"/>
        <xsl:param name="notExecutable"/>
        <xsl:variable name="total" select="number($pending)+number($failed)+number($passed)+number($notExecutable)"/>


        <div class="progressBar">
            <div class="passed">
                <xsl:attribute name="style">
                    <xsl:text>right:</xsl:text>
                    <xsl:value-of select="(number($notExecutable) div $total)*100"/>
                    <xsl:text>%;</xsl:text>
                </xsl:attribute>
            </div>
            <div class="failed">
                <xsl:attribute name="style">
                    <xsl:text>right:</xsl:text>
                    <xsl:value-of select="((number($passed)+number($notExecutable)) div $total)*100"/>
                    <xsl:text>%;</xsl:text>
                </xsl:attribute>
            </div>
            <div class="pending">
                <xsl:attribute name="style">
                    <xsl:text>right:</xsl:text>
                    <xsl:value-of select="((number($passed)+number($failed)+number($notExecutable)) div $total)*100"/>
                    <xsl:text>%;</xsl:text>
                </xsl:attribute>
            </div>
        </div>
    </xsl:template>

    <xsl:template name="string-replace-all">
        <xsl:param name="text" />
        <xsl:param name="replace" />
        <xsl:param name="by" />
        <xsl:choose>
            <xsl:when test="contains($text, $replace)">
                <xsl:value-of select="substring-before($text,$replace)" />
                <xsl:value-of select="$by" />
                <xsl:call-template name="string-replace-all">
                    <xsl:with-param name="text"
                    select="substring-after($text,$replace)" />
                    <xsl:with-param name="replace" select="$replace" />
                    <xsl:with-param name="by" select="$by" />
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$text" />
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

</xsl:stylesheet>
