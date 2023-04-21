chrome.storage.local.get({
    roomName: "",
    isConnect: false,
    textColor: ["#ff0041","#ff9c00","#7190f2","#00bf1c","#8e40cf","#12178c"]
},(items) => {
    document.getElementById("roomName").value = items.roomName;
    if(items.isConnect){
        document.getElementById("logArea").innerHTML = "<span>✅接続中</span> - コメントが表示されます";
    }else{
        document.getElementById("logArea").innerHTML = "⛔未接続 - コメントは表示されません";
    }
    document.querySelectorAll(".colBtn").forEach((btn,i) => {
        btn.value = items.textColor[i];
    });
});

//接続時
document.getElementById("connect").addEventListener("click", () => {
    const roomName = document.getElementById("roomName").value;
    chrome.storage.local.set({
        roomName: roomName,
        isConnect: true
    },() => {
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
            chrome.tabs.sendMessage(tabs[0].id, 
            JSON.stringify({ 
                type: "connect",
                roomName: roomName 
            })
            );
        });
        document.getElementById("logArea").innerHTML = "<span>✅接続中</span> - コメントが表示されます";
    });
});
//切断時
document.getElementById("disconnection").addEventListener("click", () => {
    chrome.storage.local.set({
        isConnect: false
    },() => {
        chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
        chrome.tabs.sendMessage(tabs[0].id, 
            JSON.stringify({ 
                type: "disconnect"
            })
            );
        });
        document.getElementById("logArea").innerHTML = "⛔未接続 - コメントは表示されません";
    });
});
//テキストカラー
document.querySelectorAll(".colBtn").forEach((btn) => {
    btn.addEventListener("change", (e) => {
        chrome.storage.local.set({
            textColor: [...document.querySelectorAll(".colBtn")].map(x => x.value)
        });
        console.log([...document.querySelectorAll(".colBtn")].map(x => x.value));
    });
});

document.querySelector("form").addEventListener("submit", (e) => {
    e.preventDefault();
    if(document.getElementById("sendComment").value == ""){
        return;
    }
    
    fetch('https://bolide.digicre.net/api/v1/comment/'+encodeURIComponent(document.getElementById("roomName").value), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            comment: document.getElementById("sendComment").value,
            is_question: document.getElementById("q-check").checked
        })
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error))
    document.getElementById("sendComment").value = "";
});