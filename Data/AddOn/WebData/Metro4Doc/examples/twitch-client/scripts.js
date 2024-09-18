var streamers = ["FreeCodeCamp" , "ESL_SC2", "OgamingSC2", "cretetion",  "storbeck", "habathcx", "RobotCaleb", "noobs2ninjas","brunofin"];

$(document).ready(function(){
  var i;
  for(i= 0;i<(streamers.length);i++){
    var erruser = streamers[i];
    var url = 'https://wind-bow.gomix.me/twitch-api/channels/'+streamers[i];
	$.ajax({
		type: 'GET',
		dataType: "jsonp",
		url: url,
		success: function(dataI){
			var mystream = dataI.url;
			var myname = dataI.display_name;
			var mylogo = dataI.logo;
			if(!dataI.error){
			$.getJSON('https://wind-bow.gomix.me/twitch-api/streams/'+dataI.name).done(function(data2){
				
				if(data2.stream === null){ //offline
					$('#result').append('<div class="media" ><div class="media-left"><img class = "media-object" src = "'+mylogo+'" /></div><div class="media-body"><h4 class="media-heading">'+myname + '</h4><a href = "' + mystream +'" target = "_blank">Offline</a><br>Not currently streaming </div></div>');
				}
				else{ //online
					$('#result').append('<div class="media" ><div class="media-left"><img class = "media-object" src = "'+mylogo+'" /></div><div class="media-body"><h4 class="media-heading">'+myname + '</h4><a href = "' + mystream +'" target = "_blank">Online</a><br>'+ data2["stream"]["game"] + '<br>'+ data2["stream"]["channel"]["status"] +'</div></div>');
				}
			});
    }
    if(dataI.error){ //not a user
            $('#result').append('<div class="media"><div class="media-left"><img class = "media-object" src = "image.png" /></div><div class="media-body"><div class="media-body">'+dataI.message+'</div></div>');
      }
			
		}
	});
    } 
   
   
});




