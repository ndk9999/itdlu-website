jQuery(function(){

    jQuery("ul.dropdown li").hover(function () {
    
        jQuery(this).addClass("hover");
        jQuery('ul:first', this).css('visibility', 'visible');
    
    }, function(){
    
        jQuery(this).removeClass("hover");
        jQuery('ul:first', this).css('visibility', 'hidden');
    
    });
    
   // jQuery("ul.dropdown li ul li:has(ul)").find("a:first").append(" &raquo; ");

});