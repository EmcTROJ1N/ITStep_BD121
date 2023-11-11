import {useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import styles from "../styles/contacts.css"
import {FetchController} from "../FetchController";

export function EditContact(props)
{
    const params = useParams();

    const [contact, setContact] = useState({});
    const [phones, setPhones] = useState([]);
    const [categories, setCategories] = useState([]);
    
    let PhonesController = new FetchController("api/Phones");
    let CategoriesController = new FetchController("api/Categories");
    let ContactsController = new FetchController("api/Contacts");

    useEffect(() =>
    {
        CategoriesController.OnGetAsync().then(data => setCategories(data));
        PhonesController.OnGetAsync().then(data => setPhones(data));
     
        ContactsController.OnGetAsync(params.id).then(response =>
            setContact(response))
            .catch(err => alert(err));
    }, []);
    
    
    function editContact(newContact)
    {
        newContact.id = params.id;
        ContactsController.OnPutAsync(params.id, newContact).then(response =>
            window.location.href = "/contacts")
            .catch(err => alert(err));
    }
    
    return (
        <div style={{width: '40%'}}>
                <form style={{marginLeft: '30px'}} name="addContact">
                    <h1>Edit Contact</h1>
                    
                    <label>Full name</label>
                    <input type="text" placeholder="Name" name="fullName"/>
                    
                    <label>Phone number</label>
                    <select name="phoneId">
                        {phones.map(phone =>
                            <option value={phone.id}>{phone.phoneNumber}</option>
                        )}
                    </select>

                    <label>Category</label>
                    <select name="categoryId">
                        {categories.map(category =>
                            <option value={category.id}>{category.categoryName}</option>
                        )}
                    </select>
                    
                    <button onClick={e => {
                                e.preventDefault();
                                
                                editContact({
                                   fullName: document.addContact.fullName.value, 
                                   phoneId: parseInt(document.addContact.phoneId.value),
                                   categoryId: parseInt(document.addContact.categoryId.value)
                                });
                            }}>
                        Save
                    </button>
                    <button onClick={e => window.location.href = "/contacts"}>Cancel</button>
                </form>
            </div>
    );

}