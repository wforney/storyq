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
