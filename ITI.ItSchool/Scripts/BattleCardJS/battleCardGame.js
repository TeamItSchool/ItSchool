var gameZone; // declare drawing zone
var scoreCounter = 0;

/* showValue on cards and work on svg document
 * @param doc the svg document
 * @param val the value in tag <text>
 * @parem color the color for "stroke" style
 */
function showValue(doc, val, color) {
	var text = doc.getElementsByTagName("text")[0];
	text.removeChild(text.firstChild);
	text.appendChild(doc.createTextNode(val));
	text.style.fill = color;
	//text.style.fontSize = "55px";
	var path = doc.getElementsByTagName("path")[0];
	path.style.fill = "#FFEBCD";
	path.style.stroke = color;
	var rect = doc.getElementsByTagName("rect")[0];
	rect.style.fill = "#20B2AA";
	doc.documentElement.onclick = playCard;
}

function playCard(ev) {
	var hand = document.getElementById("hand");
	var cards = hand.getElementsByClassName("card");
	var cardToFind = document.getElementById("cardToFind");
	$score = $('#score');
	
	for (var i = 0; i < cards.length; i++) {
		if (cards[i].contentDocument != ev.target.ownerDocument)
			continue; // it's not the right card

		//console.log(cards[i].id);
		//console.log(cards[i].contentDocument.getElementsByTagName("text")[0].textContent); // recup value of card clicked
		//console.log(cardToFind.contentDocument.getElementsByTagName("text")[0].textContent);
		//var card = Snap("#" + cards[i].id);
		
		if(cards[i].contentDocument.getElementsByTagName("text")[0].textContent == cardToFind.contentDocument.getElementsByTagName("text")[0].textContent)
		{
			scoreCounter ++;
			$score.text(scoreCounter);
		} else {
			if(scoreCounter != 0) 
			{
			scoreCounter --;
			$score.text(scoreCounter);
			}
		}
			
		var s = Snap("#" + cards[i].id);
		g = s.select("g");// select <g> tag in the svg document
		g.attr({transform : "translate(80, 20)", transform : "scale(0.2)"}); // on fait une translation et cela permet de rester dans le cadre
		gameZone.append(g); // append the card in drawing, in the gameZone
		cardRotationAnimation(g);
		var cardTranslateMatrix = new Snap.Matrix();

		// Sets up the matrix transforms
		cardTranslateMatrix.translate(700, 30);
		g.animate({
			transform : cardTranslateMatrix
		}, 1000, mina.easeinout, function () {
			g.select('#cardPath').animate({
				opacity : 0.1
			}, 150, mina.linear);
		});		
		movePlayedCard(cards[i], gameZone);		
	}
}

//Change the value of the cardToFind if the hand don't contain other card with same value as the cardToFind
function changeCardToFind() {
	var hand = document.getElementById("hand");
	var cards = hand.getElementsByClassName("card");
	var cardToFind = document.getElementById("cardToFind");
	var handValue = [];
	var handColor = [];
	var verifChangeCard = true;
	for (var i = 0; i < cards.length; i++) {
		handValue[i] = cards[i].contentDocument.getElementsByTagName("text")[0].textContent;
		handColor[i] = cards[i].contentDocument.getElementsByTagName("text")[0].style.fill;
		if(cards[i].contentDocument.getElementsByTagName("text")[0].textContent == cardToFind.contentDocument.getElementsByTagName("text")[0].textContent) {
			verifChangeCard = false;
		}
	}
	
	//console.log("verifChangeCard : " + verifChangeCard);
	
	//Verif if we have to change value of the cardToFind or not
	if(verifChangeCard){
	for (var i = 0; i < cards.length; i++) {
		/*console.log(cards[i].contentDocument);
		console.log(handValue[i]);
		console.log(handColor[i]);*/
		
		var rand = parseInt(Math.random() * cards.length);
		showValue(cardToFind.contentDocument, handValue[rand], handColor[rand]);
		cardToFind.removeChild(cardToFind.firstChild);
		cardToFind.appendChild(document.createTextNode(handValue[rand]));
		}
	}
}

function movePlayedCard(card, gameZone) {
	
	setTimeout(function () {
	gameZone.select('g').remove();
	}, 1500);
	
	cardId = card.id; // recup id to inject in function insertNewCard
	card.parentNode.removeChild(card);
	changeCardToFind();// before insert, verif if we have to change the cardToFind or if one remain in hand play
	insertNewCard(cardId); // insert new card in hand
	
}

function insertNewCard(cardId) {

	//var values = [1, 2, 3, 4, 5, 6, 7, 8, 9];
	var values = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z"];
	var deal = document.getElementById("deal");
	var card = deal.getElementsByClassName("card")[0];
	var hand = document.getElementById("hand");
	var cards = hand.getElementsByClassName("card");
	if (cards.length >= 6)
		return;
	var pos = parseInt(Math.random() * cards.length);
	var card = document.createElement("object");
	card.id = cardId; // on affecte l'id de la carte supprimer auparavant
	card.className = "card";
	var rand = parseInt(Math.random() * values.length);
	card.appendChild(document.createTextNode(values[rand]));
	card.type = "image/svg+xml";
	card.onload = function () {
		showValue(card.contentDocument, values[rand],
			(rand > values.length / 2) ? "#FF0000" : "#000000");
	};
	card.data = "/Images/redCard.svg";
	hand.insertBefore(card, cards[pos]);
	card.className = "card incoming";
	setTimeout(function () {
		card.className = "card";
	}, 800);

}

function cardRotationAnimation(g) {
	g.select('#cardPath').stop().animate({
		transform : 'r360,80,100'
	}, // Basic rotation around a point.
		1500, // Speed of the turning
		function () {
		g.select('#cardPath').attr({
			transform : 'rotate(0 80 100)'
		}); // Reset the position of the card.
		cardRotationAnimation(g); // Repeat this animation so it appears infinite.
	});
}


/* Create timer for the game*/
var clock;

try{
    $(document).ready(function () {
        //var clock;
      
        gameZone = Snap('#gameZone');
        var values = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        //var values = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 58, 99, 105, 999];
        //var values = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z"];
        var handValue = []; // array for card value of hand
        var handColor = []; // array for card color of hand


        //var cards = document.getElementById("hand").getElementsByClassName("card");

        var hand = document.getElementById("hand");
        var cards = hand.getElementsByClassName("card");
        var compteur = 0;

        //Initialize value of card to find, we use the handValue
        var cardToFind = document.getElementById("cardToFind");

        //mySVG = cards[2];
        //var svgDoc;
        //mySVG.addEventListener("load", function () {
        //    svgDoc = mySVG.contentDocument;
        //    alert("SVG contentDocument Loaded!");
        //    alert(svgDoc);
        //}, false);
        //alert(cards[2].contentDocument);

        //initialize hand card value
        for (var i = 0; i < cards.length; i++) {
            (function () {
                var mySVG = cards[i];
                mySVG.addEventListener("load", function () {
                    //alert("test");
                    var rand = parseInt(Math.random() * values.length);
                    var color = (rand > values.length / 2) ? "#FF0000" : "#000000"
                    showValue(mySVG.contentDocument, values[rand], color);
                    mySVG.removeChild(mySVG.firstChild);
                    mySVG.appendChild(document.createTextNode(values[rand]));
                    //We stock color value and handValue to later create cardToFind
                    handColor[compteur] = color;
                    handValue[compteur] = mySVG.contentDocument.getElementsByTagName("text")[0].textContent;
                    compteur++;
                    if (compteur == cards.length) {
                        //alert("maitenant");
                        //alert(cardToFind.contentDocument);
                        //for (var i = 0; i < cards.length; i++) {
                        //    alert(handColor[i]);
                        //    alert(handValue[i]);
                        //}
                        var rand = parseInt(Math.random() * cards.length);
                        showValue(cardToFind.contentDocument, handValue[rand], handColor[rand]);
                        cardToFind.removeChild(cardToFind.firstChild);
                        cardToFind.appendChild(document.createTextNode(handValue[rand]));
                    }

                }, false);

            }())

        }
        //Add the timer
        clock = $('#clock').FlipClock({
            clockFace: 'MinuteCounter',
            autoStart: false,
            callbacks: {
                stop: function () {
                    
                    setTimeout(function () {
                        $('#messageClock').html('La partie est termin&eacutee !')
                        //alert("Partie terminée : votre score est de " + $('#score').text() + " !");
                        //window.location.href = "http://localhost:18264/home#!/teacher/exercices";
                        for (var i = 0; i < cards.length; i++) {
                            cards[i].contentDocument.documentElement.onclick = null;
                        }
                    }, 1000);

                }
            }
        });

        clock.setCountdown(true);
        clock.setTime(60);
        clock.start();

    });
} catch (e) {
    alert(e.message);
}

