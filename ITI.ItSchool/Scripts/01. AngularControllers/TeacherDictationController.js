(function () {
    'use strict';
    angular.module('TheApp').controller('TeacherDictationController', function ($scope, SaveDictationText) {

        $scope.EmptySession = false;
        if (sessionStorage.getItem("objet") == null)
            $scope.EmptySession = true;
        else {
            var monobjet_json = sessionStorage.getItem("objet");
            var monobjet = JSON.parse(monobjet_json);
            // Affichage dans la console
            console.log(monobjet.data.FirstName + " est dans la modification de la dictée");
            console.log("Sa classe est : " + monobjet.data.Class.Name);

            $scope.GoBack = function () {
                window.history.back();
            };

            $scope.demo = {
                topDirections: ['left', 'up'],
                bottomDirections: ['down', 'right'],
                isOpen: false,
                availableModes: ['md-fling', 'md-scale'],
                selectedMode: 'md-scale',
                availableDirections: ['up', 'down', 'left', 'right'],
                selectedDirection: 'right'
            };

            $scope.Message = 'Selectionnez un niveau.';
            $scope.EasySelected = false;
            $scope.MediumSelected = false;
            $scope.HardSelected = false;
            $scope.Message2 = "";
            $scope.ShowRecorder = false;
            $scope.IsFormValid = false;
            $scope.Button = "Sauvegarder";
            var micCount = 0;
            $scope.MicButtonSVG = "../Images/material-design-icons/svg/ic_mic_off_48px.svg";

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

            var getDictation = function (LevelValue) {
                SaveDictationText.GetDictation(LevelValue).then(function (d) {
                    if (d.data != null)
                        $scope.ExerciseDictationData.Text = d.data.Text;
                });
            }

            $scope.Easy = function () {
                $scope.EasySelected = true;
                $scope.ShowRecorder = true;
                $scope.Message = "Insérez le texte (Niveau facile)";
                $scope.ExerciseDictationData.Level.Name = "Easy";
                getDictation(1);
            }

            $scope.Medium = function () {
                $scope.MediumSelected = true;
                $scope.ShowRecorder = true;
                $scope.Message = "Insérez le texte (Niveau moyen)";
                $scope.ExerciseDictationData.Level.Name = "Medium";
                getDictation(2);
            }

            $scope.Hard = function () {
                $scope.HardSelected = true;
                $scope.ShowRecorder = true;
                $scope.Message = "Insérez le texte (Niveau difficile)";
                $scope.ExerciseDictationData.Level.Name = "Hard";
                getDictation(3);
            }

            function __log(e, data) {
                log.innerHTML += "\n" + e + " " + (data || '');
            }

            var audio_context;
            var recorder;

            function startUserMedia(stream) {
                var input = audio_context.createMediaStreamSource(stream);

                //The commented line allow playback sound while using microphone
                //input.connect(audio_context.destination);

                recorder = new Recorder(input);
            }

            $scope.startRecording = function () {
                var button = document.getElementById("StartButton");
                recorder && recorder.record();
                button.disabled = true;
                button.nextElementSibling.disabled = false;
                document.getElementById("theSpan").textContent = "Enregistrement en cours...";
            }

            $scope.stopRecording = function () {
                var button = document.getElementById("StopButton")
                recorder && recorder.stop();
                button.disabled = true;
                button.previousElementSibling.disabled = false;
                document.getElementById("theSpan").textContent = "Veuillez patienter s'il vous plaît..";

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
                    window.URL = window.URL || window.webkitURL;

                    audio_context = new AudioContext;
                } catch (e) {
                    alert('No web audio support in this browser!');
                }
                /*navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
                    __log('No live audio input: ' + e);
                });*/
            }

            $scope.enableMicrophone = function () {
                if (micCount == 0) {
                    var button = document.getElementById("EnableMicrophone");
                    button.nextElementSibling.disabled = false;
                    var buttonStart = document.getElementById("StartButton")
                    buttonStart.disabled = false;
                    navigator.getUserMedia = (navigator.getUserMedia ||
                                   navigator.webkitGetUserMedia ||
                                   navigator.mozGetUserMedia ||
                                   navigator.msGetUserMedia);
                    audio_context = new AudioContext;
                    navigator.getUserMedia({ audio: true }, startUserMedia, function (e) {
                    });
                    micCount = 1;
                    $scope.MicButtonSVG = "../Images/material-design-icons/svg/ic_mic_48px.svg";
                }
                else {
                    micCount = 0;
                    location.reload();
                }

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
                                    $scope.RecordMessage = "L'enregistrement est prêt";

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
                            document.getElementById("theSpan").textContent = "Vous pouvez maintenant sauvegarder l'exercice ou bien écrire la dictée.";
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
        }
    })
})();