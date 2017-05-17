
function BrowseServer() {
    var finder = new CKFinder();
    //finder.basePath = '../';
    finder.selectActionFunction = SetFileField;
    finder.popup();
}
function SetFileField(fileUrl) {
    document.getElementById('Image').value = fileUrl;
}

function DeleteConfirm()
{
    return confirm("Bạn có chắc muốn xóa mục này?");
}