var tiles = document.getElementsByClassName("tile");
var buttons = document.getElementsByClassName("button");
document.getElementById('won').style.display = 'none';
document.getElementById('lost').style.display = 'none';
document.getElementById('draw').style.display = 'none';
//empty tiles
for(let i =0; i<tiles.length; i++){
  tiles[i].innerHTML = '&nbsp';
}

var state = [0,0,0,0,0,0,0,0,0]; //empty tiles
var game = true;

var human = false;
var computer = true; //computer's pov

var humVal = -1;
var comVal = 1;

//default values
var humText = 'X'; 
var comText = 'O';

var winMatrix = [
  [0, 1, 2],
  [3, 4, 5],
  [6, 7, 8],
  [0, 3, 6],
  [1, 4, 7],
  [2, 5, 8],
  [0, 4, 8],
  [2, 4, 6]
]

function setText(textVal){
	humText = textVal.charAt(0);
	comText = textVal.charAt(1);
  if(comText == 'X') callAI();
} 

function reset(){
  for ( let x=0; x<9; x++){
    tiles[x].innerHTML = '&nbsp';
    state[x] = 0;
  }
  game = true;
  document.getElementById('won').style.display = 'none';
  document.getElementById('lost').style.display = 'none';
  document.getElementById('draw').style.display = 'none';
}

function claim(clicked){
  if(!game) return;
  //get index of tile thats clicked
  for(let x = 0; x<9; x++){
    if(tiles[x] == clicked && state[x] ==0){
      set(x,human);
      callAI();
    }
  }
}

function set(index, player){
  if(!game){
    return;
  }
  
  if(state[index] == 0){
    if(player == human){
      tiles[index].style.color = 'red';
	    tiles[index].innerHTML = humText;
      state[index] = humVal;
    }
    else if(player == computer){
      tiles[index].style.color = 'green';
      state[index] = comVal;
	    tiles[index].innerHTML = comText;
    }
    
    if(checkFull(state)) document.getElementById('draw').style.display = 'block';

    else if(checkWin(state,player)){
      
      if(player == 'human') document.getElementById('won').style.display = 'block';
      else document.getElementById('lost').style.display = 'block';
      
      game = false;
    }
    
  }

  
}

function checkWin(board, player){
  var value = player == human ? humVal : comVal; 
  //value is -1 if human, 1 if computer
  
  //check win combinations
  for(var x =0; x<8; x++){
    var win = true;
    for(var y =0; y<3; y++){
      if(board[winMatrix[x][y]] != value){
        win = false;
        break;
      }
    }
    if(win) return true;
  }
  return false;
}

function checkFull(board){
  for(let x =0; x<9; x++){
    if(board[x] == 0) return false;
  }
  return true;
}

function callAI(){
  aiTurn(state, 0, computer);
}

function aiTurn(board, depth, player){
   var value = player == human ? humVal : comVal;
  
  if(checkWin(board, !player)) return -10+depth; //human wins
  
  if(checkFull(board)) return 0; //draw
 
  var max = -Infinity;
  var index =0;
  
  //check moves tile by tile and calc scores for each move. 
  //choose the move with maximum score.
  for(let x =0 ; x<9; x++){

    if(board[x] == 0){
      var newBoard = board.slice(); //copy of board to simulate move
      newBoard[x] = value;
      
      //recursive play, simulating human and comp alternatively.
      //when opposite player wins, comp loses points, hence !player
      //if ai loses, points decreases for ai --> human wins
      var moveVal = -aiTurn(newBoard,(depth + 1), !player);
      
      if(moveVal > max){
        max = moveVal; //best move will have max score 
        index = x;
      }
    }
  }
  if (depth == 0) set(index,computer);
  return max;
}

