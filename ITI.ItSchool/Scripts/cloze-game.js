var word = (function () {
    function word() {
    }
    return word;
})();

var ClozeGame = (function () {
    function ClozeGame() {
        this.errors = 0;
    }
    ClozeGame.prototype.getWords = function () {
    };

    ClozeGame.prototype.check = function (word, word2, word3) {
        if (word != "is")
            this.errors++;
        if (word2 != "are")
            this.errors++;
        if (word3 != "blue")
            this.errors++;

        return this.errors;
    };
    return ClozeGame;
})();

var clozeGame = new ClozeGame();
var button = document.createElement('button');
button.textContent = "Vérifier réponse !";
button.onclick = function () {
    var word = document.getElementById('sentence1').value;
    var word2 = document.getElementById('sentence2').value;
    var word3 = document.getElementById('sentence3').value;

    var errors = clozeGame.check(word, word2, word3);
    switch (errors) {
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
};
document.body.appendChild(button);
//# sourceMappingURL=cloze-game.js.map
