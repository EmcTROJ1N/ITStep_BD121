import ListItem from "@mui/material/ListItem";
import ListItemAvatar from "@mui/material/ListItemAvatar";
import Avatar from "@mui/material/Avatar";
import ListItemText from "@mui/material/ListItemText";
import * as React from "react";
import Typography from "@mui/material/Typography";
import {Link, useNavigate} from "react-router-dom";
import DeleteIcon from '@mui/icons-material/Delete';
import {IconButton, Skeleton} from "@mui/material";
import {createElement, useState} from "react";
import ReactDOM from 'react-dom';
import {createRoot} from "react-dom/client";

export function ContactItem(props)
{
    const [contact, setContact] = useState(props.Contact);
    
    return (

        <ListItem alignItems="flex-start"
                  secondaryAction={
                      <IconButton
                          onClick={() => {
                              props.SetIdForDeletingCallback(props.Contact.id);
                              props.UpdateDialogCallback(true);
                          }}
                          edge="end" aria-label="delete">
                          <DeleteIcon/>
                      </IconButton>
                  }>
            
            <ListItemAvatar>
                <Avatar alt="Remy Sharp" />
            </ListItemAvatar>
            
            <ListItemText
                primary={`${contact.firstName} ${contact.lastName}`}
                secondary={
                    <React.Fragment>
                        <Typography
                            sx={{ display: 'inline' }}
                            component="span"
                            variant="body2"
                            color="text.primary"
                            marginRight={1}
                        >
                            {contact.phoneNumber}
                        </Typography>
                        <Link to={""}
                              onClick={(e) =>
                              {
                                  e.preventDefault();
                                  props.UpdateCardVisibilityCallback(true);
                                  props.UpdateCardCallback(props.Contact);
                              }
                        }>Edit</Link>
                    </React.Fragment>
                }
            />
        </ListItem>
    )
}