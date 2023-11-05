import * as React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/DeleteOutlined';
import SaveIcon from '@mui/icons-material/Save';
import CancelIcon from '@mui/icons-material/Close';
import {
    GridRowModes,
    DataGrid,
    GridToolbarContainer,
    GridActionsCellItem,
    GridRowEditStopReasons,
} from '@mui/x-data-grid';
import {useEffect, useState} from "react";
import {FetchController} from "../../FetchController";
import {CircularProgress, Fade, Snackbar} from "@mui/material";

function EditToolbar(props) {
    const { setPhones, setRowModes, setCreating } = props;
    
    const handleClick = () =>
    {
        const id = 0;
        setPhones((oldRows) => [{
            id,
            phoneNumber: 79999999,
            fullName: "",
            note: "",
            creationDate: new Date(),
            isNew: true 
        }, ...oldRows]);
        
        setRowModes((oldModel) => ({
            ...oldModel,
            [id]: { mode: GridRowModes.Edit, fieldToFocus: 'fullName' },
        }));
        
        setCreating(true);
    };

    return (
        <GridToolbarContainer>
            <Button color="primary" startIcon={<AddIcon />} onClick={handleClick}>
                Add phone
            </Button>
        </GridToolbarContainer>
    );
}

export function Phones()
{
    const [phones, setPhones] = useState([]);
    const [rowModes, setRowModes] = useState({});
    const [creating, setCreating] = useState(false);
    const [snackBarSettings, setSnackBarSettings] = useState({
           open: false,
           message: "" 
        });
    const [loading, setLoading] = useState(true);
    
    let Phones = new FetchController("api/Phones");
    
    useEffect(() =>
    {
        async function getPhones()
        {
            let json = await Phones.OnGetAsync();
            let phones = json.map(
                phone => {
                    phone.creationDate = new Date(phone.creationDate)
                    return phone;
                });
            setPhones(phones);
            setLoading(false);
        }
        getPhones();
        //setTimeout(getPhones, 3000);
    }, []);
    
    const handleRowEditStop = (params, event) => {
        if (params.reason === GridRowEditStopReasons.rowFocusOut) {
            event.defaultMuiPrevented = true;
        }
    };

    const handleEditClick = (id) => () => {
        setRowModes({ ...rowModes, [id]: { mode: GridRowModes.Edit } });
    };

    const handleSaveClick = (id) => () => {
        setRowModes({ ...rowModes, [id]: { mode: GridRowModes.View } });
    };

    const handleDeleteClick = (id) => () => {
        
        Phones.OnDeleteAsync(id).then(response => {
            let tooltip;

            if (response.ok) {
                tooltip = "Phone deleted successfully";
                setPhones(phones.filter(phone => phone.id !== id));
            } else
                tooltip = response.title;

            setSnackBarSettings({
                open: true,
                message: tooltip
            });


            setPhones(phones.filter((row) => row.id !== id));
        });
    };

    const handleCancelClick = (id) => () => {
        setRowModes({
            ...rowModes,
            [id]: { mode: GridRowModes.View, ignoreModifications: true },
        });

        const editedRow = phones.find((row) => row.id === id);
        if (editedRow.isNew) {
            setPhones(phones.filter((row) => row.id !== id));
        }
    };

    const processRowUpdate = (newRow) => {
        const updatedRow = { ...newRow, isNew: false };
        setPhones(phones.map((row) => (row.id === newRow.id ? updatedRow : row)));
        
        if (creating)
        {
            let oldId = newRow.id;
            delete newRow.id;
            newRow.phoneNumber = newRow.phoneNumber.toString();
            
            Phones.OnPostAsync(newRow).then(response =>
            {
                let tooltip;
                if (response.id)
                {
                    console.log(response.id);
                    tooltip = "Phone added successfully";
                    
                    response.creationDate = new Date(response.creationDate);
                    setPhones([...phones.filter(row => row.id !== oldId), response]);
                    //setPhones(phones.map(row => (row.id === oldId ? )));
                }
                else
                {
                    tooltip = response.title;
                    setPhones(phones.filter(row => row.id !== newRow.id));
                }
                
                setSnackBarSettings({
                    open: true,
                    message: tooltip
                });
            });
        }
        else
        {
            console.log('edit here');
            
            newRow.phoneNumber = newRow.phoneNumber.toString();
            Phones.OnPutAsync(newRow.id, newRow).then(response =>
            {
                console.log(response);
                let tooltip;
                if (response.ok)
                {
                    tooltip = "Phone edited successfully";
                }
                else
                {
                    tooltip = response.statusText;
                    let oldRow = phones.filter(row => row.id == newRow.id)[0];
                    newRow.phoneNumber = oldRow.phoneNumber;
                    newRow.fullName = oldRow.fullName;
                    newRow.creationDate = oldRow.creationDate;
                    newRow.note = oldRow.note;
                }
                
                setSnackBarSettings({
                    open: true,
                    message: tooltip
                });
            });

        }
        setCreating(false);
        
        return updatedRow;
    };

    const handleRowModesModelChange = (newRowModesModel) => {
        setRowModes(newRowModesModel);
    };

    const columns = [
        { field: 'id', headerName: 'ID', width: 30, type: 'number' },
        { field: 'phoneNumber', headerName: 'Phone', width: 200, editable: true },
        { field: 'fullName', headerName: 'Registered to', width: 200, editable: true },
        { field: 'creationDate', headerName: 'Creation date', width: 180, editable: true, type: "date" },
        { field: 'note', headerName: 'Notes', width: 300, editable: true },
        {
            field: 'actions',
            type: 'actions',
            headerName: 'Actions',
            width: 100,
            cellClassName: 'actions',
            getActions: ({ id }) => {
                const isInEditMode = rowModes[id]?.mode === GridRowModes.Edit;

                if (isInEditMode) {
                    return [
                        <GridActionsCellItem
                            icon={<SaveIcon />}
                            label="Save"
                            sx={{
                                color: 'primary.main',
                            }}
                            onClick={handleSaveClick(id)}
                        />,
                        <GridActionsCellItem
                            icon={<CancelIcon />}
                            label="Cancel"
                            className="textPrimary"
                            onClick={handleCancelClick(id)}
                            color="inherit"
                        />,
                    ];
                }

                return [
                    <GridActionsCellItem
                        icon={<EditIcon />}
                        label="Edit"
                        className="textPrimary"
                        onClick={handleEditClick(id)}
                        color="inherit"
                    />,
                    <GridActionsCellItem
                        icon={<DeleteIcon />}
                        label="Delete"
                        onClick={handleDeleteClick(id)}
                        color="inherit"
                    />,
                ];
            },
        },
    ];

    return (
        <Fade in={true}>
            
            <Box
                sx={{
                    height: '100%',
                    width: '100%',
                    '& .actions': {
                        color: 'text.secondary',
                    },
                    '& .textPrimary': {
                        color: 'text.primary',
                    },
                }}
            >
                <div style={{
                    position: 'absolute',
                    width: '100%',
                    height: '100%',
                    left: '0%',
                    top: '0%',
                    backgroundColor: 'rgba(0,0,0,.1)',
                    display: loading ? 'block' : 'none'
                }}/>
                <CircularProgress style={{
                    position: 'absolute',
                    left: '50%',
                    top: '50%',
                    display: loading ? 'block' : 'none'
                }}/>
    
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
                
                <DataGrid
                    rows={phones}
                    columns={columns}
                    editMode="row"
                    rowModesModel={rowModes}
                    onRowModesModelChange={handleRowModesModelChange}
                    onRowEditStop={handleRowEditStop}
                    processRowUpdate={processRowUpdate}
                    slots={{
                        toolbar: EditToolbar,
                    }}
                    slotProps={{
                        toolbar: { setPhones, setRowModes, setCreating },
                    }}
                />
            </Box>
        </Fade>
    );
}