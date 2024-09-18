var cell = document.getElementById("cell");
var result = 0;

//display area
function display(n){
    //if an expression was evaluated, clear the area for new operation, unless an operator was pressed, keep the result displayed
    if(result){
        if((n=='+') || (n=='-') || (n=='*') || (n=='/')){
             result = 0;
        }
        else{
            cc();
            result = 0;
        }
    }
     
     //prevent . from being clicked/displayed more than once
     let s = cell.value;
     if((n == '.') && (s.charAt((s.length)-1) == '.')){
                     cell.value = s;
     }
     else{
       cell.value += n; //display whatever series of numbers or operators are clicked.
     }
}

function calc(val){
    cell.value = eval(val);
    result = 1;
}

function cc(){
    cell.value = " ";
}

function back(){
    var s = cell.value;
    cell.value = s.substring(0,s.length-1); 
}