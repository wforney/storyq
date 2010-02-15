<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="StoryQRun">
    <html>
      <head>
        <title>StoryQ test run</title>
        <link rel="stylesheet" href="css/jquery.treeview.css" />
        <link rel="stylesheet" href="css/screen.storyq.css" />
        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" />

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script src="jquery.treeview.js" type="text/javascript"></script>
        <script src="jquery.storyq.js" type="text/javascript"></script>
      </head>
      <body>
        
        <div id="results"/>
        
        <script type="text/javascript">
          $(function () {
            $('#results').storyq({
              url: 'StoryQ.xml'
            });
          });
        </script>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
