$.fn.extend({
    defaultValue: function(callback) {
        var nativePlaceholderSupport = (function(){
            var i = document.createElement('input');
            return ('placeholder' in i);
        })();
        // Default Value will halt here if the browser
        // has native support for the placeholder attribute
        if(nativePlaceholderSupport){
            return false;
        }
        return this.each(function(index, element) {
            // Executing Default Value twice on an element will lead to trouble
            if($(this).data('defaultValued')){
                return false;
            }
            var $input				=	$(this),
                defaultValue		=	$input.attr('placeholder');
            var	callbackArguments 	=	{'input':$input};
            // Mark as defaultvalued
            $input.data('defaultValued', true);
            // Create clone and switch
            var $clone = createClone();
            // Add clone to callback arguments
            callbackArguments.clone = $clone;
            $clone.insertAfter($input);
            var setState = function() {
                if( $input.val().length <= 0 ){
                    $clone.show();
                    $input.hide();
                } else {
                    $clone.hide();
                    $input.show().trigger('click');
                }
            };
            // Events for password fields
            $input.bind('blur', setState);
            // Create a input element clone
            function createClone(){
                var $el;
                if($input.context.nodeName.toLowerCase() == 'input') {
                    $el = $("<input />").attr({
                        'type'	: 'text'
                    });
                } else if($input.context.nodeName.toLowerCase() == 'textarea') {
                    $el = $("<textarea />");
                } else {
                    throw 'DefaultValue only works with input and textareas';
                }
                $el.attr({
                    'value'		: defaultValue,
                    'class'		: $input.attr('class')+' ui-input-ph',
                    'size'		: $input.attr('size'),
                    'style'		: $input.attr('style'),
                    'tabindex' 	: $input.attr('tabindex'),
                    'rows' 		: $input.attr('rows'),
                    'cols'		: $input.attr('cols'),
                    'name'		: 'defaultvalue-clone-' + (((1+Math.random())*0x10000)|0).toString(16).substring(1)
                });
                $el.focus(function(){
                    // Hide text clone and show real password field
                    $el.hide();
                    $input.show();
                    // Webkit and Moz need some extra time
                    // BTW $input.show(0,function(){$input.focus();}); doesn't work.
                    setTimeout(function () {
                        $input.focus();
                    }, 1);
                });
                return $el;
            }
            setState();
            if(callback){
                callback(callbackArguments);
            }
        });
    }
});
