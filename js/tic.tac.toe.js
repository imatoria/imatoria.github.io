function Resize() {
    var board = select(".board");

    board.style.width = "100%";
    board.style.height = "100%";

    var width = board.clientWidth;
    var height = board.clientHeight;

    if (width < 498 || height < 498) {
        board.style.width = Math.min(width, height) + "px";
        board.style.height = Math.min(width, height) + "px";
    }
}
window.onresize = Resize;
Resize();

window.ShowSwal = (player) => {
    Swal.fire({
        title: player + ' won!!',
        width: 350,
        padding: '3em',
        color: '#716add',
        backdrop: `
                        rgba(0,0,123,0.4)
                        left top
                        no-repeat
                      `
    }).then(() => {
        window.DotNetReference.invokeMethodAsync("ResetGame");
    });
}
window.ShowTie = () => {
    Swal.fire({
        title: 'Hard luck, nobody won!',
        width: 350,
        padding: '3em',
        color: '#716add',
        backdrop: `
                        rgba(0,0,123,0.4)
                        left top
                        no-repeat
                      `
    }).then(() => {
        window.DotNetReference.invokeMethodAsync("ResetGame");
    });
}