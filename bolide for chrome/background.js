const postComment = () => {
    fetch('https://bolide.digicre.net/api/v1/comment/teireikai230417', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            comment: 'aaaa',
            is_question: false
        })
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error(error))
}

chrome.runtime.onStartup.addListener(() => {
    chrome.storage.local.set({
        isConnect: false
    },function(){
        updateBadge();
    });
});

chrome.runtime.onMessage.addListener(function(message, sender, sendResponse) {
    console.log("message");
    switch (message.type) {
        case "updateBadge":
            updateBadge();
            break;
        default:
            break;
    }
    return true;
});


const updateBadge = () => {
    chrome.storage.local.get({
        isConnect: false
    },(items) => {
        if(items.isConnect){
            chrome.action.setBadgeText({text: "ON"});
            chrome.action.setBadgeBackgroundColor({color: "#0000FF"});
        }else{
            chrome.action.setBadgeText({text: ""});
        }
    });
}