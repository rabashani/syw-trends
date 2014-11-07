var saveThisItemEndpoint = 'http://RYW.apphb.com/home/add';
var clientParsedData; 

chrome.runtime.onInstalled.addListener(function(details) {
    console.log('installed');
});

chrome.runtime.onMessage.addListener(function(message, sender, sendResponse) {
    console.log("message recieved: " + message);
    //TODO: this method should clean the description, identify images, etc.
    clientParsedData = message; // message.name, message.description, message.image, message.price, message.currency 
});

chrome.browserAction.onClicked.addListener(function(tab) {
 
  chrome.tabs.executeScript(null, {file: "mycrawler.js"});

  var productUrl = tab.url;
  
  saveThisItem(productUrl);
  
  //success...
  chrome.browserAction.setBadgeText({text: "Yeah"});
  //chrome.runtime.getBackgroundPage('a.html');
});

function saveThisItem(productUrl)
{
  var addProductEndpoint = saveThisItemEndpoint + '?userid=1&url=' + productUrl;
  console.log(addProductEndpoint);

  var xhr = new XMLHttpRequest();
  xhr.open("GET", addProductEndpoint, true);
  xhr.onreadystatechange = function() {
    
    if (xhr.readyState == 4) {
      var serverParsedData  = xhr.responseText;
      //clientParsedData
      //showMainWindow(parsedData);
      console.log('done');
      showMainWindow()
    }
  }
  xhr.send();
}

var mainWindow;
function showMainWindow()
{
    // Create and open the main window
    chrome.app.window.create('a/index.html', {
        id: 'main',
        bounds: {
            width: 1040,
            height: 800
        }
    }, function (win) {
        mainWindow = win;

        // Register the on closed event to save on closed actions
        mainWindow.onClosed.addListener(function() {

            console.log('closed');
        });
    });
}
