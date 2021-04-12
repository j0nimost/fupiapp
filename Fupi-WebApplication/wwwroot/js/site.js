// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function CopyToClipboard() {
    var copyText = document.getElementById("url");
    console.log("logText- " + copyText.innerText);
    navigator.clipboard.writeText(copyText.innerText);
    var tooltip = document.getElementById("myTooltip");
    
    tooltip.innerHTML = "Copied";
}

