import * as React from "react";
import {useRef, useState} from "react";
import {FormControl, InputLabel, ListItemButton, MenuItem, Select, TextField, ToggleButton} from "@mui/material";
import CheckIcon from "@mui/icons-material/Check";
import ModeEditOutlineOutlinedIcon from "@mui/icons-material/ModeEditOutlineOutlined";

export function ContactListSelectItem(props)
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

                    <FormControl style={{minWidth: "75%"}}>
                        <InputLabel id="demo-simple-select-label">{props.FieldName}</InputLabel>
                        <Select
                            disabled={!selected}
                            id="filled-disabled"
                            ref={textValueField}
                            labelId="demo-simple-select-label"
                            label={props.FieldName}
                            onChange={e => {
                                setFieldValue(props.Values[e.target.value][props.FieldNameObj]);
                            }}
                            value={props.Values.filter(value => value[props.FieldNameObj] === fieldValue)[0].id}
                        >
                            {props.Values.map(item =>
                                <MenuItem value={item.id}>
                                    {item[props.FieldNameObj]}
                                </MenuItem>)}

                        </Select>
                    </FormControl>

                    <ToggleButton
                        value="check"
                        disabled={props.FieldName == "Contact ID"}
                        selected={selected}
                        onChange={e => {
                            if (selected)
                                props.ContactEditCallback(props.Id, props.FieldValue, fieldValue);
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

