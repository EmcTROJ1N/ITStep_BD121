import { useState } from 'react';
import { useEffect } from 'react';
import { useRef } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import {Button, ButtonGroup} from "@mui/material";
import {NavLink} from "reactstrap";

const url = "/api/Authors";
let dataGrid;
let selectedIds = []
async function deleteSelectedAuthors(e)
{
    const request = await fetch(url, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(selectedIds),
    });
    const response = await request.json();
    console.log(response);
}


export function Authors(props)
{
    const [authors, setAuthors] = useState([]);
   
    useEffect(async () =>
    {
        async function getAuthors()
        {
            let response = await fetch(url);
            let json = await response.json();
            setAuthors(json);
        }
        await getAuthors();
    }, []);

    const columns = [
        { field: 'auId', headerName: 'ID', width: 130 },
        { field: 'auFname', headerName: 'First name', width: 100 },
        { field: 'auLname', headerName: 'Last name', width: 100 },
        { field: 'phone', headerName: 'Phone', width: 130 },
        { field: 'address', headerName: 'Address', width: 130 },
        { field: 'city', headerName: 'City', width: 130 },
        { field: 'state', headerName: 'State', width: 30},
        { field: 'zip', headerName: 'Zip code', width: 70, type: "number"},
        { field: 'contract', headerName: 'Contract', width: 50, type: "boolean"}
    ];
 
    dataGrid = (
        <DataGrid
            style={{ marginBottom: 30 }}
            getRowId={ row => row.auId }
            onRowSelectionModelChange={ids =>
                selectedIds = ids.slice()}
            rows={ authors }
            columns={columns}
            initialState={{
                pagination: {
                    paginationModel: { page: 0, pageSize: 10 },
                },
            }}
            pageSizeOptions={[10, 20]}
            checkboxSelection
        />
    )
        
        
    return (
        <div style={{ height: 700, width: '100%'}}>

            {dataGrid}

            <ButtonGroup color="secondary" aria-label="medium secondary button group">
                <Button onClick={e => deleteSelectedAuthors(e)}>Delete selected</Button>
                <Button href="/createAuthor">
                    Create new
                </Button>
            </ButtonGroup>
            
        </div>
    );
}