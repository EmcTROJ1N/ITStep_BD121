import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import {FormControl, InputLabel, MenuItem, Select} from "@mui/material";
import {useEffect, useState} from "react";
import {FetchController} from "../../FetchController";

export function CreateDialog(props)
{
    const [phones, setPhones] = useState([]);
    const [categories, setCategories] = useState([]);
    
    let Phones = new FetchController("api/Phones");
    let Categories = new FetchController("api/Categories");
    
    useEffect(() =>
    {
        async function setPhonesAsync()
        {
            let phonesReponse = await Phones.OnGetAsync();
            setPhones(phonesReponse);
        }
        
        async function setCategoriesAsync()
        {
            let categoriesResponse = await Categories.OnGetAsync();
            setCategories(categoriesResponse);
        }
        
        setPhonesAsync();
        setCategoriesAsync();
    }, []);
    
    return (
        <div>
            <Dialog open={props.Open} onClose={e => props.SetOpenCallback(false)}>
                <DialogTitle>Create contact</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Enter all necessary data 
                    </DialogContentText>
                    <form name="contactForm">
                        <TextField
                            autoFocus
                            margin="dense"
                            name="firstName"
                            label="First name"
                            fullWidth
                            variant="standard"
                        />
                        <TextField
                            margin="dense"
                            name="lastName"
                            label="Last name"
                            fullWidth
                            variant="standard"
                        />
                        
                        <FormControl 
                            variant="standard" 
                            fullWidth
                            margin="dense">
                            <InputLabel id="demo-simple-select-standard-label">Phone</InputLabel>
                            <Select
                                labelId="demo-simple-select-standard-label"
                                id="demo-simple-select-standard"
                                label="Phone"
                                fullWidth
                                name="phone"
                            >
                                {phones.map(phone =>
                                        <MenuItem value={phone.id}>{phone.phoneNumber}</MenuItem>
                                )}
                            </Select>
                        </FormControl>


                        <FormControl
                            variant="standard"
                            margin="dense"
                            fullWidth>
                            <InputLabel id="demo-simple-select-standard-label">Category</InputLabel>
                            <Select
                                labelId="demo-simple-select-standard-label"
                                id="demo-simple-select-standard"
                                label="Category"
                                fullWidth
                                name="category"
                            >
                                {
                                    categories.map(category =>
                                        <MenuItem value={category.id}>{category.categoryName}</MenuItem>
                                    )
                                }
                            </Select>
                        </FormControl>
    
                        <TextField
                            margin="dense"
                            name="email"
                            label="Email address"
                            type="email"
                            fullWidth
                            variant="standard"
                        />
    
                        <TextField
                            margin="dense"
                            name="birthday"
                            label="Birthday"
                            type="date"
                            fullWidth
                            variant="standard"
                        />
    
                        <TextField
                            margin="dense"
                            name="address"
                            label="Address"
                            fullWidth
                            variant="standard"
                        />
                        <TextField
                            margin="dense"
                            name="notes"
                            label="Notes"
                            multiline
                            fullWidth
                            variant="standard"
                        />
                    </form>

                </DialogContent>
                <DialogActions>
                    <Button onClick={e => props.SetOpenCallback(false)}>Cancel</Button>
                    <Button onClick={e => {
                        let contact = {
                            firstName: document.contactForm.firstName.value,
                            lastName: document.contactForm.lastName.value,
                            phoneId: parseInt(document.contactForm.phone.value),
                            categoryId: parseInt(document.contactForm.category.value),
                            email: document.contactForm.email.value,
                            address: document.contactForm.address.value,
                            birthday: document.contactForm.birthday.value,
                            notes: document.contactForm.notes.value,
                            phone: null,
                            category: null,
                            //id: null
                        };
                        let phoneNumber = phones.filter(phone => phone.id === contact.phoneId)[0].phoneNumber;
                        let categoryName = categories.filter(category => category.id === contact.categoryId)[0].categoryName;
                        props.Action(contact, phoneNumber, categoryName);
                        props.SetOpenCallback(false)
                    }}>
                        Create
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}