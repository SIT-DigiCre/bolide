if (location.href.startsWith("https://bolide.digicre.net/?roomname=")) {
    window.addEventListener("load", () => {
        const interval = setInterval(() => {
            if (document.querySelector(".main")) {
                clearInterval(interval);
                insertExtArea();
            }
        }, 1000);
    });

}



const insertExtArea = () => {
    const roomName = location.href.split("?roomname=")[1];
    chrome.storage.local.set({
        roomName: roomName
    }, () => {
        chrome.storage.local.get({
            isConnect: false
        }, (items) => {
            document.querySelector(".main").insertAdjacentHTML("beforeEnd", `
        <style>
            .bolide-extension-area {
                float: left;
                width: 30rem;
                height: 450px;
                margin-top: 1.1rem;
            }
            .main .content {
                float: left;
                width: calc(100% - 30rem);
            }
            @media screen and (min-width: 1280px) {
                .main .content {
                    width: calc(1280px - 30rem);
                    margin-left: calc(50vw - 640px);
                }
            }
            @media screen and (max-width: 1000px) {
                .main .content,.bolide-extension-area {
                    float: none;
                }
                .main .content {
                    width: 100%;
                }
                .bolide-extension-area {
                    margin: 3rem auto;
                }
            }
        </style>
        <div class="bolide-extension-area card-deck text-center">
        <div class="card mb-4 box-shadow">
            <div class="card-header">
                <h4 class="my-0 font-weight-normal">bolide for Chrome</h4>
            </div>
            <div class="card-body mt-1">
                <h1 class="card-title pricing-card-title mt-2"><small class="text-muted p-2">room</small>${roomName} </h1>
                <ul class="list-unstyled mt-4 mb-5">
                    <li>拡張機能にルーム名が自動入力されました。</li>
                    <li>このページ以外でもポップアップからコメントできます。</li>
                    <li>「コメントスクリーン開始」をクリックすると</li>
                    <li>ブラウザ内で右から流れるコメントスクリーンが表示されます。</li>
                </ul>
                <button type="button" class="btn btn-lg btn-block btn-outline-primary" id="start-bolide-screen">コメントスクリーン${items.isConnect ? "終了" : "開始"}</button>
                <button type="button" class="btn btn-lg btn-block btn-outline-primary" id="execute-bolide-desktop">デスクトップ版起動</button>
            </div>
        </div>
        </div>
        `);
            document.getElementById("execute-bolide-desktop").addEventListener("click", () => {
                alert("この機能は現在開発中です。");
                return;
            });
            document.getElementById("start-bolide-screen").addEventListener("click", () => {
                chrome.storage.local.get({
                    isConnect: false
                }, (itm) => {
                    //接続済みだったら接続を切る
                    if (itm.isConnect) {
                        chrome.storage.local.set({
                            isConnect: false
                        }, () => {
                            connection.close();
                        });
                        document.getElementById("start-bolide-screen").innerHTML = "コメントスクリーン開始";
                    } else {
                        chrome.storage.local.set({
                            isConnect: true
                        }, () => {
                            connection.close();
                            connection.connect(roomName);
                        });
                        document.getElementById("start-bolide-screen").innerHTML = "コメントスクリーン終了";
                    }
                });
            });
        });
    });
};