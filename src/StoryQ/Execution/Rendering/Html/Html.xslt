<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" indent="yes"/>
  <xsl:template match="StoryQRun">
    <html>
      <head>
        <title>StoryQ test run</title>
        <style>
          <![CDATA[

/* treeview.css
----------------------------------*/
.treeview,.treeview ul{padding:0;margin:0;list-style:none;}
.treeview ul{background-color:white;margin-top:4px;}
.treeview .hitarea{background:url(images/treeview-default.gif) -64px -25px no-repeat;height:16px;width:16px;margin-left:-16px;float:left;cursor:pointer;}
/* fix for IE6 */
* html .hitarea{display:inline;float:none;}
.treeview li{margin:0;padding:3px 0pt 3px 16px;}
.treeview a.selected{background-color:#eee;}
#treecontrol{margin:1em 0;display:none;}
.treeview .hover{color:red;cursor:pointer;}
.treeview li{background:url(images/treeview-default-line.gif) 0 0 no-repeat;}
.treeview li.collapsable,.treeview li.expandable{background-position:0 -176px;}
.treeview .expandable-hitarea{background-position:-80px -3px;}
.treeview li.last{background-position:0 -1766px}
.treeview li.lastCollapsable,.treeview li.lastExpandable{background-image:url(images/treeview-default.gif);}
.treeview li.lastCollapsable{background-position:0 -111px}
.treeview li.lastExpandable{background-position:-32px -67px}
.treeview div.lastCollapsable-hitarea,.treeview div.lastExpandable-hitarea{background-position:0;}
.treeview-red li{background-image:url(images/treeview-red-line.gif);}
.treeview-red .hitarea,.treeview-red li.lastCollapsable,.treeview-red li.lastExpandable{background-image:url(images/treeview-red.gif);}
.treeview-black li{background-image:url(images/treeview-black-line.gif);}
.treeview-black .hitarea,.treeview-black li.lastCollapsable,.treeview-black li.lastExpandable{background-image:url(images/treeview-black.gif);}
.treeview-gray li{background-image:url(images/treeview-gray-line.gif);}
.treeview-gray .hitarea,.treeview-gray li.lastCollapsable,.treeview-gray li.lastExpandable{background-image:url(images/treeview-gray.gif);}
.treeview-famfamfam li{background-image:url(images/treeview-famfamfam-line.gif);}
.treeview-famfamfam .hitarea,.treeview-famfamfam li.lastCollapsable,.treeview-famfamfam li.lastExpandable{background-image:url(images/treeview-famfamfam.gif);}
.filetree li{padding:3px 0 2px 16px;}
.filetree span.folder,.filetree span.file{padding:1px 0 1px 16px;display:block;}
.filetree span.folder{background:url(images/folder.gif) 0 0 no-repeat;}
.filetree li.expandable span.folder{background:url(images/folder-closed.gif) 0 0 no-repeat;}
.filetree span.file{background:url(images/file.gif) 0 0 no-repeat;}

/* storyq.css
----------------------------------*/
/* Reset Base Font Size
----------------------------------*/
html,body{height:100%;margin:0;padding:0;}
html>body{font-size:16px;font-size:68.75%;}
body{font-family:Verdana,helvetica,arial,sans-serif;font-size:68.75%;background:#fff;color:#333;}
h1,h2{font-family:'trebuchet ms',verdana,arial;padding:1em;margin:0}
h1{font-size:large}
a img{border:none;}

/* Global settings for widget
----------------------------------*/
#results{padding:1em;}
#tree{border :1px solid #DDDDDD;border-bottom :5px solid #BEBEBE;padding:1em;}

/* Treeview
----------------------------------*/
h2.results{margin-bottom:1em;}
.failed{color:red;}
.passed{color:green;}
.pending{color:orange;}
.count{color:gray;font-style:italic;}
.storyq ul{margin-top:0;}
.storyq li{padding:0px 0 0px 16px;}
span.result{padding:1px 0 1px 16px;display:block;}
span.namespace,.storyq span.project{padding:1px 0 1px 34px;display:block;}
span.total{background:no-repeat;left:50%;position:absolute;}
span.result:hover{cursor:pointer;}

li>span.passed{background:url(images/results.png) 0 0 no-repeat;}
span.passedproject{background:url(images/results.png) 0 -24px no-repeat;}
span.passedscenario{background:url(images/results.png) 0 -49px no-repeat;}
li>span.failed{background:url(images/results.png) 0 -72px no-repeat;}
span.failedproject{background:url(images/results.png) 0 -95px no-repeat;}
span.failedscenario{background:url(images/results.png) 0 -120px no-repeat;}
li>span.pending{background:url(images/results.png) 0 -143px no-repeat;}
span.pendingproject{background:url(images/results.png) 0 -165px no-repeat;}
span.pendingscenario{background:url(images/results.png) 0 -190px no-repeat;}

/* Pane 
----------------------------------*/
h3.pending{background:#E6D853;color:black;}
h3.passed{background:#8EE486;color:black;}
h3.failed{background:#FFAABF;color:black;}
#summary{padding:0em;}
.summary{padding-bottom:1em;border:1px solid #DDDDDD;border-top:none;}
result{left:60%;position:absolute;}
.scenario{padding-top:1em;}
code.story{font-size:12px;padding:1em;}
story,exception,exceptions{display:block;}
exceptions,exception{padding:1em;}
.indent0{margin-left:20px;}
.indent1{margin-left:40px;}
.indent2{margin-left:60px;}
.indent3{margin-left:80px;}
.indent4{margin-left:100px;}
.indent5{margin-left:120px;}

          
          ]]>
        </style>

        <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/start/jquery-ui.css" type="text/css" />

        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
        <script>
   <![CDATA[
 
 /*
 * Treeview 1.4 - jQuery plugin to hide and show branches of a tree
 * 
 * http://bassistance.de/jquery-plugins/jquery-plugin-treeview/
 * http://docs.jquery.com/Plugins/Treeview
 *
 * Copyright (c) 2007 Jörn Zaefferer
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 * Revision: $Id: jquery.treeview.js 4684 2008-02-07 19:08:06Z joern.zaefferer $
 *
 */

;(function($) {

	$.extend($.fn, {
		swapClass: function(c1, c2) {
			var c1Elements = this.filter('.' + c1);
			this.filter('.' + c2).removeClass(c2).addClass(c1);
			c1Elements.removeClass(c1).addClass(c2);
			return this;
		},
		replaceClass: function(c1, c2) {
			return this.filter('.' + c1).removeClass(c1).addClass(c2).end();
		},
		hoverClass: function(className) {
			className = className || "hover";
			return this.hover(function() {
				$(this).addClass(className);
			}, function() {
				$(this).removeClass(className);
			});
		},
		heightToggle: function(animated, callback) {
			animated ?
				this.animate({ height: "toggle" }, animated, callback) :
				this.each(function(){
					jQuery(this)[ jQuery(this).is(":hidden") ? "show" : "hide" ]();
					if(callback)
						callback.apply(this, arguments);
				});
		},
		heightHide: function(animated, callback) {
			if (animated) {
				this.animate({ height: "hide" }, animated, callback);
			} else {
				this.hide();
				if (callback)
					this.each(callback);				
			}
		},
		prepareBranches: function(settings) {
			if (!settings.prerendered) {
				// mark last tree items
				this.filter(":last-child:not(ul)").addClass(CLASSES.last);
				// collapse whole tree, or only those marked as closed, anyway except those marked as open
				this.filter((settings.collapsed ? "" : "." + CLASSES.closed) + ":not(." + CLASSES.open + ")").find(">ul").hide();
			}
			// return all items with sublists
			return this.filter(":has(>ul)");
		},
		applyClasses: function(settings, toggler) {
			this.filter(":has(>ul):not(:has(>a))").find(">span").click(function(event) {
				toggler.apply($(this).next());
			}).add( $("a", this) ).hoverClass();
			
			if (!settings.prerendered) {
				// handle closed ones first
				this.filter(":has(>ul:hidden)")
						.addClass(CLASSES.expandable)
						.replaceClass(CLASSES.last, CLASSES.lastExpandable);
						
				// handle open ones
				this.not(":has(>ul:hidden)")
						.addClass(CLASSES.collapsable)
						.replaceClass(CLASSES.last, CLASSES.lastCollapsable);
						
	            // create hitarea
				this.prepend("<div class=\"" + CLASSES.hitarea + "\"/>").find("div." + CLASSES.hitarea).each(function() {
					var classes = "";
					$.each($(this).parent().attr("class").split(" "), function() {
						classes += this + "-hitarea ";
					});
					$(this).addClass( classes );
				});
			}
			
			// apply event to hitarea
			this.find("div." + CLASSES.hitarea).click( toggler );
		},
		treeview: function(settings) {
			
			settings = $.extend({
				cookieId: "treeview"
			}, settings);
			
			if (settings.add) {
				return this.trigger("add", [settings.add]);
			}
			
			if ( settings.toggle ) {
				var callback = settings.toggle;
				settings.toggle = function() {
					return callback.apply($(this).parent()[0], arguments);
				};
			}
		
			// factory for treecontroller
			function treeController(tree, control) {
				// factory for click handlers
				function handler(filter) {
					return function() {
						// reuse toggle event handler, applying the elements to toggle
						// start searching for all hitareas
						toggler.apply( $("div." + CLASSES.hitarea, tree).filter(function() {
							// for plain toggle, no filter is provided, otherwise we need to check the parent element
							return filter ? $(this).parent("." + filter).length : true;
						}) );
						return false;
					};
				}
				// click on first element to collapse tree
				$("a:eq(0)", control).click( handler(CLASSES.collapsable) );
				// click on second to expand tree
				$("a:eq(1)", control).click( handler(CLASSES.expandable) );
				// click on third to toggle tree
				$("a:eq(2)", control).click( handler() ); 
			}
		
			// handle toggle event
			function toggler() {
				$(this)
					.parent()
					// swap classes for hitarea
					.find(">.hitarea")
						.swapClass( CLASSES.collapsableHitarea, CLASSES.expandableHitarea )
						.swapClass( CLASSES.lastCollapsableHitarea, CLASSES.lastExpandableHitarea )
					.end()
					// swap classes for parent li
					.swapClass( CLASSES.collapsable, CLASSES.expandable )
					.swapClass( CLASSES.lastCollapsable, CLASSES.lastExpandable )
					// find child lists
					.find( ">ul" )
					// toggle them
					.heightToggle( settings.animated, settings.toggle );
				if ( settings.unique ) {
					$(this).parent()
						.siblings()
						// swap classes for hitarea
						.find(">.hitarea")
							.replaceClass( CLASSES.collapsableHitarea, CLASSES.expandableHitarea )
							.replaceClass( CLASSES.lastCollapsableHitarea, CLASSES.lastExpandableHitarea )
						.end()
						.replaceClass( CLASSES.collapsable, CLASSES.expandable )
						.replaceClass( CLASSES.lastCollapsable, CLASSES.lastExpandable )
						.find( ">ul" )
						.heightHide( settings.animated, settings.toggle );
				}
			}
			
			function serialize() {
				function binary(arg) {
					return arg ? 1 : 0;
				}
				var data = [];
				branches.each(function(i, e) {
					data[i] = $(e).is(":has(>ul:visible)") ? 1 : 0;
				});
				$.cookie(settings.cookieId, data.join("") );
			}
			
			function deserialize() {
				var stored = $.cookie(settings.cookieId);
				if ( stored ) {
					var data = stored.split("");
					branches.each(function(i, e) {
						$(e).find(">ul")[ parseInt(data[i]) ? "show" : "hide" ]();
					});
				}
			}
			
			// add treeview class to activate styles
			this.addClass("treeview");
			
			// prepare branches and find all tree items with child lists
			var branches = this.find("li").prepareBranches(settings);
			
			switch(settings.persist) {
			case "cookie":
				var toggleCallback = settings.toggle;
				settings.toggle = function() {
					serialize();
					if (toggleCallback) {
						toggleCallback.apply(this, arguments);
					}
				};
				deserialize();
				break;
			case "location":
				var current = this.find("a").filter(function() { return this.href.toLowerCase() == location.href.toLowerCase(); });
				if ( current.length ) {
					current.addClass("selected").parents("ul, li").add( current.next() ).show();
				}
				break;
			}
			
			branches.applyClasses(settings, toggler);
				
			// if control option is set, create the treecontroller and show it
			if ( settings.control ) {
				treeController(this, settings.control);
				$(settings.control).show();
			}
			
			return this.bind("add", function(event, branches) {
				$(branches).prev()
					.removeClass(CLASSES.last)
					.removeClass(CLASSES.lastCollapsable)
					.removeClass(CLASSES.lastExpandable)
				.find(">.hitarea")
					.removeClass(CLASSES.lastCollapsableHitarea)
					.removeClass(CLASSES.lastExpandableHitarea);
				$(branches).find("li").andSelf().prepareBranches(settings).applyClasses(settings, toggler);
			});
		}
	});
	
	// classes used by the plugin
	// need to be styled via external stylesheet, see first example
	var CLASSES = $.fn.treeview.classes = {
		open: "open",
		closed: "closed",
		expandable: "expandable",
		expandableHitarea: "expandable-hitarea",
		lastExpandableHitarea: "lastExpandable-hitarea",
		collapsable: "collapsable",
		collapsableHitarea: "collapsable-hitarea",
		lastCollapsableHitarea: "lastCollapsable-hitarea",
		lastCollapsable: "lastCollapsable",
		lastExpandable: "lastExpandable",
		last: "last",
		hitarea: "hitarea"
	};
	
	// provide backwards compability
	$.fn.Treeview = $.fn.treeview;
	
})(jQuery);
        ]]>
        </script>

        <script>
          <![CDATA[
 /*!
 * jQuery StoryQ 0.3.0
 *
 * Copyright (c) 2010 todd@goneopen.com 
 * Dual licensed under the MIT (MIT-LICENSE.txt)
 * and GPL (GPL-LICENSE.txt) licenses.
 *
 * http://github.com/toddb/jquery.storyq
 */
 
 ;(function($) {
  
  $.storyq = {
		VERSION: "0.3.0",
		defaults: {
			success: null,
			url: null,
			data: null,
			summary: 'summary',
			tree: 'tree',
			minHeight: 100,
			minWidth: 600
		}
	};

	$.fn.extend({	  
	  storyq: function(settings) {   
      settings = $.extend({}, $.storyq.defaults, settings);   
      
      self = this.extend({

        xml2tree: function(xml){
           var tree = self.tree();
           
           self.heading(xml).appendTo($(tree));
           
           $('Project', xml).each( function() {
             var project = $('<li>')
           			.html('&lt;'+ $(this).attr('Name') + '>')
           			.append($('<span>').addClass('count').html(self.number_tests(this)))
           			.append($('<span>').addClass(self.state(this)).addClass('total').html(self.number_state(this)))
           			.wrapInner($('<span>').addClass(self.state(this)+'project').addClass('project result')
           			)
           			.appendTo(tree)
           			
              $('Namespace', this).each(function(){
                var ns = $('<ul>')
                  .append($('<li>')
                              .html($(this).attr('Name'))
                              .append($('<span>').addClass('count').html(self.number_tests(this)))
                         			.append($('<span>').addClass(self.state(this)).addClass('total').html(self.number_state(this)))
                              .wrapInner($('<span>').addClass(self.state(this)+'scenario').addClass('namespace result')))
                  .appendTo(project)
                  
                  $('Class', this).each(function(){
                     var cls = $('<ul>')
                       .append($('<li>')
                                .html($(this).attr('Name'))
                  			          .append($('<span>').addClass('count').html(self.number_tests(this)))
                             			.append($('<span>').addClass(self.state(this)).addClass('total').html(self.number_state(this)))
                                  .wrapInner($('<span>').addClass(self.state(this)).addClass('class result'))
                                  )
                       .appendTo($('li', ns))
                  
                       var story = $('<ul>').appendTo($('li', cls))
                       
                       $('Method', this).each(function(){
                           $('<li>')
                              .append($('<span>').html($(this).attr('Name')).addClass('name').attr('state', self.state(this)))
              			          .append($('<span>').html(self.test_result(this)).addClass('total'))
                              .append(self.story(this).hide())
                              .wrapInner($('<span>').addClass(self.state(this)).addClass('test result'))
                              .appendTo(story)
                          })
  
                   })
                  
              })
           })
            return tree
          },
          
          state: function(xml){
            if (self.failed(xml).length)  return "failed"
            if (self.pending(xml).length) return "pending"
            return "passed"
          },
          
          failed: function(xml) {
            return $('Result[Type=Failed]', xml)
          },

          pending: function(xml) {
            return $('Result[Type=Pending]', xml)
          },
          
          passed: function(xml) {
            return $('Result[Type=Passed]', xml)
          },
 
          notexecutable: function(xml) {
            return $('Result[Type=NotExecutable]', xml)
          },
                   
          number_state: function(xml){
            text = function(number, text){
              return " " + number + " Test" + self.pluralise(number) + " " + text
            }
            if (self.failed(xml).length)  return text(self.failed(xml).size(), 'failed')
            if (self.pending(xml).length) return text(self.pending(xml),       'pending')
            return text(self.passed(xml).length, 'passed')          
          },
          
          number_tests: function(xml){
            number = $('Method', xml).size();
            return ' (' + number +' Test' + self.pluralise(number) + ')'
           },

           test_result: function(xml){
             if (self.state(xml) == 'passed') return " Success"
             if (self.state(xml) == 'pending') return " Ignored: Pending"
             return " Failed: Exception: " + $('Exception:first', xml).attr('Message')
            },
 
           pluralise: function(number){
              return (number > 1) ? 's' : ''
           },
          
           summary: function(text){
              return $('#'+settings.summary).text(text)
           },

           story: function(xml){
                  var results = $('<code class="story">')
                  $('Result', xml).each(function(idx){
                      $('<story>')
                          .addClass('indent'+$(this).attr('IndentLevel'))
                          .addClass((idx == 4) ? 'scenario' : '')  // todo regex on scenario
                          .text($(this).attr('Prefix') + ' ' + $(this).attr('Text'))
                          .append($('<result>').text(self.story_result($(this))))
                          .appendTo(results)

                  })
                  self.exception(xml).appendTo(results)         
                  return results
           },
             
           story_result: function(xml){
               if ($(xml).attr('Type') == "NotExecutable") return
               if ($(xml).attr('Type') == "Passed") return "Passed"
               if ($(xml).attr('Type') == "Failed") return "Failed: " + $($('Exception', xml)).attr('Message')
               return "Pending !!"
           },
                  
           exception: function(xml){
               var exceptions = $('<exceptions>')
               $('Exception', xml).each(function(idx){
                 if (idx == 0) exceptions.append($('<h3>').text("Full Exception Details"))
                 $('<exception>')
                      .text('['+ (idx+1) + ']: ' + self.escapeHtml($(this).text()))
                      .appendTo(exceptions)
                })
                return exceptions
           },
         
           escapeHtml: function(html){
             return html.replace(/</g, "&lt;")
           },

           addPaneHandlers:  function() {
            return $('.result').click(function() { self.updateSummary($(this).parent()) })
           },
           
           summary: function(){
             return $('<div id="'+ settings.summary +'">')
           },
           
           emptySummary: function() {
             return $('#' + settings.summary).empty()
           },
         
           updateSummary: function(xml) {
             var summary = self.emptySummary()
             return $('code.story', xml).each(function() {
                      var test = $('<div class="summary">');
                      test.append($('<h3 class="ui-state-default ui-corner-all ui-helper-clearfix" style="padding:4px;">')
                                          .addClass(self.storyState(this))
                                          .text($('span.name', $(this).parent()).text() + ': ' + self.storyState(this)))
                      test.append($(this).clone().show())
                      summary.append(test)
                    });
           },

           tree: function(){
              return $('<ul id="'+ settings.tree +'">').addClass('ui-widget-content ui-resizable')
           },
           
           heading: function(xml) {
             return $('<h2 class="ui-widget-header ui-state-default ui-corner-all ui-helper-clearfix" style="padding:4px;">')
                           .addClass('results')
                           .text("Tests failed: " + self.failed(xml).length + 
                                 ", passed: " + self.passed(xml).length + 
                                 ", ignored: " + self.pending(xml).length)
           },
           
           storyState: function(xml) {
             return $(xml).siblings('span.name').attr('state');
           },
          
        })  

      return this.each(function(value, results){        
        if (settings.url) {
            $.ajax({
                type: 'GET',
                url: settings.url,
                data: settings.data,
                dataType: 'xml',
                success: function(xml) {
                  
                    self.addClass('storyq')

                    self.xml2tree(xml).appendTo($(results));
                    self.addPaneHandlers()

                    if (jQuery.isFunction(settings.load)) settings.load(xml);
                    if (jQuery.isFunction(settings.success)) { 
                        settings.success(xml)
                      } else {
                        $('#'+ settings.tree).treeview();
                      }

                    self.summary().insertAfter($('#'+ settings.tree))
                    
                    $('#'+ settings.tree).resizable({
                    			minHeight: settings.minHeight,
                    			minWidth: settings.minWidth
                    		});
                    
               }
            });
        }
        
      })
		}
	  
	})
})(jQuery);
         
          ]]>
        </script>
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
