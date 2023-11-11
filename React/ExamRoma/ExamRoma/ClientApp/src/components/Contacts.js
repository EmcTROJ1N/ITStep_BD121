import {useEffect, useState} from "react";
import styles from "../styles/contacts.css"
import {FetchController} from "../FetchController";

export function Contacts(props)
{
    const [contacts, setContacts] = useState([]);
    const [phones, setPhones] = useState([]);
    const [categories, setCategories] = useState([]);
    
    let PhonesController = new FetchController("api/Phones");
    let CategoriesController = new FetchController("api/Categories");
    let ContactsController = new FetchController("api/Contacts");
    
    function getSourceData()
    {
        let data = [...contacts];
        
        contacts.map(contact => {
            let phone = phones.filter(phone => phone.id === contact.phoneId)[0];
            let category = categories.filter(category => category.id === contact.categoryId)[0];
            
            contact.phoneNumber = phone.phoneNumber;
            contact.categoryName = category.categoryName;
            
            return contact;
        });
        
        return data;
    }

    useEffect(() =>
    {
        CategoriesController.OnGetAsync().then(data => setCategories(data));
        PhonesController.OnGetAsync().then(data => setPhones(data));
        ContactsController.OnGetAsync().then(data => setContacts(data));
    }, []);


    function deleteContact(id)
    {
        ContactsController.OnDeleteAsync(id).then(response =>
            setContacts(contacts.filter(contact => contact.id !== id)))
            .catch(err => alert(err))
    }

    function createContact(contact)
    {
        ContactsController.OnPostAsync(contact)
            .then(response => {
                setContacts([...contacts, response]);
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
                        <td>Id</td>
                        <td>Contact full name</td>
                        <td>Phone number</td>
                        <td>Category</td>
                        <td>Action</td>
                    </tr>
                    </thead>
                    <tbody>

                    {getSourceData().map(contact =>
                        (
                            <tr>
                                <td>{contact.id}</td>
                                <td>{contact.fullName}</td>
                                <td>{contact.phoneNumber}</td>
                                <td>{contact.categoryName}</td>
                                <td>
                                    <button onClick={e => window.location.href = `/editContact/${contact.id}`}>Edit</button>
                                    <button onClick={e =>  deleteContact(contact.id)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>


            <div style={{width: '40%'}}>
                <form style={{marginLeft: '30px'}} name="addContact">
                    <h1>Add Contact</h1>
                    
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
                                
                                createContact({
                                   fullName: document.addContact.fullName.value, 
                                   phoneId: parseInt(document.addContact.phoneId.value),
                                   categoryId: parseInt(document.addContact.categoryId.value)
                                });
                            }}>
                        Add
                    </button>
                </form>
            </div>
        </div>
    )
}
