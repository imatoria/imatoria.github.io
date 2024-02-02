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