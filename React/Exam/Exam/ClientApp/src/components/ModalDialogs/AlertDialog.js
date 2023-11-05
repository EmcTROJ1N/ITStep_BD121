import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import {useState} from "react";

export default function AlertDialog(props)
{
    return (
        <div>
            <Dialog
                open={props.Open}
                onClose={e => props.SetOpenCallback(false)}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {props.Header}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        {props.Question}
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={e => {
                        props.Action();
                        props.SetOpenCallback(false);
                    }}>
                        Ok
                    </Button>
                    <Button onClick={e => props.SetOpenCallback(false)} autoFocus>
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}