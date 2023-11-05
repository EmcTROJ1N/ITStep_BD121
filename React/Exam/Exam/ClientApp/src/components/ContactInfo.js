import * as React from 'react';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardContent from '@mui/material/CardContent';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import { red } from '@mui/material/colors';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import {useEffect, useRef, useState} from "react";
import {Box, Fade, } from "@mui/material";
import List from "@mui/material/List";
import CloseIcon from '@mui/icons-material/Close';
import {ContactListItem} from "./ContactsListItems/ContactListItem";
import {ContactListDateItem} from "./ContactsListItems/ContactListDateItem";
import {ContactListSelectItem} from "./ContactsListItems/ContactListSelectItem";
import {FetchController} from "../FetchController";

export default function ContactInfo(props)
{
    const [fade, setFade] = useState(true);
    
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
        (props.Visible &&
            <Fade in={true}>
                <Card sx={{ margin: 2 }}>
                    <CardHeader
                        avatar={
                            <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                                {props.Contact.firstName[0]}
                            </Avatar>
                        }
                        action={
                            <IconButton aria-label="settings"
                                onClick={e => props.VisibilityCallback(false)}>
                                <CloseIcon />
                            </IconButton>
                        }
                        title= {`${props.Contact.firstName} ${props.Contact.lastName}`}
                        subheader={props.Contact.phoneNumber}
                    />
                    <CardContent>
                        <Box>
                            <List
                                sx={{ width: '60%',  bgcolor: 'background.paper' }}
                                component="nav"
                                aria-labelledby="nested-list-subheader">
                                
                                <ContactListItem ContactEditCallback={props.ContactEditCallback}
                                                 FieldName="Contact ID"
                                                 FieldValue={props.Contact.id}
                                                 Id={props.Contact.id}
                                />
                                <ContactListSelectItem ContactEditCallback={props.ContactEditCallback} FieldName="Phone"
                                                       FieldValue={props.Contact.phoneNumber}
                                                       Values={phones}
                                                       FieldNameObj="phoneNumber"
                                                       Id={props.Contact.id}
                                />
                                <ContactListSelectItem ContactEditCallback={props.ContactEditCallback} FieldName="Category"
                                                       FieldValue={props.Contact.categoryName}
                                                       Values={categories}
                                                       FieldNameObj="categoryName"
                                                       Id={props.Contact.id}
                                />
                                <ContactListItem ContactEditCallback={props.ContactEditCallback} FieldName="Address" FieldValue={props.Contact.address}/>
                                <ContactListItem ContactEditCallback={props.ContactEditCallback} FieldName="Email" FieldValue={props.Contact.email}/>
                                <ContactListDateItem ContactEditCallback={props.ContactEditCallback} FieldName="Birthday" FieldValue={props.Contact.birthday}/>
                                <ContactListItem ContactEditCallback={props.ContactEditCallback} FieldName="Notes" FieldValue={props.Contact.notes}/>
        
                            </List>
                        </Box>
                    </CardContent>
                </Card>
            </Fade>
        )
    );
}