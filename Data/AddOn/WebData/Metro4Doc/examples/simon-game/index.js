const cell = document.getElementsByClassName(`cell`); //all cells
const audio = document.getElementsByTagName(`audio`); //all audios
const start = document.getElementById('start'); 
const c_disp = document.getElementById('count-disp'); //count display div
const gameCount = document.getElementById('count'); // span count
c_disp.style.display = 'none';
var count = 1;
gameCount.innerHTML = count;
var cpattern= [];
var userPattern = [];
var index;

//generate 20 random computer moves and store in cpattern
for(let i = 1; i <=20; i++){
    cpattern.push(Math.floor(Math.random() * 4));
}

start.addEventListener('click',gameOn);

// cell[1].addEventListener('click',userPlay(cell[1],1), false);

function gameOn(){
        
        for(var x = 1; x<=20; x++)
        {
            index = cpattern[x];
            c_disp.style.display = 'block';
           // setTimeout(blink(cell[index]),1000);
            userClick();
        }
        
        }

function userClick(){
    if(document.getElementById('cell1').click){
        blink(document.getElementById('cell1'));
    }
}

        
//blink color
function blink(ele){
    ele.classList.add('blinkdiv');
    setTimeout(function() {
    ele.classList.remove('blinkdiv');
  }, 300);
}

//user move
function userPlay(ele,ind){
    blink(ele);
    audio[ind].play();
}