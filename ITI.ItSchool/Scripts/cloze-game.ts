class ClozeGame
{
    constructor() { }

    check( word: string ) {
        if (word == "180") {
            alert("Right!");
        } else {
            alert( "Wrong..." );
        }
    }
} 

var clozeGame = new ClozeGame();
var button = document.createElement('button');
button.textContent = "Vérifier réponse !";
button.onclick = function () {
    var word = (<HTMLInputElement>document.getElementById('word')).value;
    clozeGame.check( word );
}
document.body.appendChild(button);