const screentime = 10; //コメント表示時間


class WebsocketAction {
    constructor() {
        this.roomName = null;
        this.ws = null;
        this.commentsArea = [null, null, null, null, null, null, null, null, null, null, null, null, null];
    };
    //接続する
    connect(roomName) {
        this.roomName = roomName;
        if(roomName == "") return;
        this.ws = new WebSocket('wss://bolide.digicre.net/api/v1/room/' + roomName);

        this.ws.onopen = () => {
            console.log("Connected.");
            chrome.runtime.sendMessage({ type: "updateBadge" });
        };

        this.ws.onmessage = (event) => {
            const commentHeight = this.commentsArea.indexOf(null);
            this.commentsArea[commentHeight] = event.data;
            displayComment(JSON.parse(event.data), this.roomName, commentHeight);
            console.log(commentHeight);
            if (commentHeight > -1) {
                setTimeout(() => {
                    this.commentsArea[commentHeight] = null;
                }, screentime * 1000);
            }
        };

        this.ws.onclose = (event) => {
            chrome.storage.local.get({
                isConnect: true
            }, (items) => {
                if (items.isConnect) {
                    console.log('Socket is closed. 再接続します。', event.reason);
                    this.connect(this.roomName);
                } else {
                    console.log('Socket is closed. 再接続しません。', event.reason);
                }
            });
        };

        this.ws.onerror = (event) => {
            console.error(event.message);
            this.ws.close();
            alert("接続に失敗しました。");
        };
    }
    //切断する
    close() {
        if (this.ws) {
            this.ws.close();
            console.log("切断しました。", this.roomName);
            chrome.runtime.sendMessage({ type: "updateBadge" });
        }
    }
}

class Comment {
    constructor(content, roomName) {
        this.content = content;
        this.roomName = roomName;
    }
    start(commentHeight) {
        chrome.storage.local.get({
            textColor: "#ff0041"
        }, (items) => {
            console.log("start", this.content, this.roomName);
            this.node = document.createElement("div");
            this.node.classList.add("bolide-ext-comment");
            this.node.innerHTML = ((this.content.is_question) ? "[質問]" : "") + this.content.comment;
            this.node.style.top = commentHeight * 8 + "vh";
            this.node.style.width = this.content.comment.length * 8 + "vh";
            this.node.style.color = items.textColor;
            const insertAt = document.fullscreenElement || document.body || document.documentElement;
            insertAt.appendChild(this.node);
        });
    }
    remove() {
        this.node.remove();
    }
}

//ページ読み込み時に実行状態だったらconnect
const connection = new WebsocketAction();
chrome.storage.local.get({
    roomName: "",
    isConnect: true
}, (items) => {
    if (items.isConnect) {
        connection.connect(items.roomName);
    }
});


//ボタンが押されたときの処理
chrome.runtime.onMessage.addListener((r, sender, sendResponse) => {
    const request = JSON.parse(r);
    console.log(request);
    if (request.type === "connect") {
        connection.close();
        connection.connect(request.roomName);
    } else if (request.type === "disconnect") {
        connection.close();
    }
    return true;
});


//コメントcss
document.addEventListener("DOMContentLoaded", () => {
    document.body.insertAdjacentHTML("beforeend", `
    <style>
    .bolide-ext-comment {
        animation-name: scroll-bolide-comment;
        animation-duration:${screentime}s;
        animation-timing-function: linear;
        animation-fill-mode: forwards;
        display: block;
        position: fixed;
        z-index: 999999999999999999999999;
        font-size: 8vh;
        white-space: nowrap;
        text-align: center;
        margin:0;
        padding:0;
    }
    @keyframes scroll-bolide-comment {
    0% {
        transform: translateX(100%);
        right: 0;
        }
    
    100% {
        transform: translateX(0);
        right: 100%;
        }
    }
    </style>
    `);
});


//コメント表示
const displayComment = (content, roomName, commentHeight) => {
    const comment = new Comment(content, roomName);
    chrome.storage.local.get({
        isConnect: true
    }, (items) => {
    if(items.isConnect){
            comment.start(commentHeight);
        setTimeout(() => {
            comment.remove();
        }, screentime * 1000);
        }
    });
}