import * as React from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import {Button, Checkbox, FormControlLabel} from "@mui/material";


const url = "/api/Authors";


export function CreateAuthor()
{
    return (
        <Box
            component="form"
            sx={{
                '& .MuiTextField-root': { m: 1, width: '25ch' },
            }}

            defaultHeaders={({ initialValues }) => ({
                "Content-Type": "application/json",
                "Authorization": "Bearer " + initialValues.token,
            })}
           
            action={url}
            method="POST"
            noValidate
            autoComplete="off"
        >
            <h1>Create author</h1>
            <TextField
                required
                id="outlined-required"
                label="First name"
                style={{width: 500}}
                name="AuFname"
            />
            <br/>
            
            <TextField
                required
                id="outlined-required"
                label="Last name"
                style={{width: 500}}
                name="AuLname"
            />
            <br/>

            <TextField
                required
                id="outlined-required"
                label="Phone"
                type="number"
                style={{width: 500}}
                name="Phone"
            />
            <br/>
            
            <TextField
                required
                id="outlined-required"
                label="Address"
                style={{width: 500}}
                name="Address"
            />
            <br/>
            
            <TextField
                required
                id="outlined-required"
                label="City"
                style={{width: 500}}
                name="City"
            />
            <br/>

            <TextField
                required
                id="outlined-required"
                label="State"
                style={{width: 500}}
                name="State"
            />
            <br/>

            <TextField
                required
                id="outlined-required"
                label="Zip code"
                type="number"
                style={{width: 500}}
                name="Zip"
            />
            <br/>

            <FormControlLabel control={<Checkbox defaultChecked />}
                              label="Contract" name="Contract" />
            <br/>
            <Button variant="contained"
                    type="submit">
                Create
            </Button>

        </Box>
    )
}