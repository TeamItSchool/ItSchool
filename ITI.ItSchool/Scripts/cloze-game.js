var ClozeGame = (function () {
    function ClozeGame() {
    }
    ClozeGame.prototype.check = function (word) {
        if (word == "180") {
            alert("Right!");
        } else {
            alert("Wrong...");
        }
    };
    return ClozeGame;
})();

var clozeGame = new ClozeGame();
var button = document.createElement('button');
button.textContent = "Vérifier réponse !";
button.onclick = function () {
    var word = document.getElementById('word').value;
    clozeGame.check(word);
};
document.body.appendChild(button);
//# sourceMappingURL=cloze-game.js.map
