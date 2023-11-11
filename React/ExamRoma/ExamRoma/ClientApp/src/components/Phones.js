import {useEffect, useState} from "react";
import {FetchController} from "../FetchController";

export function Phones(props)
{
    const [phones, setPhones] = useState([]);
    let PhonesController = new FetchController("api/Phones");
    
    useEffect(() =>
    {
        async function setPhonesAsync()
        {
            let response = await PhonesController.OnGetAsync();
            setPhones(response);
        }

        setPhonesAsync();
    }, []);


    function deletePhone(id)
    {
        PhonesController.OnDeleteAsync(id).then(response =>
            setPhones(phones.filter(phone => phone.id !== id)))
            .catch(err => alert(err))
    }

    function createPhone(phone)
    {
        PhonesController.OnPostAsync({phoneNumber: phone})
            .then(response => {
                setPhones([...phones, response]);
        }).catch(err => alert(err));
    }

    return (
        <div style={{
            display: 'flex',
        }}>
            <div style={{width: '60%'}}>
                <h1>Phones</h1>
                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                    <tr>
                        <td>Phone number</td>
                        <td>Action</td>
                    </tr>
                    </thead>
                    <tbody>

                    {phones.map(phone =>
                        (
                            <tr>
                                <td>{phone.phoneNumber}</td>
                                <td><button onClick={e =>  deletePhone(phone.id)}>Delete</button></td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>


            <div style={{width: '40%'}}>
                <form style={{marginLeft: '30px'}} name="addPhone">
                    <h1 style={{marginTop: '10px', marginBottom: '10px'}} >Add phone</h1>
                    <input style={{marginTop: '10px', marginBottom: '10px'}} type="text" placeholder="+7xxxxxxxx" name="phoneNumber"/>
                    <br/>
                    <button style={{marginTop: '10px', marginBottom: '10px'}}
                            onClick={e => {
                                e.preventDefault();
                                createPhone(document.addPhone.phoneNumber.value)
                                document.addPhone.phoneNumber.value = "";
                            }}>
                        Add
                    </button>
                </form>
            </div>
        </div>
    )
}
