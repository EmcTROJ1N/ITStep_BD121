import {Skeleton} from "@mui/material";
import Avatar from "@mui/material/Avatar";
import * as React from "react";


export function ContactItemSkeleton() {
    return <div style={{
        display: 'flex',
        width: '100%',
        alignItems: 'center',
        marginBottom: '20px',
        marginTop: '20px',
    }}>
        <Skeleton variant="circular">
            <Avatar/>
        </Skeleton>

        <Skeleton style={{
            width: '80%',
            height: '60px',
            marginLeft: '10px'
        }}>
        </Skeleton>
    </div>
}