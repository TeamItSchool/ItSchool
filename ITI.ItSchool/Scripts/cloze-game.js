var ClozeGame = (function () {
    function ClozeGame() {
        this.errors = 0;
        this.wordsToHide = new Array();
    }
    Object.defineProperty(ClozeGame.prototype, "totalErrors", {
        get: function () {
            return this.errors;
        },
        enumerable: true,
        configurable: true
    });

    ClozeGame.prototype.getWords = function () {
        this.wordsToHide.push("is");
        this.wordsToHide.push("are");
        this.wordsToHide.push("blue");
    };

    ClozeGame.prototype.check = function (wordsReceived) {
        this.getWords();
        var i = 0;
        for (i; i < this.wordsToHide.length; i++) {
            if (this.wordsToHide[i] != wordsReceived[i]) {
                this.errors++;
            }
        }
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

    var array = [word, word2, word3];
    clozeGame.check(array);

    var errors = clozeGame.totalErrors;

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
