import { useState, useRef, useEffect } from 'react';
import * as React from 'react';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import {Button, IconButton, ListItemButton, Skeleton, Snackbar} from "@mui/material";
import ListItem from "@mui/material/ListItem";
import ContactInfo from "../ContactInfo";
import {ContactItem} from "../ContactItem";
import {ContactItemSkeleton} from "../Skeletons/ContactItemSkeleton";
import {CreateDialog} from "../ModalDialogs/CreateDialog";
import AlertDialog from "../ModalDialogs/AlertDialog";
import {FetchController} from "../../FetchController";
import {Categories} from "./Categories";

export function Contacts()
{
    const [contacts, setContacts] = useState([]);
    const [blackListContatsIds, setBlackListContactsIds] = useState([]);
    const [contactForEdit, setContactForEdit] = useState();
    const [cardVisible, setCardVisibility] = useState(false);
    const [loaded, setLoaded] = useState(false);
    const [deleteDialog, setDeleteDialog] = useState(false);
    const [createDialog, setCreateDialog] = useState(false);
    const [idForDeleting, setIdForDeleting] = useState();
    const [snackBarSettings, setSnackBarSettings] = useState({
           open: false,
           message: "" 
        });
    let Contacts = new FetchController("api/Contacts");
    let Procedures = new FetchController("api/Procedures");
    
    useEffect(() =>
    {
        async function getContacts()
        {
            let json = (await Procedures.OnGetAsync()).map(contact => {
                contact.birthday = new Date(contact.birthday);
                return contact;
            });
            
            setContacts(json);
            setLoaded(true);
        }
        //setTimeout(getContacts, 10000);
        getContacts();
    }, []);
    
    async function createContact(contact, phoneNumber, categoryName)
    {
        let response = await Contacts.OnPostAsync(contact);
        console.log(response);
        
        
        let tooltip;
        if (response.id)
        {
            tooltip = "Contact added successfully";
            contact.phoneNumber = phoneNumber;
            contact.categoryName = categoryName;
            contact.id = response.id
            
            setContacts([...contacts, contact]);
        }
        else
            tooltip = response.title;
        
        setSnackBarSettings({
            open: true,
            message: tooltip
        });
    }
    
    async function deleteContact(id)
    {
        console.log(id);
        let response = await Contacts.OnDeleteAsync(id);
        let tooltip;
        
        if (response.ok)
        {
            tooltip = "Contact deleted successfully";
            setBlackListContactsIds([...blackListContatsIds, id])//;
        }
        else
            tooltip = response.title;
        
        setSnackBarSettings({
            open: true,
            message: tooltip
        });
    
    }
    
    async function editContact(id, lastValue, newValue)
    {
        console.log(id);
        console.log(lastValue);
        console.log(newValue);
        console.log(contactForEdit);
        let newContact = contactForEdit;
        let fieldName = Object.keys(newContact)[Object.values(newContact).indexOf(lastValue)];
        newContact[fieldName] = newValue;
        
        let tooltip;
        let response = await Contacts.OnPutAsync(newContact.id, newContact);
        
        if (response.ok)
        {
            tooltip = "Contact edited successfully";
            setContactForEdit(newContact);
        }
        else
        {
            tooltip = response.title;
            if (!response.title)
                tooltip = response.statusText;
        }

        setSnackBarSettings({
            open: true,
            message: tooltip
        });
    }
    
    return (
        <div style={{
            height: '100%',
            width: '100%',
            display: 'flex',
        }}>
            <AlertDialog
                Open={deleteDialog}
                SetOpenCallback={setDeleteDialog}
                Header="System message"
                Question="Are you sure you want to delete this user? The action is irreversible."
                Action={() => deleteContact(idForDeleting)}
            />
            <CreateDialog
                Open={createDialog}
                SetOpenCallback={setCreateDialog}
                Action={(contact, phoneNumber, categoryName) => createContact(contact, phoneNumber, categoryName)}
            />
            <Snackbar
                open={snackBarSettings.open}
                autoHideDuration={3000}
                onClose={e =>
                    setSnackBarSettings({
                        open: false,
                        message: ""
                    })}
                message={snackBarSettings.message}
            />
            
            <div style={{width: '30%'}}>
                <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
                    {!loaded &&
                        ([...new Array(9)]).map(n =>
                            <ContactItemSkeleton/>)}
                    
                    {loaded &&
                        contacts.map((contact, idx) => {
                            if (blackListContatsIds.includes(contact.id))
                                return;
                            return (
                                <div key={contact.id}>
                                    <ContactItem
                                        Contact={contact}
                                        UpdateCardCallback={setContactForEdit}
                                        UpdateCardVisibilityCallback={setCardVisibility}
                                        UpdateDialogCallback={setDeleteDialog}
                                        SetIdForDeletingCallback={setIdForDeleting}
                                    />
    
                                    {idx !== contacts.length - 1 && (
                                        <Divider variant="inset" component="li" />
                                    )}
                                </div>
                            );
                        })
                    }
                    
                    <ListItem varian="contained">
                        <Button
                            variant="contained"
                            onClick={e => setCreateDialog(true)}
                        >
                            Create new
                        </Button>
                    </ListItem>
                </List>
            </div>
            
            <div style={{width: '70%'}}>
                <ContactInfo Visible={cardVisible}
                             Contact={contactForEdit}
                             VisibilityCallback={setCardVisibility}
                             ContactEditCallback={editContact}
                />
            </div>
            
        </div>
    );
}