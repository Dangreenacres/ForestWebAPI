(function () {
    viewModel.musics([]);
    $.get('/api/Music', function (data) {
        viewModel.musics(data);
    });
});
