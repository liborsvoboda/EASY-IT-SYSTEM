$(document).ready(function() {

  var quote = 'Never trust the storyteller. Only trust the story. ';
  var author = "Sandman";
  $(".quote").html('<i class="fa fa-quote-left" aria-hidden="true"></i> '+quote+'<i class="fa fa-quote-right" aria-hidden="true"></i>');
  $(".author").html(' - '+author);
  var colors = ["#00FFFF", "#FFFF00", "#FFA500", "#00FF00"];
  var randColor,randindex;
  var quotes = ["Things need not have happened to be true. Tales and dreams are the shadow-truths that will endure when mere facts are dust and ashes, and forgot.", 
  "Destinations are often a surprise to the destined.","It's astonishing how much trouble one can get into, if one works at it.","It is a fool's prerogative to utter truths that no one else will speak.",
  "Little one, I would like to see anyone - prophet, king or god - persuade a thousand cats to do anything at the same time.",
  "You get life, and breath, a world to walk and a path through the world--and the free will to wander the world as you choose.",
  "I could never again be an angel.  Innocence, once lost, can never be regained.",
  "I am content to be what I am.  What more than that could any man desire?",
  "Reason.  It is no more reliable a tool than instinct, myth, or dream.  But it has the potential to be far more dangerous...",
  "I don't think Home's a place anymore.  I think it's a state of mind." ];
  var authors = ["Dream","Thessaly","Destruction","Dream","Orange Cat","Destruction","Lucifer","Joshua Norton I","Destruction","Barbie"];
 
 

  $("#new").on('click', function() {
    
      randColor = colors[Math.floor(Math.random() * colors.length)];
      randindex = Math.floor(Math.random() * quotes.length);
	  quote = quotes[randindex];
	  author = authors[randindex];
	  
      var begin = '<i class="fa fa-quote-left" aria-hidden="true"></i> ';

      var end = ' <i class="fa fa-quote-right" aria-hidden="true"></i>';
      $(".quote").html(begin + quotes[randindex] + end);
      $(".quote").css({
        'color': randColor
      });
     
      $(".author").html("- " + authors[randindex]);
      $(".author").css({
        'color': randColor
      });

      $(".quotes").css({
        'border': randColor
      });
	  
         $(".quotes").addClass("animated pulse").one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function() {
      $(this).removeClass('animated pulse');
    }); 	
    });

	$("#tweet").on('click', function() {
		var tweet = $("#link");
		var url = "https:twitter.com/intent/tweet?text=" + quote + ' - ' + author;
		tweet.attr('href', url);
    //window.open(url);
  });
  

  
});