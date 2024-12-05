function goToChatBot() {

    var div = document.createElement('div');
    div.id = 'chatBot';
    div.style.width = '350px';
    div.style.height = '400px';
    div.style.position = 'fixed';
    div.style.right = '20px'; 
    div.style.bottom = '20px'; 
    div.style.boxShadow = '0px 4px 8px rgba(0, 0, 0, 0.2)';
    div.style.borderRadius = '10px';
    div.style.overflow = 'hidden';
    div.style.backgroundColor = '#ffffff'; 
    div.style.zIndex = '1000';
    div.style.border = '1px solid #7C8BDE'


    var iframe = document.createElement('iframe');
    iframe.src = '/ChatBot/Index';
    iframe.style.width = '100%';
    iframe.style.height = '100%';
    iframe.style.border = 'none';
    iframe.style.zIndex = '1000';

    div.appendChild(iframe);

    var closeButton = document.createElement('button');
    closeButton.innerHTML = 'X';
    closeButton.style.position = 'absolute';
    closeButton.style.top = '5px';
    closeButton.style.right = '5px';
    // closeButton.style.backgroundColor = 'white';
    closeButton.style.color = 'red';
    closeButton.style.border = 'none';
    closeButton.style.padding = '5px 10px';
    closeButton.style.cursor = 'pointer';
    closeButton.style.borderRadius = '5px';
    closeButton.onclick = function() {
        document.body.removeChild(div);
    };

    div.appendChild(closeButton);

    document.body.appendChild(div);
}

