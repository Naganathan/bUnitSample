
window.render = function (nodes) {
    var kk = document.createElement("div");
    document.body.appendChild(kk);

    var ss = kk.getBoundingClientRect();

    return {isRendering:true};
}
