document.body.style.backgroundColor="red";

chrome.runtime.sendMessage({name: document.title, description: document.body.innerHTML, image:"image", price:"10", currency:"USD"}, function(response) {
  console.log(response.farewell);
});

