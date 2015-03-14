/*jQuery(function($) {'use strict',

	var form = $('.contact-form');
    form.submit(function () {'use strict',
		$this = $(this);
        $.post("sendemail.php", $(".contact-form").serialize(),function(result){
            if(result.type == 'success'){
                $this.prev().text(result.message).fadeIn().delay(3000).fadeOut();
            }
        });
        return false;
    });

});
*/
function ctOnSuccess() {
    $(".status").fadeIn().delay(3000).fadeOut();
    $("#FullName").val('');
    $("#Email").val('');
    $("#Subject").val('');
    $("textarea#message").val('');
    $(".btnSend").attr("disabled", "disabled");
}
function ctOnFailure() {
    $(".status").hide();
    $(".statusfail").fadeIn().delay(3000).fadeOut();
}
function ctOnBegin() {
    $(".btnSend").val('Đang gửi');
}
// Google Map Customization
jQuery(document).ready(function(){

	var map;

	map = new GMaps({
	    el: '#googleMap',
	    lat: 11.957162,
	    lng: 108.445131,
		scrollwheel:false,
		zoom: 14,
		zoomControl : false,
		panControl : false,
		streetViewControl : false,
		mapTypeControl: false,
		overviewMapControl: false,
		clickable: false
	});

	var image = '/content/images/logo.png';
	map.addMarker({
	    lat: 11.955660,
		lng: 108.442566,
		 // icon: image,
		animation: google.maps.Animation.DROP,
		verticalAlign: 'bottom',
		horizontalAlign: 'center',
		backgroundColor: '#ffffff',
	});

	var styles = [ 

	{
		"featureType": "road",
		"stylers": [
		{ "color": "" }
		]
	},{
		"featureType": "water",
		"stylers": [
		{ "color": "#A2DAF2" }
		]
	},{
		"featureType": "landscape",
		"stylers": [
		{ "color": "#ABCE83" }
		]
	},{
		"elementType": "labels.text.fill",
		"stylers": [
		{ "color": "#000000" }
		]
	},{
		"featureType": "poi",
		"stylers": [
		{ "color": "#2ECC71" }
		]
	},{
		"elementType": "labels.text",
		"stylers": [
		{ "saturation": 1 },
		{ "weight": 0.1 },
		{ "color": "#111111" }
		]
	}

	];

	map.addStyle({
		styledMapName:"Styled Map",
		styles: styles,
		mapTypeId: "map_style"  
	});

	map.setStyle("map_style");
	 
});