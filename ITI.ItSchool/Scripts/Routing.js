angular.module('TheApp', ['ngRoute', 'ngMaterial', 'ngGrid']) // ['ngRoute'] is required for the routing and
    //['ngMaterial'] for the material design components
.run(function ($log, $rootScope) {
    $log.debug("startApp running");
})
.config(function ($routeProvider, $locationProvider) {
    //here we will write code for implement routing 
    $routeProvider
    .when('/', { // This is for reditect to another route
        redirectTo: function () {
            return '/home';
        }
    })
    .when('/home', {
        templateUrl: '/Templates/Home.html'/*,
        controller: 'HomeController'*/
    })
    .when('/test_microphone', {
        templateUrl: '/Templates/voiceRecorder.html'/*,
        controller:'MicrophoneController'*/
    })
    .when('/teacher', {
        templateUrl: '/Templates/TeacherHomePage.html'/*,
        controller: 'TeacherHomeController'*/
    })
    .when('/teacher/login', {
        templateUrl: '/Templates/TeacherLoginPage.html'/*,
        controller: 'TeacherLoginController'*/
    })
    .when('/teacher/registration', {
        templateUrl: '/Templates/TeacherRegistrationPage.html'/*,
        controller: 'TeacherRegistrationController'*/
    })
    .when('/teacher/lobby', {
        templateUrl: '/Templates/TeacherLobbyPage.html'/*,
        controller: 'TeacherLobbyController'*/
    })
    .when('/teacher/exercices', {
        templateUrl: '/Templates/TeacherSelectExercicesPage.html'/*,
        controller: 'TeacherSelectExercicesController'*/
    })
    .when('/teacher/exercices/matter', {
        templateUrl: '/Templates/TeacherSelectMatterPage.html'/*,
        controller: 'TeacherSelectMatterController'*/
    })
    .when('/teacher/exercices/cloze_exercise', {
        templateUrl: '/Templates/TeacherCustomizeClozeExercisePage.html'
    })
    .when('/teacher/exercices/matter/drag_drop_maths', {
        templateUrl: '/Templates/TeacherCustomizeDragAndDropMathsPage.html'/*,
        controller: 'TeacherDragAndDropMathsController'*/
    })
    .when('/teacher/exercices/matter/drag_drop_conjugaiton', {
        templateUrl: '/Templates/TeacherCustomizeDragAndDropConjugaitonPage.html'/*,
        controller: 'TeacherDragAndDropConjugaitonController'*/
    })
    .when('/teacher/exercices/matter/drag_drop_english', {
        templateUrl: '/Templates/TeacherCustomizeDragAndDropEnglishPage.html'/*,
        controller: 'TeacherDragAndDropEnglishController'*/
    })
    .when('/teacher/exercices/dictation', {
        templateUrl: '/Templates/TeacherCustomizeDictationPage.html'/*,
        controller: 'TeacherDictationController'*/
    })
    .when('/teacher/exercices/battleCard', {
        templateUrl: '/Templates/TeacherDescriptionBattleCardPage.html'/*,
        controller: 'TeacherDescriptionBattleCardController'*/
    })
    .when('/teacher/exercices/battleCard/customize', {
        templateUrl: '/Templates/TeacherCustomizeBattleCardPage.html'
        //controller: 'TeacherCustomizeBattleCardController'
    })
    .when('/kid', {
        templateUrl: '/Templates/KidHomePage.html'/*,
        controller: 'KidHomeController'*/
    })
    .when('/kid/login', {
        templateUrl: '/Templates/KidLoginPage.html'/*,
        controller: 'KidLoginController'*/
    })
    .when('/kid/registration', {
        templateUrl: '/Templates/KidRegistrationPage.html'/*,
        controller: 'KidRegistrationController'*/
    })
    .when('/kid/lobby', {
        templateUrl: '/Templates/KidLobbyPage.html'/*,
        controller: 'KidLobbyController'*/
    })
    .when('/kid/exercices', {
        templateUrl: '/Templates/KidSelectExercicesPage.html'/*,
        controller: 'KidSelectExercicesController'*/
    })
    .when('/kid/exercices/dictation', {
        templateUrl: '/Templates/KidPlayDictationPage.html'/*,
        controller: 'KidPlayDictationController'*/
    })
    .when('/kid/exercices/battleCard', {
        templateUrl: '/Templates/KidDescriptionBattleCardPage.html'/*,
        controller: 'KidDescriptionBattleCardController'*/
    })
    .when('/kid/exercices/battleCard/play', {
        templateUrl: '/Templates/KidPlayBattleCardPage.html'/*,
        controller: 'KidPlayBattleCardController'*/
    })
    .otherwise({   // This is when any route not matched
    templateUrl: '/Templates/Error.html',
    controller: 'ErrorController'
    })
    $locationProvider.html5Mode(false).hashPrefix('!'); // This is for Hashbang Mode
})
.controller('MicrophoneController', function ($scope) {
    function __log(e, data) {
        log.innerHTML += "\n" + e + " " + (data || '');
    }

    var audio_context;
    var recorder;

    function startUserMedia(stream) {
        var input = audio_context.createMediaStreamSource(stream);
        __log('Media stream created.');
        __log("input sample rate " + input.context.sampleRate);

        input.connect(audio_context.destination);
        __log('Input connected to audio context destination.');

        recorder = new Recorder(input);
        __log('Recorder initialised.');
    }

    $scope.startRecording = function () {
        var button = document.getElementById("StartButton");
        recorder && recorder.record();
        button.disabled = true;
        button.nextElementSibling.disabled = false;
        __log('Recording...');
    }

    $scope.stopRecording = function () {
        var button = document.getElementById("StopButton")
        recorder && recorder.stop();
        button.disabled = true;
        button.previousElementSibling.disabled = false;
        __log('Stopped recording.');

        // create WAV download link using audio data blob
        createDownloadLink();

        recorder.clear();
    }

    function createDownloadLink() {
        recorder && recorder.exportWAV(function (blob) {
            /*var url = URL.createObjectURL(blob);
            var li = document.createElement('li');
            var au = document.createElement('audio');
            var hf = document.createElement('a');

            au.controls = true;
            au.src = url;
            hf.href = url;
            hf.download = new Date().toISOString() + '.wav';
            hf.innerHTML = hf.download;
            li.appendChild(au);
            li.appendChild(hf);
            recordingslist.appendChild(li);*/

            /*objectURL = window.URL.createObjectURL(blob);
            window.location.href = objectURL;*/
            var 
            objectURL = window.URL.createObjectURL(blob);
            var url = objectURL;
            window.open(url, 'Download');
        });
    }

    window.onload = function init() {
        try {
            // webkit shim
            window.AudioContext = window.AudioContext || window.webkitAudioContext;
            navigator.getUserMedia = (navigator.getUserMedia ||
                            navigator.webkitGetUserMedia ||
                            navigator.mozGetUserMedia ||
                            navigator.msGetUserMedia);
            window.URL = window.URL || window.webkitURL;

            audio_context = new AudioContext;
            __log('Audio context set up.');
            __log('navigator.getUserMedia ' + (navigator.getUserMedia ? 'available.' : 'not present!'));
        } catch (e) {
            alert('No web audio support in this browser!');
        }
        /*navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
            __log('No live audio input: ' + e);
        });*/
    }

    $scope.enableMicrophone = function () {
        var button = document.getElementById("EnableMicrophone");
        button.disabled = true
        button.nextElementSibling.disabled = false;
        var buttonStart = document.getElementById("StartButton")
        buttonStart.disabled = false;
        navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
            __log('No live audio input: ' + e);
        });
    }
    function disableMicrophone() {
        location.reload();
    }
    (function (window) {

        var WORKER_PATH = '/Scripts/recorderWorker.js';
        var encoderWorker = new Worker('/Scripts/mp3Worker.js');

        var Recorder = function (source, cfg) {
            var config = cfg || {};
            var bufferLen = config.bufferLen || 4096;
            this.context = source.context;
            this.node = (this.context.createScriptProcessor ||
                         this.context.createJavaScriptNode).call(this.context,
                                                                 bufferLen, 2, 2);
            var worker = new Worker(config.workerPath || WORKER_PATH);
            worker.postMessage({
                command: 'init',
                config: {
                    sampleRate: this.context.sampleRate
                }
            });
            var recording = false,
              currCallback;

            this.node.onaudioprocess = function (e) {
                if (!recording) return;
                worker.postMessage({
                    command: 'record',
                    buffer: [
                      e.inputBuffer.getChannelData(0),
                      //e.inputBuffer.getChannelData(1)
                    ]
                });
            }

            this.configure = function (cfg) {
                for (var prop in cfg) {
                    if (cfg.hasOwnProperty(prop)) {
                        config[prop] = cfg[prop];
                    }
                }
            }

            this.record = function () {
                recording = true;
            }

            this.stop = function () {
                recording = false;
            }

            this.clear = function () {
                worker.postMessage({ command: 'clear' });
            }

            this.getBuffer = function (cb) {
                currCallback = cb || config.callback;
                worker.postMessage({ command: 'getBuffer' })
            }

            this.exportWAV = function (cb, type) {
                currCallback = cb || config.callback;
                type = type || config.type || 'audio/wav';
                if (!currCallback) throw new Error('Callback not set');
                worker.postMessage({
                    command: 'exportWAV',
                    type: type
                });
            }

            //Mp3 conversion
            worker.onmessage = function (e) {
                var blob = e.data;
                //console.log("the blob " +  blob + " " + blob.size + " " + blob.type);

                var arrayBuffer;
                var fileReader = new FileReader();

                fileReader.onload = function () {
                    arrayBuffer = this.result;
                    var buffer = new Uint8Array(arrayBuffer),
                    data = parseWav(buffer);

                    console.log(data);
                    console.log("Converting to Mp3");
                    log.innerHTML += "\n" + "Converting to Mp3";

                    encoderWorker.postMessage({
                        cmd: 'init', config: {
                            mode: 3,
                            channels: 1,
                            samplerate: data.sampleRate,
                            bitrate: data.bitsPerSample
                        }
                    });

                    encoderWorker.postMessage({ cmd: 'encode', buf: Uint8ArrayToFloat32Array(data.samples) });
                    encoderWorker.postMessage({ cmd: 'finish' });
                    encoderWorker.onmessage = function (e) {
                        if (e.data.cmd == 'data') {

                            console.log("Done converting to Mp3");
                            log.innerHTML += "\n" + "Done converting to Mp3";

                            /*var audio = new Audio();
                            audio.src = 'data:audio/mp3;base64,'+encode64(e.data.buf);
                            audio.play();*/

                            //console.log ("The Mp3 data " + e.data.buf);

                            var mp3Blob = new Blob([new Uint8Array(e.data.buf)], { type: 'audio/mp3' });
                            uploadAudio(mp3Blob);

                            var url = 'data:audio/mp3;base64,' + encode64(e.data.buf);
                            var li = document.createElement('li');
                            var au = document.createElement('audio');
                            var hf = document.createElement('a');

                            au.controls = true;
                            au.src = url;
                            hf.href = url;
                            hf.download = 'audio_recording_' + new Date().getTime() + '.mp3';
                            hf.innerHTML = hf.download;
                            li.appendChild(au);
                            li.appendChild(hf);
                            recordingslist.appendChild(li);

                        }
                    };
                };

                fileReader.readAsArrayBuffer(blob);

                currCallback(blob);
            }


            function encode64(buffer) {
                var binary = '',
                    bytes = new Uint8Array(buffer),
                    len = bytes.byteLength;

                for (var i = 0; i < len; i++) {
                    binary += String.fromCharCode(bytes[i]);
                }
                return window.btoa(binary);
            }

            function parseWav(wav) {
                function readInt(i, bytes) {
                    var ret = 0,
                        shft = 0;

                    while (bytes) {
                        ret += wav[i] << shft;
                        shft += 8;
                        i++;
                        bytes--;
                    }
                    return ret;
                }
                if (readInt(20, 2) != 1) throw 'Invalid compression code, not PCM';
                if (readInt(22, 2) != 1) throw 'Invalid number of channels, not 1';
                return {
                    sampleRate: readInt(24, 4),
                    bitsPerSample: readInt(34, 2),
                    samples: wav.subarray(44)
                };
            }

            function Uint8ArrayToFloat32Array(u8a) {
                var f32Buffer = new Float32Array(u8a.length);
                for (var i = 0; i < u8a.length; i++) {
                    var value = u8a[i << 1] + (u8a[(i << 1) + 1] << 8);
                    if (value >= 0x8000) value |= ~0x7FFF;
                    f32Buffer[i] = value / 0x8000;
                }
                return f32Buffer;
            }

            function uploadAudio(mp3Data) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var fd = new FormData();
                    var mp3Name = encodeURIComponent('audio_recording_' + new Date().getTime() + '.mp3');
                    console.log("mp3name = " + mp3Name);
                    fd.append('fname', mp3Name);
                    fd.append('data', event.target.result);
                    $.ajax({
                        type: 'POST',
                        url: 'upload.php',
                        data: fd,
                        processData: false,
                        contentType: false
                    }).done(function (data) {
                        //console.log(data);
                        log.innerHTML += "\n" + data;
                    });
                };
                reader.readAsDataURL(mp3Data);
            }

            source.connect(this.node);
            this.node.connect(this.context.destination);    //this should not be necessary
        };

        /*Recorder.forceDownload = function(blob, filename){
          console.log("Force download");
          var url = (window.URL || window.webkitURL).createObjectURL(blob);
          var link = window.document.createElement('a');
          link.href = url;
          link.download = filename || 'output.wav';
          var click = document.createEvent("Event");
          click.initEvent("click", true, true);
          link.dispatchEvent(click);
        }*/

        window.Recorder = Recorder;

    })(window);

})
.controller('HomeController', function ($scope) {
    $scope.Message = "Bienvenue";
})
.controller('KidLoginController', function ($scope, LoginService) {

    sessionStorage.removeItem("objet");

    $scope.Message = "Embarque dans l'aventure It'School :)";
    $scope.IsLogedIn = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.IsTeacher = false;
    $scope.ButtonMessage = "Connexion";

    $scope.LoginData = {
        Username: '',
        Password: ''
    };
    //Check if Form is valid or not // here LoginForm is our form Name
    $scope.$watch('LoginForm.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.Login = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            $scope.ButtonMessage = "Connexion en cours..";
            LoginService.GetUser($scope.LoginData).then(function (d) {
                if (d.data.Username == null && d.data.Nickname == null) {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Oops, tu as entré un mauvais pseudo ou il n'existe pas. Réessaie ou inscris-toi si tu n'es pas inscrit.");
                } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                    $scope.LoginData.Password = "";
                    alert("Oops, tu as entré un mauvais mot de passe. Réessaye.");
                }
                else if (d.data.Nickname != null && d.data.Group.Name == "Élèves" && d.data.Password == $scope.LoginData.Password) {
                    var monobjet_json = JSON.stringify(d);
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);

                    $scope.IsLogedIn = true;
                    $scope.Message = "Bienvenue " + d.data.FirstName;
                } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs") {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Attention, vous vous trouvez actuellement dans l'espace des élèves.\n Veuillez cliquez sur le bouton 'Espace Professeur'.");
                    $scope.Message = "Rejoins l'espace 'Enfant'";
                    $scope.IsTeacher = true;
                }
                else {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Aïe.. Quelque chose s'est mal passé. Vois avec tes parents ou ton professeur.")
                }

                console.log(d.data);
            })
        }
    };
})
.controller("KidRegistrationController", function ($scope, RegistrationService, LoginService) {
    $scope.submitText = "Inscription";
    $scope.IsRegistered = false;
    $scope.submitted = false;
    $scope.Message = "Inscris-toi sur It'School ! :)";
    $scope.message = "";
    $scope.IsFormValid = false;

    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: '',
        Class: {
            Name: ''
        },
        Group: {
            Name: 'Élèves'
        }
    };

    $scope.Classes = null;

    RegistrationService.GetClasses().then(function (d) {
        $scope.Classes = d.data;
        //alert($scope.Grades[0].Name);
    }, function (error) {
        alert('Error on Classes 151');
    });

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Inscription") {
            $scope.submitted = true;
            $scope.message = "";

            if ($scope.IsFormValid) {
                $scope.submitText = "Patience..";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Le compte a bien été créé.') {
                        $scope.IsRegistered = true;
                        $scope.Message = "Embarque dans l'aventure It'School !";
                        $scope.IsLogedIn = false;
                        $scope.Submitted = false;
                        $scope.IsFormValid = false;
                        $scope.IsTeacher = false;

                        $scope.LoginData = {
                            Username: '',
                            Password: ''
                        };
                        //Check if Form is valid or not // here f1 is our form Name
                        $scope.$watch('LoginForm.$valid', function (newVal) {
                            $scope.IsFormValid = newVal;
                        });
                        $scope.Login = function () {
                            $scope.Submitted = true;
                            if ($scope.IsFormValid) {
                                $scope.ButtonMessage = "Connexion en cours..";
                                LoginService.GetUser($scope.LoginData).then(function (d) {
                                    if (d.data.Username == null && d.data.Nickname == null) {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as entré un mauvais pseudo ou il n'existe pas. Réessaie ou inscris-toi si tu n'es pas inscrit.");
                                    } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as entré un mauvais mot de passe. Réessaye.");
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves" && d.data.Password == $scope.LoginData.Password) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Bienvenue " + d.data.FirstName + " " + d.data.Group.Name;
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs") {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Attention, vous vous trouvez actuellement dans l'espace des élèves.\n Veuillez cliquez sur le bouton 'Espace Professeur'.");
                                        $scope.Message = "Rejoins l'espace 'Enfant'";
                                        $scope.IsTeacher = true;
                                    }
                                    else {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu as du rentrer un mauvais pseudo ou un mauvais mot de passe. Réessaye de te connecter.")
                                    }

                                    console.log(d.data.FirstName + " " + d.data.LastName);
                                })
                            }
                        };
                        //Have to clear form here
                        //ClearForm();
                    }
                    $scope.submitText = "Inscription";
                });
            }
            else {
                $scope.message = 'Il faut bien tout remplir :)';
            }
        }
    }
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})
.controller('KidLobbyController', function ($scope, LoginService) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName);
    $scope.Message = "Bonjour " + monobjet.data.FirstName;

})
.controller('KidSelectExercicesController', function ($scope) {
    $scope.Message = 'A quoi veux-tu jouer ?';
})
.controller('KidPlayDictationController', function ($scope, CheckDictationText) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de la dictée");

    $scope.Message = 'Sélectionne un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Valider";

    $scope.Game = {
        //A REMPLIR
        Data: '',
        Level: {
            Name: 'Test'
        },
        ExerciseType: {
            Name: 'Dictation'
        }
    };

    //Check if Form is valid or not // here DictText is our form Name
    $scope.$watch('DictText.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveText = function () {
        if ($scope.IsFormValid) {
            $scope.Button = "Validtion en cours..."
            $scope.Game.Data.trim();
            $scope.Game.Data = monobjet.data.Nickname + "/" + $scope.Game.Data;
            SaveDictationText.GetText($scope.Game).then(function (d) {
                $scope.Button = "Dictée sauvegardée";
            })
        }
    };

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau facile)";
        $scope.Game.Level.Name = "Easy";
    }
    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau moyen)";
        $scope.Game.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau difficile)";
        $scope.Game.Level.Name = "Hard";
    }
})
.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})
.controller('TeacherHomeController', function ($scope) {
    $scope.Message = 'Page "Professeurs"';
})
.controller('TeacherLobbyController', function ($scope, LoginService) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName);
    $scope.Message = "Bonjour " + monobjet.data.FirstName + " " + monobjet.data.LastName;

})
.controller('TeacherLoginController', function ($scope, LoginService) {

    sessionStorage.removeItem("objet");

    $scope.Message = "Entrez vos identifiants pour vous connecter.";
    $scope.IsLogedIn = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.IsKid = false;
    $scope.ButtonMessage = "Connexion";

    $scope.LoginData = {
        Username: '',
        Password: ''
    };
    //Check if Form is valid or not // here LoginForm is our form Name
    $scope.$watch('LoginForm.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.Login = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            $scope.ButtonMessage = "Connexion en cours..";
            LoginService.GetUser($scope.LoginData).then(function (d) {
                if (d.data.Username == null && d.data.Nickname == null) {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Vous avez entré un mauvais pseudo ou celui-ci n'existe pas. Veuillez réessayer ou vous inscrire si ce n'est pas le déjà le cas.");
                } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                    $scope.LoginData.Password = "";
                    alert("Vous avez dû entrer un mauvais mot de passe. Veuillez réessayer.");
                } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs" && d.data.Password == $scope.LoginData.Password) {
                    var monobjet_json = JSON.stringify(d);
                    sessionStorage.setItem("objet", monobjet_json);

                    var monobjet_json = sessionStorage.getItem("objet");
                    var monobjet = JSON.parse(monobjet_json);
                    // Affichage dans la console
                    console.log(monobjet.data.FirstName);

                    $scope.IsLogedIn = true;
                    $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves") {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Oops, tu es dans l'espace des professeurs. Vas dans l'espace des élèves pour te connecter.");
                    $scope.Message = "Rejoins l'espace 'Enfant'";
                    $scope.IsKid = true;
                }
                else {
                    $scope.LoginData.Username = "";
                    $scope.LoginData.Password = "";
                    alert("Une erreur s'est produite. Veuillez contacter le service technique.")
                }

                console.log(d.data.FirstName + " " + d.data.LastName);
            })
        }
    };
})
.controller("TeacherClozeExerciseController", function ($scope, ExerciseDatas) {

    var sessions_elements_json = sessionStorage.getItem("objet");
    var sessions_elements = JSON.parse(sessions_elements_json);

    $scope.Message = 'Que voulez-vous faire ?';
    $scope.NewExerciseSelected = false;
    $scope.SetExistingExerciseSelected = false;

    $scope.new_exercise = function () {
        $scope.NewExerciseSelected = true;
    }

    $scope.set_exercise = function () {
        $scope.SetExistingExerciseSelected = true;

        ExerciseDatas.GetClozeExercises().then(function (d) {
            console.log('Data GetClozeExercises(): ' + d.data);
            $scope.ClozeExercises = d.data;
        });
    }

    $scope.Message = 'Sélectionnez un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau 'facile')";
        $scope.ExerciseClozeData.Level.Name = "Easy";
    }

    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau 'moyen')";
        $scope.ExerciseClozeData.Level.Name = "Medium";
    }

    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau 'difficile')";
        $scope.ExerciseClozeData.Level.Name = "Hard";
    }

    $scope.Message = 'Configuration de l\'exercice';
    $scope.Button = 'Sauvegarder';
    $scope.IsFormValid = false;
    $scope.ExerciseClozeData = {
        Name: '', 
        Text: '',
        HiddenWords: '',
        Level: {
            Name: ''
        },
        Chapter: {
            Name: ''
        },
        UsersIds: []
    };

    $scope.Children = null;
    $scope.Levels = null;
    
    ExerciseDatas.GetChildren(sessions_elements.data.Class.ClassId).then(function (d) {
            console.log( 'Data GetChildren: ' + d.data );
            $scope.Children = d.data;

            for (var i = 0; i < $scope.Children.length; i++ ) {
                $scope.ExerciseClozeData.UsersIds.push( $scope.Children[i].UserId );
            }
            console.log( 'UsersIds in scope : ' + $scope.ExerciseClozeData.UsersIds );
        });

    ExerciseDatas.GetLevels().then(function (d) {
        console.log( 'Data GetLevel: ' + d.data );
        $scope.Levels = d.data;
    });

        //ExerciseDatas.GetClozeExercise().then(function (d) {
        //    $scope.DatabaseText = d.data.Text;
        //    $scope.Exercise.Words = d.data.Words;
        //    $scope.Exercise.Text = d.data.Text;
        //    $scope.Selections = [];

        //var textLowerCase = d.data.Text.toLowerCase();
        //var wordsText = textLowerCase.split(/[\s,.:;]+/);
        //var wordsWithCount = [];
        //var uniqueWords = [];
        //var arrayJsonFormat = [];

        //var savedIndex = 0;

        //// Sorting as we have a unique words array
        //for(var i = 0; i < wordsText.length; ++i ) {
        //    if ($.inArray(wordsText[i], uniqueWords) == -1) {
        //        var word = {
        //            'value': wordsText[i],
        //            'count': 1
        //        };
        //        arrayJsonFormat.push( word );
        //        uniqueWords.push(wordsText[i]);
        //    } else {
        //        for (var j = 0; j < arrayJsonFormat.length; j++) {
        //            if( arrayJsonFormat[j].value == wordsText[i] ) {
        //                arrayJsonFormat[j].count += 1;
        //                break;
        //            }
        //        }
        //    }
        //}
        //$scope.myData = arrayJsonFormat;
        //}, function (error) {
        //    alert("An error occured");
        //});

        //$scope.gridOptions = {
        //    data: 'myData',
        //    selectedItems: $scope.Selections,
        //    multiSelect: true
        //};

    ExerciseDatas.GetChapters().then(function (d) {
        $scope.Chapters = d.data;
    }, function (error) {
        alert('An error occured. See console for more details.')
        console.log('Error L435 is ' + error);
    });

    $scope.$watch('ClozeExercise', function (newValue) {
        $scope.IsFormValid = newValue;
    });

    $scope.$watch('setClozeEx', function (value) {
        $scope.IsFormValid = value;
    });

    $scope.update_data = function () {
        // TODO
    };

    $scope.SaveData = function () {
        console.log( 'in SaveData(), l896' );
        ExerciseDatas.CreateClozeExercise( $scope.ExerciseClozeData ).then(function (creationInfo) {
            var state = creationInfo.data;
            if( state == 'created' ) {
                $scope.Button = 'Exercice sauvegardé !'
                console.log('State: exercise saved');
            } else if( state == 'error existing name' ) {
                alert( 'Attention : la création de l\'exercice n\'a pas pu aboutir. Il existe déjà un exercice du même nom.' );
            } else if( state == 'error validation form' ) {
                alert( 'Erreur lors de la soumission de l\'exercice : le formulaire n\'est pas valide.' );
            } else {
                alert( 'L\'exercice n\'a pas pu être sauvegardé. Vérifiez vos entrées ou recommencez.' );
            }
        });
    };
})
.controller("TeacherRegistrationController", function ($scope, RegistrationService, LoginService) {

    sessionStorage.removeItem("objet");
    $scope.submitText = "Inscription";
    $scope.submitted = false;
    $scope.IsRegistered = false;
    $scope.Message = "Inscription";
    $scope.message = "";
    $scope.IsFormValid = false;
    $scope.ButtonMessage = "Connexion";

    $scope.User = {
        Nickname: '',
        Password: '',
        FirstName: '',
        LastName: '',
        Mail: '',
        Class: {
            Name: ''
        },
        Group: {
            Name: 'Professeurs'
        }
    };

    $scope.Classes = null;

    RegistrationService.GetClasses().then(function (d) {
        $scope.Classes = d.data;
    }, function (error) {
        alert('Error on classes 424');
    });

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Inscription") {
            $scope.submitted = true;
            $scope.message = "";
            if ($scope.IsFormValid) {
                $scope.submitText = "Veuillez patienter s'il vous plaît";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Le compte a bien été créé.') {
                        $scope.IsRegistered = true;

                        $scope.Message = "Entrez vos identifiants pour vous connecter.";
                        $scope.IsLogedIn = false;
                        $scope.Submitted = false;
                        $scope.IsFormValid = false;

                        $scope.LoginData = {
                            Username: '',
                            Password: ''
                        };
                        //Check if Form is valid or not // here f1 is our form Name
                        $scope.$watch('LoginForm.$valid', function (newVal) {
                            $scope.IsFormValid = newVal;
                        });
                        $scope.Login = function () {
                            $scope.Submitted = true;
                            if ($scope.IsFormValid) {
                                $scope.ButtonMessage = "Connexion en cours...";
                                LoginService.GetUser($scope.LoginData).then(function (d) {
                                    if (d.data.Username == null && d.data.Nickname == null) {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Vous avez entré un mauvais pseudo ou celui-ci n'existe pas. Veuillez réessayer ou vous inscrire si ce n'est pas le déjà le cas.");
                                    } else if (d.data.Nickname != null && d.data.Password != $scope.LoginData.Password) {
                                        $scope.LoginData.Password = "";
                                        alert("Vous avez dû entrer un mauvais mot de passe. Veuillez réessayer.");
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Professeurs" && d.data.Password == $scope.LoginData.Password) {
                                        var monobjet_json = JSON.stringify(d);
                                        sessionStorage.setItem("objet", monobjet_json);

                                        var monobjet_json = sessionStorage.getItem("objet");
                                        var monobjet = JSON.parse(monobjet_json);
                                        // Affichage dans la console
                                        console.log(monobjet.data.FirstName);

                                        $scope.IsLogedIn = true;
                                        $scope.Message = "Vous êtes bien connecté. Bienvenue " + d.data.FirstName + " " + d.data.LastName;
                                    } else if (d.data.Nickname != null && d.data.Group.Name == "Élèves") {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Oops, tu es dans l'espace des professeurs. Vas dans l'espace des élèves pour te connecter.");
                                        $scope.Message = "Rejoins l'espace 'Enfant'";
                                        $scope.IsKid = true;
                                    }
                                    else {
                                        $scope.LoginData.Username = "";
                                        $scope.LoginData.Password = "";
                                        alert("Une erreur s'est produite. Veuillez contacter le service technique.")
                                    }

                                    console.log(d.data.FirstName + " " + d.data.LastName);
                                })
                            }
                        };
                        //Have to clear form here
                        //ClearForm();
                    }
                    $scope.submitText = "Inscription";
                });
            }
            else {
                $scope.message = 'Remplissez bien tous les champs.';
            }
        }
    }
    //Clear form (reset)
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})
.controller('TeacherSelectMatterController', function ($scope) {
    $scope.Message = "Selectionnez une matière";
})
.controller('TeacherDragAndDropMathsController', function ($scope) {
    $scope.Message = "Drag and Drop";
})
.controller('TeacherSelectExercicesController', function ($scope) {
    $scope.Message = 'Selectionnez un exercice à modifier';
})
.controller('TeacherDictationController', function ($scope, SaveDictationText) {

    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de la dictée");
    console.log("Sa classe est : " + monobjet.data.Class.Name);

    $scope.Message = 'Selectionnez un niveau.';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.Message2 = "";
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    $scope.ExerciseDictationData = {
        //A REMPLIR
        Text: '',
        Level: {
            Name: 'Test'
        },
        AudioData: '',
        UsersIds: ''
    };

    $scope.Children = null;
    $scope.selected = [];

    SaveDictationText.GetChildren(monobjet.data.ClassId).then(function (d) {
        $scope.Children = d.data;
        $scope.ExerciseDictationData.UsersIds = $scope.Children;
        console.log($scope.ExerciseDictationData.UsersIds);
        //alert($scope.Grades[0].Name);
    });

    $scope.toggle = function (child, list) {
        var idx = list.indexOf(child.UserId);
        if (idx > -1)
            list.splice(idx, 1);
        else
            list.push(child.UserId);
    };

    $scope.exists = function (child, list) {
        return list.indexOf(child.UserId) > -1;
    };

    //Check if Form is valid or not // here DictText is our form Name
    $scope.$watch('DictText.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });

    $scope.IsFormValid
    $scope.SaveText = function () {
        if ($scope.IsFormValid) {
            $scope.Button = "Sauvegarde en cours..."
            $scope.ExerciseDictationData.Text.trim();
            $scope.ExerciseDictationData.Text = monobjet.data.Nickname + "/" + $scope.ExerciseDictationData.Text;
            var res = $scope.ExerciseDictationData.Text.split("/");

            if ($scope.ExerciseDictationData.Level.Name != "Easy")
                $scope.ExerciseDictationData.UsersIds = $scope.selected;
            else if ($scope.ExerciseDictationData.Level.Name != "Medium")
                $scope.ExerciseDictationData.UsersIds = $scope.selected;
            else if ($scope.ExerciseDictationData.Level.Name != "Hard")
                $scope.ExerciseDictationData.UsersIds = $scope.selected;

            SaveDictationText.GetText($scope.ExerciseDictationData).then(function (d) {
                $scope.ExerciseDictationData.Text = res[1];
                console.log(d.data);
                if (d.data == "Jeu enregistré")
                    $scope.Button = "Dictée sauvegardée";
                else {
                    alert(d.data);
                    $scope.Button = "Sauvegarder"
                }
            })
        }
    };    

    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Insérez le texte (Niveau facile)";
        $scope.ExerciseDictationData.Level.Name = "Easy";
    }
    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Insérez le texte (Niveau moyen)";
        $scope.ExerciseDictationData.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Insérez le texte (Niveau difficile)";
        $scope.ExerciseDictationData.Level.Name = "Hard";
    }

    function __log(e, data) {
        log.innerHTML += "\n" + e + " " + (data || '');
    }

    var audio_context;
    var recorder;

    function startUserMedia(stream) {
        var input = audio_context.createMediaStreamSource(stream);
        __log('Media stream created.');
        __log("input sample rate " + input.context.sampleRate);

        input.connect(audio_context.destination);
        __log('Input connected to audio context destination.');

        recorder = new Recorder(input);
        __log('Recorder initialised.');
    }

    $scope.startRecording = function () {
        var button = document.getElementById("StartButton");
        recorder && recorder.record();
        button.disabled = true;
        button.nextElementSibling.disabled = false;
        __log('Recording...');
    }

    $scope.stopRecording = function () {
        var button = document.getElementById("StopButton")
        recorder && recorder.stop();
        button.disabled = true;
        button.previousElementSibling.disabled = false;
        __log('Stopped recording.');

        // create WAV download link using audio data blob
        createDownloadLink();

        recorder.clear();
    }

    function createDownloadLink() {
        recorder && recorder.exportWAV(function (blob) {
            /*var url = URL.createObjectURL(blob);
            var li = document.createElement('li');
            var au = document.createElement('audio');
            var hf = document.createElement('a');

            au.controls = true;
            au.src = url;
            hf.href = url;
            hf.download = new Date().toISOString() + '.wav';
            hf.innerHTML = hf.download;
            li.appendChild(au);
            li.appendChild(hf);
            recordingslist.appendChild(li);*/

            /*objectURL = window.URL.createObjectURL(blob);
            window.location.href = objectURL;*/
        });
    }

    window.onload = function init() {
        try {
            // webkit shim
            window.AudioContext = window.AudioContext || window.webkitAudioContext;
            navigator.getUserMedia = (navigator.getUserMedia ||
                            navigator.webkitGetUserMedia ||
                            navigator.mozGetUserMedia ||
                            navigator.msGetUserMedia);
            window.URL = window.URL || window.webkitURL;

            audio_context = new AudioContext;
            __log('Audio context set up.');
            __log('navigator.getUserMedia ' + (navigator.getUserMedia ? 'available.' : 'not present!'));
        } catch (e) {
            alert('No web audio support in this browser!');
        }
        /*navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
            __log('No live audio input: ' + e);
        });*/
    }

    $scope.enableMicrophone = function () {
        var button = document.getElementById("EnableMicrophone");
        button.disabled = true
        button.nextElementSibling.disabled = false;
        var buttonStart = document.getElementById("StartButton")
        buttonStart.disabled = false;
        navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
            __log('No live audio input: ' + e);
        });
    }
    function disableMicrophone() {
        location.reload();
    }
    (function (window) {

        var WORKER_PATH = '/Scripts/recorderWorker.js';
        var encoderWorker = new Worker('/Scripts/mp3Worker.js');

        var Recorder = function (source, cfg) {
            var config = cfg || {};
            var bufferLen = config.bufferLen || 4096;
            this.context = source.context;
            this.node = (this.context.createScriptProcessor ||
                         this.context.createJavaScriptNode).call(this.context,
                                                                 bufferLen, 2, 2);
            var worker = new Worker(config.workerPath || WORKER_PATH);
            worker.postMessage({
                command: 'init',
                config: {
                    sampleRate: this.context.sampleRate
                }
            });
            var recording = false,
              currCallback;

            this.node.onaudioprocess = function (e) {
                if (!recording) return;
                worker.postMessage({
                    command: 'record',
                    buffer: [
                      e.inputBuffer.getChannelData(0),
                      //e.inputBuffer.getChannelData(1)
                    ]
                });
            }

            this.configure = function (cfg) {
                for (var prop in cfg) {
                    if (cfg.hasOwnProperty(prop)) {
                        config[prop] = cfg[prop];
                    }
                }
            }

            this.record = function () {
                recording = true;
            }

            this.stop = function () {
                recording = false;
            }

            this.clear = function () {
                worker.postMessage({ command: 'clear' });
            }

            this.getBuffer = function (cb) {
                currCallback = cb || config.callback;
                worker.postMessage({ command: 'getBuffer' })
            }

            this.exportWAV = function (cb, type) {
                currCallback = cb || config.callback;
                type = type || config.type || 'audio/wav';
                if (!currCallback) throw new Error('Callback not set');
                worker.postMessage({
                    command: 'exportWAV',
                    type: type
                });
            }

            //Mp3 conversion
            worker.onmessage = function (e) {
                var blob = e.data;
                //console.log("the blob " +  blob + " " + blob.size + " " + blob.type);

                var arrayBuffer;
                var fileReader = new FileReader();

                fileReader.onload = function () {
                    arrayBuffer = this.result;
                    var buffer = new Uint8Array(arrayBuffer),
                    data = parseWav(buffer);

                    console.log(data);
                    
                    var bytesArray = [];
                    for (var i = 0; i < data.samples.length; i++) {
                        bytesArray.push(data.samples[i]);
                    }

                    console.log("Converting to Mp3");
                    log.innerHTML += "\n" + "Converting to Mp3";

                    encoderWorker.postMessage({
                        cmd: 'init', config: {
                            mode: 3,
                            channels: 1,
                            samplerate: data.sampleRate,
                            bitrate: data.bitsPerSample
                        }
                    });

                    encoderWorker.postMessage({ cmd: 'encode', buf: Uint8ArrayToFloat32Array(data.samples) });
                    encoderWorker.postMessage({ cmd: 'finish' });
                    encoderWorker.onmessage = function (e) {
                        if (e.data.cmd == 'data') {

                            console.log("Done converting to Mp3");
                            log.innerHTML += "\n" + "Done converting to Mp3";

                            /*var audio = new Audio();
                            audio.src = 'data:audio/mp3;base64,'+encode64(e.data.buf);
                            audio.play();*/

                            //console.log ("The Mp3 data " + e.data.buf);

                            var mp3Blob = new Blob([new Uint8Array(e.data.buf)], { type: 'audio/mp3' });
                            uploadAudio(mp3Blob);

                            var url = 'data:audio/mp3;base64,' + encode64(e.data.buf);
                            $scope.ExerciseDictationData.AudioData = url;
                            var li = document.createElement('li');
                            var au = document.createElement('audio');
                            var hf = document.createElement('a');

                            au.controls = true;
                            au.src = url;
                            hf.href = url;
                            hf.download = 'audio_recording_' + new Date().getTime() + '.mp3';
                            hf.innerHTML = hf.download;
                            li.appendChild(au);
                            li.appendChild(hf);
                            recordingslist.appendChild(li);

                        }
                    };
                };

                fileReader.readAsArrayBuffer(blob);

                currCallback(blob);
            }


            function encode64(buffer) {
                var binary = '',
                    bytes = new Uint8Array(buffer),
                    len = bytes.byteLength;

                for (var i = 0; i < len; i++) {
                    binary += String.fromCharCode(bytes[i]);
                }
                return window.btoa(binary);
            }

            function parseWav(wav) {
                function readInt(i, bytes) {
                    var ret = 0,
                        shft = 0;

                    while (bytes) {
                        ret += wav[i] << shft;
                        shft += 8;
                        i++;
                        bytes--;
                    }
                    return ret;
                }
                if (readInt(20, 2) != 1) throw 'Invalid compression code, not PCM';
                if (readInt(22, 2) != 1) throw 'Invalid number of channels, not 1';
                return {
                    sampleRate: readInt(24, 4),
                    bitsPerSample: readInt(34, 2),
                    samples: wav.subarray(44)
                };
            }

            function Uint8ArrayToFloat32Array(u8a) {
                var f32Buffer = new Float32Array(u8a.length);
                for (var i = 0; i < u8a.length; i++) {
                    var value = u8a[i << 1] + (u8a[(i << 1) + 1] << 8);
                    if (value >= 0x8000) value |= ~0x7FFF;
                    f32Buffer[i] = value / 0x8000;
                }
                return f32Buffer;
            }

            function uploadAudio(mp3Data) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    var fd = new FormData();
                    var mp3Name = encodeURIComponent('audio_recording_' + new Date().getTime() + '.mp3');
                    console.log("mp3name = " + mp3Name);
                    fd.append('fname', mp3Name);
                    fd.append('data', event.target.result);
                    $.ajax({
                        type: 'POST',
                        url: 'upload.php',
                        data: fd,
                        processData: false,
                        contentType: false
                    }).done(function (data) {
                        //console.log(data);
                        log.innerHTML += "\n" + data;
                    });
                };
                reader.readAsDataURL(mp3Data);
            }

            source.connect(this.node);
            this.node.connect(this.context.destination);    //this should not be necessary
        };

        /*Recorder.forceDownload = function(blob, filename){
          console.log("Force download");
          var url = (window.URL || window.webkitURL).createObjectURL(blob);
          var link = window.document.createElement('a');
          link.href = url;
          link.download = filename || 'output.wav';
          var click = document.createEvent("Event");
          click.initEvent("click", true, true);
          link.dispatchEvent(click);
        }*/

        window.Recorder = Recorder;

    })(window);
})
.factory('SaveDictationText', function ($http) {
    var fac = {};
    var data = "";

    fac.GetChildren = function (d) {
        return $http.get('/Data/GetSpecificChilden/' + d)
    }
    
    fac.GetText = function (d) {
        return $http({
            url: '/Data/SaveDictation',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})
.controller('TeacherDragAndDropController', function ($scope, SaveDragAndDropExercice) {
    $scope.Message = "Modifiez à vos souhaits l'exercice du 'Glissé-Déposé'";
    $scope.card1 = "1";
    $scope.card2 = "2";
    $scope.card3 = "3";
    $scope.card4 = "4";
    $scope.card5 = "5";
    $scope.card6 = "6";
    $scope.card7 = "7";
    $scope.card8 = "8";
    $scope.card9 = "9";
    $scope.card10 = "10";

    $scope.IsSaved = false;
    $scope.Submitted = false;
    $scope.IsFormValid = false;

    $scope.CardsData = {
        Card1: '', Card2: '',
        Card3: '', Card4: '',
        Card5: '', Card6: '',
        Card7: '', Card8: '',
        Card9: '', Card10: ''
    };
    //Check if Form is valid or not // here SaveTeacherDragAndDrop is our form Name
    $scope.$watch('SaveDragAndDropTeacher.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });

    $scope.Save = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            LoginService.GetUser($scope.CardsData).then(function (d) {
                if (d.data.Card1 != null) {
                    $scope.IsSaved = true;
                    $scope.Message = "Yup";
                }
                else {
                    alert('Woops')
                }
            })
        }
    };
})
.factory('SaveDragAndDropExercice', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUser = function (d) {
        return $http({
            url: '/Data/SaveDragAndDropTeacher',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})

// Description customize BattleCard
.controller('TeacherDescriptionBattleCardController', function ($scope) {

})

// Customize BattleCard
.controller('TeacherCustomizeBattleCardController', function ($scope, SaveBattleCardChoice) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la modification de card game");
    console.log("ClassId : " + monobjet.data.ClassId)

    $scope.Message = 'Choix du niveau';
    $scope.EasySelected = false;
    $scope.MediumSelected = false;
    $scope.HardSelected = false;
    $scope.IsFormValid = false;
    $scope.Button = "Sauvegarder";

    $scope.ExerciseBattleCard = {
        //A REMPLIR
        Choice: '',
        Level: {
            Name: 'Test'
        }
    };

    $scope.Children = null;
    $scope.selected = [];

    SaveBattleCardChoice.GetUsers(monobjet.data.ClassId).then(function (d) {
        //console.log("test");
        //console.log(d);
        //console.log("Taille de la data : " + d.data.length);
        //console.log(d.data);
        //console.log(d.data[0]);
        //console.log(d.data[1]);
        //console.log(d.data[0].FirstName);
        //console.log(d.data[0].LastName);
        //console.log(d.data[0].Nickname);
        //console.log(d.data[0].ClassId);

        $scope.Children = d.data;
        $scope.ExerciseBattleCard.Users = $scope.Children;
        console.log($scope.ExerciseBattleCard.Users);
    });

    $scope.toggle = function (child, list) {
        var idx = list.indexOf(child);
        if (idx > -1)
            list.splice(idx, 1);
        else
            list.push(child);
    };

    $scope.exists = function (child, list) {
        return list.indexOf(child) > -1;
    };

    //Check if Form is valid or not // here FormChoice is our form Name
    $scope.$watch('FormChoice.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });
    $scope.IsFormValid
    $scope.SaveChoice = function () {
        if ($scope.IsFormValid && $scope.ExerciseBattleCard.Choice != "") {
            $scope.Button = "Sauvegarde en cours..."
            $scope.ExerciseBattleCard.Choice.trim();
            $scope.ExerciseBattleCard.Choice = monobjet.data.Nickname + "/" + $scope.ExerciseBattleCard.Choice;
            var res = $scope.ExerciseBattleCard.Choice.split("/");

            if ($scope.ExerciseBattleCard.Level.Name != "Easy") {
                console.log($scope.ExerciseBattleCard.Level.Name);
                $scope.ExerciseBattleCard.Users = $scope.selected;
            }
                

            SaveBattleCardChoice.GetChoice($scope.ExerciseBattleCard).then(function (d) {
                $scope.ExerciseBattleCard.Choice = res[1];
                console.log(d.data);
                if (d.data == "Jeu enregistré")
                    $scope.Button = "Battle Card sauvegardé";
                else {
                    alert(d.data);
                    $scope.Button = "Sauvegarder"
                }
            })
        }
    };
    $scope.Easy = function () {
        $scope.EasySelected = true;
        $scope.Message = "Niveau facile";
        $scope.ExerciseBattleCard.Level.Name = "Easy";
    }

    $scope.Medium = function () {
        $scope.MediumSelected = true;
        $scope.Message = "Niveau moyen";
        $scope.ExerciseBattleCard.Level.Name = "Medium";
    }
    $scope.Hard = function () {
        $scope.HardSelected = true;
        $scope.Message = "Niveau difficile";
        $scope.ExerciseBattleCard.Level.Name = "Hard";
    }
})

.factory('SaveBattleCardChoice', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUsers = function (data) {
        //return $http.get('/Data/GetClasses')
        return $http.get('/Data/GetUsersByClasses/' + data)
    }
    fac.GetChoice = function (d) {
        return $http({
            url: '/Data/SaveBattleCard',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };
    return fac;
})

// Kid description BattleCard
.controller('KidDescriptionBattleCardController', function ($scope) {

})

// Play BattleCard
.controller('KidPlayBattleCardController', function ($scope) {
    var monobjet_json = sessionStorage.getItem("objet");
    var monobjet = JSON.parse(monobjet_json);
    // Affichage dans la console
    console.log(monobjet.data.FirstName + " est dans la page de jeu card game");


    $scope.Time = 'Vous avez 1 minute(s) ! ';
    $scope.Score = 0
    $scope.svgCard = "/Images/redCard.svg";
})

.controller('KidHomeController', function ($scope) {
    $scope.Message = 'Page "Élève"';
})

.controller('ErrorController', function ($scope) {
    $scope.Message = "404 Not Found!";
})

    // INSCRIPTION
.controller('Registration', function ($scope, RegistrationService) {
    //Default Variable
    $scope.submitText = "Save";
    $scope.submitted = false;
    $scope.message = "";
    $scope.IsFormValid = false;
    $scope.User = {
        UserName: 'Steve',
        Password: '', 
        FullName: '',
        EmailID: '',
        Gender: ''
    };

    //Check form validation // here RegisterForm is our form name
    $scope.$watch("RegisterForm.$valid", function (newValue) {
        $scope.IsFormValid = newValue;
    })

    //Save Data
    $scope.SaveData = function (data) {
        if ($scope.submitText == "Save") {
            $scope.submitted = true;
            $scope.message = "";

            if ($scope.IsFormValid) {
                $scope.submitText = "Please wait...";
                $scope.User = data;
                RegistrationService.SaveFormData($scope.User).then(function (d) {
                    alert(d);
                    if (d == 'Success') {
                        //Have to clear form here
                        ClearForm();
                    }
                    $scope.submitText = "Save";
                });
            }
            else {
                $scope.message = 'Please fill required fields value';
            }
        }
    }

    //Clear form (reset)
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine(); // here registerForm is our form name
        $scope.submitted = false;
    }
})

//Factories
    //Login Factory
.factory('LoginService', function ($http) {
    var fac = {};
    var data = "";
    fac.GetUser = function (d) {
        return $http({
            url: '/Data/UserLogin',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        })
    };

    return fac;
})
.factory('CheckDictationText', function ($http) {
    var fac = {};
    var data = "";
    fac.GetDictationText = function (d) {
        return $http({
            url: '/Data/CheckDictationText',
            method: 'POST',
            data: JSON.stringify(d),
            header: { 'content-type': 'application/json' }
        })
    };
    return fac;
})
    //Registration Factory
.factory('RegistrationService', function ($http, $q) {
    //here q is an angularJS service which helps us to run asynchronous function and return result when processing is done
    var fac = {};
    fac.SaveFormData = function (data) {
        var defer = $q.defer();
        $http({
            url: '/Data/Register',
            method: 'POST',
            data: JSON.stringify(data),
            header: { 'content-type': 'application/json' }
        }).success(function (d) {
            //Success callback
            defer.resolve(d);
        }).error(function (e) {
            //Failed callback
            console.log( "An error occured. Error is : " + e + "Data is : " + JSON.stringify(data) );
            defer.reject(e);
        });
        return defer.promise;
    }

    fac.GetClasses = function () {
        return $http.get('/Data/GetClasses')
    }
    return fac;
})

.factory('ExerciseDatas', function ($http) {
    var fac = {};
    var data = "";
    fac.CreateClozeExercise = function (d) {
        return $http({
            url: '/Data/CreateClozeExercise',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            defer.resolve(d);
        }).error(function (e) {
            defert.reject(e);
        });
    };

    fac.GetClozeExercises = function () {
        return $http.get( '/Data/GetClozeExercises' );
    }

    fac.GetClasses = function () {
        return $http.get('/Data/GetClasses')
    }

    fac.GetChildren = function ( data ) {
        return $http.get('/Data/GetUsersByClasses/' + data);
    }

    fac.GetGroups = function () {
        return $http.get('/Data/GetGroups')
    }

    fac.GetChapters = function () {
        return $http.get('/Data/GetChapters')
    }

    fac.GetLevels = function () {
        return $http.get('/Data/GetLevels');
    }

    return fac;
});