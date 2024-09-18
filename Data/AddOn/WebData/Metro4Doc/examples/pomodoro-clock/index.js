var timerCount = document.getElementById("timer-count");
var timerCountVal = parseInt(timerCount.textContent); 

var brkCount = document.getElementById("break-count");
var brkCountVal = parseInt(brkCount.textContent); 

var tPlus = document.getElementById("timer-plus"); 
var tMinus = document.getElementById("timer-minus"); 

var bPlus = document.getElementById("break-plus"); 
var bMinus = document.getElementById("break-minus"); 

var countDown = document.getElementById("countdown");
countDown.innerHTML = timerCountVal;

var btime = document.getElementById("breakTime");
btime.innerHTML = brkCountVal;

 var starter;

function Tincrease(val){
    timerCountVal = ++timerCountVal;
    if(timerCountVal >= 25){
        timerCount.innerHTML = timerCountVal;
        countDown.innerHTML = timerCountVal;
    }
}

function Tdecrease(val){
    timerCountVal = --timerCountVal;
    if(timerCountVal>=25){
    timerCount.innerHTML = timerCountVal;
    countDown.innerHTML = timerCountVal;
     }
}

function Bincrease(val){
    brkCountVal = ++brkCountVal;
     if(brkCountVal>=5){
    brkCount.innerHTML = brkCountVal;
    btime.innerHTML = brkCountVal;
     }
}

function Bdecrease(val){
    brkCountVal = --brkCountVal;
     if(brkCountVal>=5){
      brkCount.innerHTML = brkCountVal;
      btime.innerHTML = brkCountVal;
     }
}

function Tstart(){
	counter();
    var starter = setInterval(counter,60000);
	function counter(){
		var c = parseInt(countDown.textContent);
		var minRem = 59;
		c--;
		var min = setInterval(minCounter, 1000);
		function minCounter(){
			if((minRem>=0)&&(minRem<10)){
                countDown.textContent = c + ' : 0' + minRem;
            }
            else{
			countDown.textContent = c + ' : ' + minRem;
            }
			minRem--;
            
			if(minRem < 0){
				clearInterval(min);
			}
		}
	
    if(c==0){
      clearInterval(starter);
      (document.getElementById("audio")).play();
      Bstart();
    }
	document.getElementById("stop").addEventListener('click', function(){
		clearInterval(starter);
		clearInterval(min);
	});
 }  
}

function Bstart(){
    counter();
	document.getElementById("brk-counter").style.display = "block";
    var starter = setInterval(counter,60000);
    function counter(){
    var c = parseInt(btime.textContent);
	var minRem = 59;
    c--;
	var min = setInterval(minCounter, 1000);
		function minCounter(){
			if((minRem>=0)&&(minRem<10)){
                btime.textContent = c + ' : 0' + minRem;
            }
            else{
			btime.textContent = c + ' : ' + minRem;
            }
			minRem--;
			if(minRem < 0){
				clearInterval(min);
			}
		}
    if(c<0){
      clearInterval(starter);
    }
  }
}

function reset(){
    timerCount.innerHTML = 25;
    brkCount.innerHTML = 5; 
    countDown.innerHTML = 25;
    btime.innerHTML = 5;
}

