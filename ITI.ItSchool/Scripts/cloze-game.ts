class ClozeGame
{
    errors: number = 0;
    wordsToHide: string[];

    constructor() { }

    get totalErrors(): number {
        return this.errors;
    }

    getWords(): void {
        this.wordsToHide.push("is");
        this.wordsToHide.push("are");
        this.wordsToHide.push("blue");
    }

    check( wordsReceived: string[] ): void {
        var i: number = 0;
        for (i; i < this.wordsToHide.length; i++ ) {
            if( this.wordsToHide[i] != wordsReceived[i] ) {
                this.errors++;
            }
        }
    }
} 

var clozeGame = new ClozeGame();
var button = document.createElement('button');

button.textContent = "Vérifier réponse !";
button.onclick = function() {
    var word = (<HTMLInputElement>document.getElementById('sentence1')).value;
    var word2 = (<HTMLInputElement>document.getElementById('sentence2')).value;
    var word3 = (<HTMLInputElement>document.getElementById('sentence3')).value;

    var array = [word, word2, word3];
    clozeGame.check( array );

    var errors = clozeGame.totalErrors;

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