class word {
    content: string;
}

class ClozeGame
{
    errors: number = 0;

    constructor() { }

    getWords(): Array<word> {

    }

    check(word: string, word2: string, word3: string): number {

        if (word != "is") this.errors++;
        if (word2 != "are") this.errors++;
        if (word3 != "blue") this.errors++;

        return this.errors;
    }
} 

var clozeGame = new ClozeGame();
var button = document.createElement('button');
button.textContent = "Vérifier réponse !";
button.onclick = function() {
    var word = (<HTMLInputElement>document.getElementById('sentence1')).value;
    var word2 = (<HTMLInputElement>document.getElementById('sentence2')).value;
    var word3 = (<HTMLInputElement>document.getElementById('sentence3')).value;


    var errors = clozeGame.check(word, word2, word3);
    switch(errors) {
        case 0:
            alert("Bravo ! Tu n'as fait aucune faute !");
            break;
        case 1:
            alert("Très bien ! Tu as fait 1 faute.");
            break;
        default:
            alert("Dommage... Tu as fait plus de 2 fautes. Réessaie encore !");
            break;
    }
}
document.body.appendChild(button);