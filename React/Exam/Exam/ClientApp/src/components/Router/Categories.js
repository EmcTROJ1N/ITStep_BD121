import ListItem from "@mui/material/ListItem";
import List from "@mui/material/List";
import {Alert, Fade, IconButton, ListItemButton} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import ListItemText from "@mui/material/ListItemText";
import {useEffect, useRef, useState} from "react";
import {FetchController} from "../../FetchController";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import TextField from "@mui/material/TextField";
import DialogActions from "@mui/material/DialogActions";
import Button from "@mui/material/Button";
import * as React from "react";

export function Categories(props)
{
    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [modalOpened, setModalOpened] = useState(false);
    const [alertSettings, setAlertSettings] = useState({
        message: "",
        open: false,
        severity: ""
    });
    
    let Categories = new FetchController("api/Categories");
    
    useEffect(() =>
    {
        async function getCategories()
        {
            let json = await Categories.OnGetAsync();
            setCategories(json);
            setLoading(false);
        }
        //setTimeout(getCategories, 10000);
        getCategories();
    }, []);
    
    async function deleteCategory(id)
    {
        let response = await Categories.OnDeleteAsync(id);
        let message;
        let severity;
        
        if (response.ok)
        {
            setCategories(categories.filter(category => category.id !== id));
            severity = "success";
            message = "Category deleted";
        }
        else
        {
            severity = "error";
            message = response.statusText;
        }
        
        setAlertSettings({
            message: message,
            open: true,
            severity: severity
        });
        setTimeout(() => {
            setAlertSettings({
                open: false,
                message: "",
                severity: ""
            });
        }, 3000);
    }
    
    async function createCategory(name)
    {
        let category = {
            categoryName: name,
            note: ""
        };
        let severity;
        let message;
        let response = await Categories.OnPostAsync(category);
        
        if (response.id)
        {
            severity = "success";
            message = "Success!"
            console.log(response);
            setCategories([...categories, response]);
        }
        else
        {
            severity = "error";
            message = response.title;
        }

        setAlertSettings({
            message: message,
            open: true,
            severity: severity
        });
        setTimeout(() => {
            setAlertSettings({
                open: false,
                message: "",
                severity: ""
            });
        }, 3000);
        
        setModalOpened(false);
    }
    
    return (
        
        <div>

            
            <Dialog open={modalOpened} onClose={e => setModalOpened(false) }>
                <DialogTitle>Create category</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Enter new category name
                    </DialogContentText>
                    <form name="categoryForm">
                        <TextField
                            autoFocus
                            margin="dense"
                            name="categoryName"
                            label="Name"
                            fullWidth
                            variant="standard"
                        />
                    </form>

                </DialogContent>
                <DialogActions>
                    <Button onClick={e => setModalOpened(false)}>Cancel</Button>
                    <Button onClick={e => createCategory(document.categoryForm.categoryName.value)}>
                        Create
                    </Button>
                </DialogActions>
            </Dialog>
            
            <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
                {categories.map(category => (
                    <ListItem
                        key={category.id}
                        secondaryAction={
                            <IconButton aria-label="comment" key={category.id} onClick={e => {
                                deleteCategory(category.id);
                            }}>
                                <DeleteIcon/>
                            </IconButton>
                        }
                    >
                        <ListItemText primary={category.categoryName} />
                    </ListItem>
                ))}
                <ListItemButton onClick={e => setModalOpened(true)}>Add category</ListItemButton>
            </List>
            
                <Alert severity={alertSettings.severity} style={{
                    display: alertSettings.open ? '' : 'none'
                }}>
                    {alertSettings.message}
                </Alert>
        </div>
    )
}