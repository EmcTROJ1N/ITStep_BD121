let redirect = () => window.location.href = "./../";

function deleteFiles(e)
{
    let filesArr = [];
    document.getElementsByName("fileCheckBox").forEach(checkBox => 
    {
        if (checkBox.checked)
            filesArr.push(checkBox.value);
    });

    $.ajax({
        url: './DeleteFiles',
        method: "POST",
        data: JSON.stringify(filesArr),
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        processData: true,
        success: function (data) {
            let table = $('#FilesTable');
            table.find("tbody tr").remove();
            data.forEach(file => 
            {
                table.append(
                    `<tr>` +
                    `    <td class="hover-bg ts-item"><input type="checkbox" name="fileCheckBox" value=${file.FullName}></td>` +
                    `       <td class="hover-bg ts-item"><span>${file.FileName}</span></td>` +
                    `       <td class="hover-bg ts-item"><span>${file.Extension}</span></td>` +
                    `       <td class="hover-bg ts-item"><span>${file.Size}</span></td>` +
                    `       <td class="hover-bg ts-item"><span>${file.CreationDate}</span></td>` +
                    "</tr>"
                )
            });
        }
    });


        // fetch("./DeleteFiles", {
    //     method: 'POST',
    //     headers: {
    //         'Accept': 'application/json; charset=utf-8',
    //         'Content-Type': 'application/json;charset=UTF-8'
    //     },
    //     body: JSON.stringify(filesArr)
    // }).then(response => response.json())
    //     .then((result) => location.reload())
    //     .catch((err) => alert(err));
}

function submitButton(e)
{
    e.preventDefault();
    let linkId = e.target.getAttribute("id");
    document.getElementById((linkId)).submit();
}