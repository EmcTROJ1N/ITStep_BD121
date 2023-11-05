import * as React from "react";
import {useRef, useState} from "react";
import {ListItemButton, TextField, ToggleButton} from "@mui/material";
import CheckIcon from "@mui/icons-material/Check";
import ModeEditOutlineOutlinedIcon from "@mui/icons-material/ModeEditOutlineOutlined";

export function ContactListItem(props)
{
    const [selected, setSelected] = useState(false);
    
    const [fieldValue, setFieldValue] = useState(props.FieldValue);
    const [lastfieldValue, setLastFieldValue] = useState(props.FieldValue);
    
    const textValueField = useRef(null);
    
    if (props.FieldValue !== fieldValue &&
        props.FieldValue !== lastfieldValue)
    {
        setLastFieldValue(props.FieldValue);
        setFieldValue(props.FieldValue);
    }
    
    if (textValueField.current)
        textValueField.current.disabled = selected;
    
    return (
        <ListItemButton>
            <div style=
                     {{
                         display: 'flex',
                         width: '100%',
                         justifyContent: 'space-between',
                         alignItems: 'center'
                     }}>
                <h6>{props.FieldName}</h6>
                
                <div style={{
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    width: '300px'
                }}>

                    <TextField
                        disabled={!selected}
                        id="filled-disabled"
                        ref={textValueField}
                        onChange={e => {
                            setFieldValue(e.target.value);
                        }}
                        value={fieldValue}
                    />
                
                    <ToggleButton
                        value="check"
                        disabled={props.FieldName == "Contact ID"}
                        selected={selected}
                        onChange={e => {
                            if (selected) {
                                props.ContactEditCallback(props.Id, props.FieldValue, fieldValue);
                            }
                            setSelected(!selected);
                        }}
                    >
                        { selected ? <CheckIcon/> : <ModeEditOutlineOutlinedIcon/> }
                    </ToggleButton>
                </div>

            </div>
        </ListItemButton>
    )
}

