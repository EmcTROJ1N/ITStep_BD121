let redirect = () => window.location.href = "./../";

function deleteSelected()
{
    let filesArr = new Array();
    document.getElementsByName("fileCheckBox").forEach(checkBox => 
    {
        if (checkBox.checked)
            filesArr.push(checkBox.value);
    });
    
    fetch("./DeleteFiles", {
        method: 'POST',
        headers: {
            'Accept': 'application/json; charset=utf-8',
            'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify(filesArr)
    }).then(response => response.json())
        .then((result) => location.reload())
        .catch((err) => alert(err));
}

function submitButton(e)
{
    e.preventDefault();
    let linkId = e.target.getAttribute("id");
    document.getElementById((linkId)).submit();
}