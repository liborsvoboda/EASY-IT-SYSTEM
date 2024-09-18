//search box
$(function(){
  $('.input-search').focus(function(){
    $(this).parent().addClass('expanded');
  });
   
  $('.input-search').blur(function(){ 
    	$(this).parent().removeClass('expanded');
  });

  //autocomplete suggestions
  
/* Autocomplete not working

  $("#search").autocomplete({
	source: function(request, response) {
		$.ajax({
			url: "http://en.wikipedia.org/w/api.php",
			dataType: "jsonp",
      data: {
                    'action': "opensearch",
                    'format': "jsonp",
                    'search': request.term
                },
			success: function(data) {
				response(data[1]);
			}
		});
	}
});
*/

  //return search results when enter is pressed
  $("#search").keypress(function(e){
  if(e.which==13){
    
  e.preventDefault(); // stop form submission
    searchFunc();
  }
  });
  
  $("#search-btn").click(searchFunc);
    
    function searchFunc(){
  var search = $("#search").val()
  search = encodeURIComponent(search);
  var url = "https://en.wikipedia.org/w/api.php?format=json&action=opensearch&search="+search+"&suggest=true&origin=*&callback=?";
     
    $.ajax({
      type:"GET",
      url:url,
      async:false,
      dataType:'json',
      success: function(data){
        $("#results").html('');
        if(data[1].length == 0){
          $("#results").prepend("<li>Sorry, no results found.</li>");
        }
       for(var i=0; i<data[1].length;i++){
        $("#results").prepend("<li><a target='_blank' href="+data[3][i]+">"+data[1][i]+"</a><p>"+data[2][i]+"</p></li>");
       }
      },
      error: function(msg){
      $("#results").text("Error");
     }
});
  }
 
});


window.onscroll = function() {scrollFunction()};

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("myBtn").style.display = "block";
    } else {
        document.getElementById("myBtn").style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Chrome, Safari and Opera 
    document.documentElement.scrollTop = 0; // For IE and Firefox
}